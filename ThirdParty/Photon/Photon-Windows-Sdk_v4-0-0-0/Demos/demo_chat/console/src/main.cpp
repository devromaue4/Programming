#include <iostream>

#include "ChatListener.h"

using namespace ExitGames;
using namespace ExitGames::Common;

bool useTcp = false;

static const ExitGames::Common::JString appId = L"<no-app-id>"; // set your app id here
static const ExitGames::Common::JString appVersion = L"1.0";

#if defined _EG_WINDOWS_PLATFORM
#	define SLEEP(ms) Sleep(ms)
#else
#	define SLEEP(ms) usleep(ms*1000)
#endif

const int USER_COUNT = 1000;

static JVector<JString> friends;

static void usage();
static int handleKbd(Chat::Client& chatClient);

static void subscribe(Chat::Client& chatClient)
{
	JVector<JString> chs;
	chs.addElement("a");
	chs.addElement("b");
	chs.addElement("v");
	if(chatClient.opSubscribe(chs))
		wprintf(L"Subscribing...\n");
	if(chatClient.opAddFriends(friends))
		wprintf(L"Adding friends: %s ...\n", friends.toString().cstr());
}

static void unsubscribe(Chat::Client& chatClient)
{
	JVector<JString> chs;
	chs.addElement("a");
	chs.addElement("nonexisting");
	chs.addElement("b");
	chs.addElement("v");
	if(chatClient.opUnsubscribe(chs))
	wprintf(L"Unsubscribing...\n");
	if(chatClient.opRemoveFriends(friends))
		wprintf(L"Clearing friends: %s ...\n", friends.toString().cstr());
}

int main(int argc, const char* argv[])
{
	usage();
	ChatListener chatListener;
	JString usernamePrefix("user");
	int t = GETTIMEMS();
	int localIndex = (t > 0 ? t : -t) % USER_COUNT;
	JString username;
	for(int i=0; i<USER_COUNT; ++i)
	{
		if(i == localIndex)
			username = usernamePrefix + i;
		else
			friends.addElement(usernamePrefix + i);
	}
	Chat::Client chatClient(chatListener, appId, appVersion, username, useTcp ? Photon::ConnectionProtocol::TCP:Photon::ConnectionProtocol::UDP);

	wprintf(L"Connecting to nameserver as user %ls\n\n", username.cstr());
	chatClient.connect();
	while(true)
	{
		chatClient.service();
		if(handleKbd(chatClient))
			break;
	}

	return 0;
}

static const wchar_t* statusNames[] = {L"OFFLINE", L"INVISIBLE", L"ONLINE", L"AWAY", L"DND", L"LFG", L"PLAYING"};

static void usage()
{
	wprintf(L"Photon Cloud Chat Demo\n");
	wprintf(L"h - print this help\n");
	wprintf(L"s - subscribe\n");
	wprintf(L"u - unsubscribe\n");
	wprintf(L"<enter>bla<enter> - publish message to random channel\n");
	wprintf(L"<enter>x:bla<enter> - publish message to channel 'x'\n");
	wprintf(L"<enter>x@bla<enter> - send private message to user 'x'\n");
	wprintf(L"d - disconnect\n");
	wprintf(L"c - connect\n");
	wprintf(L"l - list all channels and messages\n");
	wprintf(L"Esc - close the application\n");
	for(int i = 0;i < sizeof(statusNames) / sizeof(const char*);++i)
		wprintf(L"%c - %s\n", '1' + i, statusNames[i]);
	wprintf(L"\n");
}

#ifdef _EG_WINDOWS_PLATFORM
#include <conio.h>
static int getcharIfKbhit(void)
{
	int res = _kbhit();
	if(res)
		res = _getch();
	return res;
}
#else
#include <termios.h>
#include <fcntl.h>
int getcharIfKbhit(void)
{
	struct termios oldt, newt;
	int ch;
	int oldf;

	tcgetattr(STDIN_FILENO, &oldt);
	newt = oldt;
	newt.c_lflag &= ~(ICANON | ECHO);
	tcsetattr(STDIN_FILENO, TCSANOW, &newt);
	oldf = fcntl(STDIN_FILENO, F_GETFL, 0);
	fcntl(STDIN_FILENO, F_SETFL, oldf | O_NONBLOCK);

	ch = getchar();

	tcsetattr(STDIN_FILENO, TCSANOW, &oldt);
	fcntl(STDIN_FILENO, F_SETFL, oldf);

	return ch;
}
#endif

static void listChannels(Chat::Client& chatClient);

static int handleKbd(Chat::Client& chatClient)
{
	static bool typeMode = false;
	static const int TYPE_BUF_LEN = 1000;
	static char typeBuf[TYPE_BUF_LEN + 1];
	static int typeBufPos = 0;

	int k = getcharIfKbhit();
	if(!k)
		;
	else if(k == 27)
		return 1;
	else if(k == 13)
	{
		wprintf(L"\n");
		typeMode = !typeMode;
		if(!typeMode)
		{
			typeBuf[typeBufPos] = '\0';
			int sep = 0;
			while(typeBuf[sep] && typeBuf[sep++] != ':');
			if(sep != typeBufPos)
			{
				typeBuf[sep - 1] = '\0';
				chatClient.opPublishMessage(typeBuf, ValueObject<JString>(typeBuf + sep));
			}
			else
			{
				int sep = 0;
				while(typeBuf[sep] && typeBuf[sep++] != '@');
				if(sep != typeBufPos)
				{
					typeBuf[sep - 1] = '\0';
					chatClient.opSendPrivateMessage(typeBuf, ValueObject<JString>(typeBuf + sep));
				}
				else
				{
					static int cnt = 0;
					const JVector<Chat::Channel>& chs = chatClient.getPublicChannels();
					if(chs.getSize())
						chatClient.opPublishMessage(chs[cnt++ % chs.getSize()].getName(), ValueObject<JString>(typeBuf));
				}
			}
		}
		else
			wprintf(L" <<< ");
		typeBufPos = 0;
	}
	else
	{
		if(typeMode)
		{
			if(typeBufPos < TYPE_BUF_LEN)
			{
				typeBuf[typeBufPos++] = k;
				wprintf(L"%c", k);
			}
		}
		else
		{
			if(k == 'h')
				usage();
			if(k == 's')
				subscribe(chatClient);
			if(k == 'u')
				unsubscribe(chatClient);
			if(k == 'c')
				chatClient.connect();
			if(k == 'd')
				chatClient.disconnect();
			if(k == 'l')
				listChannels(chatClient);
			if(k >= '1' && k <= '5')
			{
				int s = k - '1';
				wprintf(L"Changing online status to %d ...\n", s);
				chatClient.opSetOnlineStatus(s, ValueObject<JString>(JString("My status changed to ") + statusNames[s]));
			}
		}
	}
	return 0;
}

static void listChannels(Chat::Client& chatClient)
{
	wprintf(L"Channels:\n");
	for(unsigned int i = 0;i < chatClient.getPublicChannels().getSize();++i)
	{
		// test
		const JString& name = chatClient.getPublicChannels()[i].getName();
		const Chat::Channel* ch = chatClient.getPublicChannel(name);
		wprintf(L"%ls: %d messages\n", ch->getName().cstr(), ch->getMessageCount());
	}
	wprintf(L"--------\n");
	wprintf(L"Private Channels:\n");
	for(unsigned int i = 0;i < chatClient.getPrivateChannels().getSize();++i)
	{
		// test
		const JString& name = chatClient.getPrivateChannels()[i].getName();
		const Chat::Channel* ch = chatClient.getPrivateChannel(name);
		wprintf(L"%ls: %d messages\n", ch->getName().cstr(), ch->getMessageCount());
	}
	wprintf(L"--------\n");
	wprintf(L"Messages:\n");
	const JVector<Chat::Channel>& chs = chatClient.getPublicChannels();
	for(unsigned int i = 0;i < chs.getSize();++i)
	{
		const Chat::Channel& ch = chs[i];
		for(unsigned int i = 0;i < ch.getMessageCount();++i)
			wprintf(L"[%ls: %ls] >>> %ls\n", ch.getName().cstr(), ch.getSenders()[i].cstr(), ch.getMessages()[i].toString().cstr());
	}
	const JVector<Chat::Channel>& pchs = chatClient.getPrivateChannels();
	for(unsigned int i = 0;i < pchs.getSize();++i)
	{
		const Chat::Channel& ch = pchs[i];
		for(unsigned int i = 0;i < ch.getMessageCount();++i)
			wprintf(L"[Private: %ls: %ls] >>> %ls\n", ch.getName().cstr(), ch.getSenders()[i].cstr(), ch.getMessages()[i].toString().cstr());
	}
	wprintf(L"--------\n");
}