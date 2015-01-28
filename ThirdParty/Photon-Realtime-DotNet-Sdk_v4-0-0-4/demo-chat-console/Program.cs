namespace ExitGames.Client.Photon.Chat.Demo
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Linq;
    using ExitGames.Client.Photon.Chat;

    internal class ConsoleCommand
    {
        public string Name { get; set; }
        public string Note { get; set; }
        public Action Handler { get; set; }
        public HashSet<ChatState> PreconditionStates { get; set; }
    }

    class Program
    {
        private static bool isDone = false;
        private static Chatter chatter;

        static void Main(string[] args)
        {
            chatter = new Chatter();
            while (!isDone)
            {
                isDone = chatter.UpdateGame();
                Thread.Sleep(50);
            }
        }
    }

    class Chatter : IChatClientListener
    {
        private ChatClient chatClient;
        private List<string> friends;
        protected string UserName { get; set; }
        private const string UserNamePrefix = "u";

        readonly char[] separator = new[] { ':' };

        private bool done = false;
        string commandKey = string.Empty;
        string keybuffer = string.Empty;
        string data = string.Empty;
        Dictionary<string, ConsoleCommand> commandCollection = new Dictionary<string, ConsoleCommand>();

        private string playerNr = (System.Diagnostics.Process.GetProcessesByName(
            System.IO.Path.GetFileNameWithoutExtension(System.Reflection.Assembly.GetEntryAssembly().Location)).Count()).ToString();

        public Chatter()
        {
            this.GetPlayerNr();
            SetupCommands();

            this.UserName = UserNamePrefix + this.playerNr;  //made-up username
            Console.WriteLine("\n\nYou are: " + UserName);
            Console.WriteLine("Press 'h' for a list of commands.");

            chatClient = new ChatClient(this);
            string appId = //"ee57cad7-42d3-49b2-a615-2b3cd55c382f"; 
             "c30c1067-8bdd-4f00-b9fb-72f07377ddc1"; //"<your appid>";
            chatClient.Connect(appId, "1.0", this.UserName, null);
        }

        private void GetPlayerNr()
        {
            Console.WriteLine("Type a number and Enter.\nNo input makes you user: '{0}{1}'.", UserNamePrefix, this.playerNr);
            var input = Console.ReadLine();
            if (!string.IsNullOrEmpty(input))
            {
                this.playerNr = input;
            }
        }


        private void SetupCommands()
        {
            commandCollection.Add(
                "h",
                new ConsoleCommand
                {
                    Name = "Help",
                    PreconditionStates = new HashSet<ChatState> { },
                    Handler = () =>
                    {
                        foreach (var cmd in commandCollection)
                        {
                            Console.WriteLine("{0}\t| {1} {2}", cmd.Key, cmd.Value.Name, cmd.Value.Note);
                        }
                        Console.WriteLine("Input a command as single line.\nTokens are separated by ':'.\nAs example \"o:2:Im happy.\"");
                    }
                });
            commandCollection.Add(
                "q",
                new ConsoleCommand
                {
                    Name = "Quit",
                    PreconditionStates = new HashSet<ChatState> { },
                    Handler = () =>
                    {
                        this.chatClient.Disconnect();
                        Thread.Sleep(1000);
                        done = true;
                    }
                });

            commandCollection.Add(
                "o",
                new ConsoleCommand
                {
                    Name = "Set Online Status",
                    Note = ": status as int: msg as string",
                    PreconditionStates = new HashSet<ChatState> { },
                    Handler = () =>
                    {
                        if (data.Length > 0)
                        {
                            if (data.Contains(":"))
                            {
                                var data2 = data.Split(separator, 2);
                                var status = 0;
                                if(int.TryParse(data2[0], out status))
                                {
                                    this.chatClient.SetOnlineStatus(status, data2[1]);
                                }
                            }
                            else
                            {
                                Console.WriteLine("missing input");
                            }
                        }
                    }
                });
            commandCollection.Add(
                "fa",
                new ConsoleCommand
                {
                    Name = "Add Friends",
                    Note = ": friend as string: friend as string: ...",
                    PreconditionStates = new HashSet<ChatState> { },
                    Handler = () =>
                    {
                        if (data.Length > 0)
                        {
                            if (data.Contains(":"))
                            {
                                var data2 = data.Split(separator);
                                chatClient.AddFriends(data2);
                            }
                            else
                            {
                                Console.WriteLine("missing input");
                            }
                        }
                    }
                });
            commandCollection.Add(
                "fr",
                new ConsoleCommand
                {
                    Name = "Remove Friends",
                    Note = ": friend as string: friend as string: ...",
                    PreconditionStates = new HashSet<ChatState> { },
                    Handler = () =>
                    {
                        if (data.Length > 0)
                        {
                            if (data.Contains(":"))
                            {
                                var data2 = data.Split(separator);
                                chatClient.RemoveFriends(data2);
                            }
                            else
                            {
                                Console.WriteLine("missing input");
                            }
                        }
                    }
                });
            commandCollection.Add(
                "sc",
                new ConsoleCommand
                {
                    Name = "Subscribe",
                    Note = ": channelname as string",
                    PreconditionStates = new HashSet<ChatState> { },
                    Handler = () =>
                    {
                        if (data.Length > 0)
                        {
                            if (data.Contains(":"))
                            {
                                var data2 = data.Split(separator);
                                this.chatClient.Subscribe(data2, 10);
                            }
                            else
                            {
                                this.chatClient.Subscribe(new[] { data }, 10);
                            }
                        }
                    }
                });
            commandCollection.Add(
                "usc",
                new ConsoleCommand
                {
                    Name = "Unsubscribe",
                    Note = ": channelname as string",
                    PreconditionStates = new HashSet<ChatState> { },
                    Handler = () =>
                    {
                        if (data.Length > 0)
                        {
                            if (data.Contains(":"))
                            {
                                var data2 = data.Split(separator);
                                this.chatClient.Unsubscribe(data2);
                            }
                            else
                            {
                                this.chatClient.Unsubscribe(new[] { data });
                            }
                        }
                    }
                });
            commandCollection.Add(
                "pm",
                new ConsoleCommand
                {
                    Name = "Publish Message",
                    Note = ": channelname as string: msg as string",
                    PreconditionStates = new HashSet<ChatState> { },
                    Handler = () =>
                    {
                        if (data.Length > 0)
                        {
                            if (data.Contains(":"))
                            {
                                var data2 = data.Split(separator, 2);
                                this.chatClient.PublishMessage(data2[0], data2[1]);
                            }
                            else
                            {
                                Console.WriteLine("missing input");
                            }
                        }
                    }
                });
            commandCollection.Add(
                "sp",
                new ConsoleCommand
                {
                    Name = "Send Private Message",
                    Note = ": whom as string: msg as string",
                    PreconditionStates = new HashSet<ChatState> { },
                    Handler = () =>
                    {
                        if (data.Length > 0)
                        {
                            if (data.Contains(":"))
                            {
                                var data2 = data.Split(separator, 2);
                                this.chatClient.SendPrivateMessage(data2[0], data2[1]);
                            }
                            else
                            {
                                Console.WriteLine("missing input");
                            }
                        }
                    }
                });
        }

        public bool UpdateGame()
        {
            while(Console.KeyAvailable)
            {
                var key = Console.ReadKey();
                if (Char.IsLetterOrDigit(key.KeyChar) || key.KeyChar == ':' || key.Key == ConsoleKey.Enter)
                {
                    if (key.Key == ConsoleKey.Enter)
                    {
                        var line = keybuffer.Split(separator, 2);
                        if (line.Length > 0)
                        {
                            commandKey = line[0];
                            if (line.Length > 1)
                            {
                                data = line[1];
                            }
                        }

                        keybuffer = string.Empty;
                        break;
                    }
                    keybuffer += key.KeyChar;
                }
            }
            if (!string.IsNullOrEmpty(commandKey))
            {
                if (commandCollection.ContainsKey(commandKey))
                {
                    var cmd = commandCollection[commandKey];

                    Console.WriteLine(">>> {0}:{1}", cmd.Name, data);
                    if (cmd.PreconditionStates.Count == 0 || (cmd.PreconditionStates.Count > 0 && cmd.PreconditionStates.Contains(chatClient.State)))
                    {
                        cmd.Handler();
                    }
                    else
                    {
                        Console.Write("'{0}' - only allowed in state(s):", cmd.Name);
                        foreach (var state in cmd.PreconditionStates)
                        {
                            Console.Write(" {0}", state);
                        }
                        Console.WriteLine();
                    }

                }
                else
                {
                    Console.WriteLine(">>Unknown command={0} (data={1})", commandKey, data);
                }
            }

            commandKey = string.Empty;
            data = string.Empty;
            this.chatClient.Service();
            return done;
        }

        public void OnDisconnected()
        {
        }

        public void OnConnected()
        {
            Console.WriteLine("OnConnected");
        }

        public void OnChatStateChange(ChatState state)
        {
            // use OnConnected() and OnDisconnected()
            // this method might become more useful in the future, when more complex states are being used.

            if (state == ChatState.ConnectedToNameServer)
                Console.WriteLine("{0}: {1}", state, chatClient.NameServerAddress);
            else if (state == ChatState.ConnectedToFrontEnd)
                Console.WriteLine("{0}: {1}", state, chatClient.FrontendAddress);
            else
                Console.WriteLine(state);
        }

        public void OnGetMessages(string channelName, string[] senders, object[] messages)
        {
            string msgs = "";
            for (int i = 0; i < senders.Length; i++)
            {
                msgs += senders[i] + ":'" + messages[i] + "' | ";
            }
            Console.WriteLine("OnGetMessages - Channel=" + channelName + " #msgs="+senders.Length+" : "+ msgs);
        }

        public void OnPrivateMessage(string sender, object message, string channelName)
        {
            Console.WriteLine("OnPrivateMessage - Channel=" + channelName + " msg='" + message+ "' sender=" + sender);
        }

        public void OnSubscribed(string[] channels, bool[] results)
        {
            for (var i = 0; i < channels.Length; i++)
            {
                Console.WriteLine("OnSubscribed: {0} - {1}", channels[i], results[i]);
            }
        }

        public void OnUnsubscribed(string[] channels)
        {
            foreach (string channel in channels)
            {
                Console.WriteLine("Unsubscribed:" + channel);
            }
        }

        public void OnStatusUpdate(string user, int status, bool hasMessage, object message)
        {
            Console.WriteLine("Status change for: " + user + " to: " + status + ((hasMessage) ? " '" + (message==null?"null":message) + "'" : " <noMsg>"));
        }
    }
}