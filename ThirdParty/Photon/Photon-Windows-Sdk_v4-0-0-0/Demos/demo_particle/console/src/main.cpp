// demo_particle_console.cpp : Defines the entry point for the console application.
//

#include "../../src/DemoConstants.h"
#include "../../src/LoadBalancingListener.h"

#include "../../src/BaseView.h"

int getcharIfKbhit(void);

#ifdef _EG_WINDOWS_PLATFORM
#include <conio.h>

int getcharIfKbhit(void)
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

#if defined _EG_WINDOWS_PLATFORM
#	define SLEEP(ms) Sleep(ms)
#else
#	define SLEEP(ms) usleep(ms*1000)
#endif

bool useTcp = false;

using namespace ExitGames::LoadBalancing;
using namespace ExitGames::Common;
using namespace ExitGames::Photon;

static LoadBalancingListener* lbl = NULL;
static Client* lbc = NULL;

void info(Client* lbc);
void trafficStats(Client* lbc);

static bool sendAcksOnly = false;
static nByte sendGroup;
static bool useGroups = false;
static JVector<nByte> groups;

int main(int argc, char* argv[])
{
	BaseView* view = new BaseView();
	lbl = new LoadBalancingListener(view);
	lbc = new Client(*lbl, appId, appVersion, JString(L"NVC")+GETTIMEMS(), useTcp);
	lbc->setDebugOutputLevel(ExitGames::Common::DebugLevel::INFO);
//	lbc->setCrcEnabled(true);
	lbl->setLBC(lbc);
	lbc->setTrafficStatsEnabled(true);
//	lbc->setLimitOfUnreliableCommands(15);
	view->info( "LBC: connecting...");
	lbl->connect();
	while(true) {
		if(sendAcksOnly)
		{
			lbc->serviceBasic();
			while(lbc->dispatchIncomingCommands());
			lbc->sendAcksOnly();
		}
		else
		{
			//lbc->service();
			lbc->serviceBasic();
			while(lbc->dispatchIncomingCommands());
			while(lbc->sendOutgoingCommands());
		}
		lbl->service();

		int k = getcharIfKbhit();
		if(k == 27)
			break;
		if(k == 'd')
			lbc->disconnect();
		if(k == 'c')
			lbl->connect();
		if(k == '/')
			sendAcksOnly = !sendAcksOnly;
		if(k == 'g')
		{
			lbl->setUseGroups(false); // reset autogroups
			useGroups = !useGroups;
			JVector<nByte> empty;
			if(useGroups)
				lbc->opChangeGroups(&empty, &groups);
			else
				lbc->opChangeGroups(&empty, NULL);
		}
		if(k >= '0' && k <= '9')
		{
			lbl->setUseGroups(false); // reset autogroups
			nByte g = k - '0';
			if(useGroups)
			{
				JVector<nByte> r;
				JVector<nByte> a;
				if(groups.contains(g))
				{
					groups.removeElement(g);
					r.addElement(g);
					lbc->opChangeGroups(&r, NULL);
				}
				else
				{
					groups.addElement(g);
					a.addElement(g);
					lbc->opChangeGroups(NULL, &a);
				}
			}
			else
			{
				sendGroup = g;
				lbl->setSendGroup(g);
			}
			info(lbc);
		}

		if(k == 'i') info(lbc);
		if(k == 's') trafficStats(lbc);
		if(k == 'r')
		{
			lbc->resetTrafficStats();
			trafficStats(lbc);
		}
		if(k == 't')
		{
			lbc->setTrafficStatsEnabled(!lbc->getTrafficStatsEnabled());
			trafficStats(lbc);
		}
		int t = GETTIMEMS();
		while(GETTIMEMS() < t + 100);
	}
	delete lbc;
	delete lbl;
	return 0;
}

void info(Client* lbc)
{
	printf("getState: %d\n", lbc->getState());
	printf("getDisconnectedCause: %d\n", lbc->getDisconnectedCause());
	printf("getTimestampOfLastSocketReceive: %d\n", lbc->getTimestampOfLastSocketReceive());
	printf("getPacketLossByCrc: %d\n", lbc->getPacketLossByCrc());
	printf("useGroups: %d\n", useGroups);
	printf("sendGroup: %d\n", sendGroup);
	printf("groups:");
	for(unsigned int i=0;i<groups.getSize();++i)
		printf(" %d", groups[i]);
	printf("\n");
}

void trafficStats(Client* lbc)
{
	wprintf(L"TrafficStats: enabled %dms\n", lbc->getTrafficStatsElapsedMs());
	wprintf(L"TrafficStatsGameLevel: %s\n", lbc->getTrafficStatsGameLevel().toString().cstr());
//	wprintf(L"TrafficStatsGameLevel vital: %s\n", lbc->getTrafficStatsGameLevel().toStringVitalStats().cstr());
	wprintf(L"TrafficStats vital: %s\n", lbc->vitalStatsToString(true).cstr());

	for(int i = 0;i < 2;++i)
	{
		const TrafficStats* s;
		if(!i)
		{
			s = &lbc->getTrafficStatsOutgoing();
			wprintf(L"TrafficStatsOutgoing: ========\n");
		}
		else
		{
			s = &lbc->getTrafficStatsIncoming();
			wprintf(L"TrafficStatsIncoming: ========\n");
		}
		wprintf(L"%d\n",s->getPackageHeaderSize());
		wprintf(L"%d\n",s->getReliableCommandCount());
		wprintf(L"%d\n",s->getUnreliableCommandCount());
		wprintf(L"%d\n",s->getFragmentCommandCount());
		wprintf(L"---\n%d\n",s->getControlCommandCount());
		wprintf(L"%d\n",s->getTotalPacketCount());
		wprintf(L"%d\n",s->getTotalCommandsInPackets());
		wprintf(L"%d\n",s->getReliableCommandBytes());
		wprintf(L"---\n%d\n",s->getUnreliableCommandBytes());
		wprintf(L"%d\n",s->getFragmentCommandBytes());
		wprintf(L"%d\n",s->getControlCommandBytes());
		wprintf(L"%d\n",s->getTotalCommandCount());
		wprintf(L"---\n%d\n",s->getTotalCommandBytes());
		wprintf(L"%d\n",s->getTotalPacketBytes());
		wprintf(L"%d\n",s->getTimestampOfLastAck());
		wprintf(L"%d\n",s->getTimestampOfLastReliableCommand());
		wprintf(L"========\n");
	}
}