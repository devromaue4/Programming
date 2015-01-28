
#include "ChatListener.h"

using namespace ExitGames;

void ChatListener::debugReturn(Common::DebugLevel::DebugLevel debugLevel, const Common::JString& string)
{
	wprintf(L"debugReturn: %ls\n", string.cstr());
}

void ChatListener::connectionErrorReturn(int errorCode)
{
	wprintf(L"connectionErrorReturn: %d\n", errorCode);
}

void ChatListener::clientErrorReturn(int errorCode)
{
	wprintf(L"clientErrorReturn: %d\n", errorCode);
}

void ChatListener::warningReturn(int warningCode)
{
	wprintf(L"warningReturn: %d\n", warningCode);
}

void ChatListener::serverErrorReturn(int errorCode)
{
	wprintf(L"serverErrorReturn: %d\n", errorCode);
}

void ChatListener::connectReturn(int errorCode, const Common::JString& errorString)
{
	wprintf(L"connectReturn: %d, %ls\n", errorCode, errorString.cstr());
}

void ChatListener::disconnectReturn(void)
{
}

void ChatListener::onStateChange(int state)
{
	wprintf(L"onStateChange: %d\n", state);
}

void ChatListener::onGetMessages(const Common::JString& channelName, const Common::JVector<Common::JString>& senders, const Common::JVector<Common::Object>& messages)
{
	for(unsigned int i = 0;i < senders.getSize();++i)
		wprintf(L"[%ls: %ls] >>> %ls\n", channelName.cstr(), senders[i].cstr(), messages[i].toString().cstr());
}

void ChatListener::onPrivateMessage(const Common::JString& sender, const Common::Object& message, const Common::JString& channelName)
{
	wprintf(L"[Private '%ls': %ls] >>> %ls\n", channelName.cstr(), sender.cstr(), message.toString().cstr());
}

void ChatListener::subscribeReturn(const Common::JVector<Common::JString>& channels, const Common::JVector<bool>& results)
{
	wprintf(L"subscribeReturn: %ls, %ls\n", channels.toString().cstr(), results.toString().cstr());
}

void ChatListener::unsubscribeReturn(const Common::JVector<Common::JString>& channels)
{
	wprintf(L"unsubscribeReturn: %ls\n", channels.toString().cstr());
}

void ChatListener::onStatusUpdate(const Common::JString& user, int status, bool gotMessage, const Common::Object& message)
{
	wprintf(L"onStatusUpdate: %ls: %d / %ls\n", user.cstr(), status, gotMessage ? message.toString().cstr() : L"[message skipped]");
}
