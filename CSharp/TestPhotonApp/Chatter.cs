using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExitGames.Client.Photon.Chat;


namespace TestPhotonApp
{
    class Chatter : IChatClientListener
    {
        public Chatter()
        {

        }

        public void OnChatStateChange(ChatState state) { }
        public void OnConnected() { }
        public void OnDisconnected() { }
        public void OnGetMessages(string channelName, string[] senders, object[] messages) { }
        public void OnPrivateMessage(string sender, object message, string channelName) { }
        public void OnStatusUpdate(string user, int status, bool gotMessage, object message) { }
        public void OnSubscribed(string[] channels, bool[] results) { }
        public void OnUnsubscribed(string[] channels) { }
    }
}
