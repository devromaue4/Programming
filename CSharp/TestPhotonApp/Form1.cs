using System;
using System.Windows.Forms;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExitGames.Client.Photon;
using ExitGames.Client.Photon.LoadBalancing;

namespace TestPhotonApp
{
    public class Form1 : Form
    {
        #region Fields
        private ListBox listRooms;
        private ListBox listPlayers;
        private Button btnCreateRoom;
        private Label label1;
        private Label label2;

        private Random rand = new Random();
        private Label label3;
        private Label labelNameConectedRoom;
        private Button btnLeaveRoom;
        private Label lobby_room;
        private Label label4;
        private Label CurrentState;
        private Label label5;
        private Label PlayerCount;
        private Label label6;
        private Label PropsPlayer;
        private Label label7;
        private Label labelEventReceived;
        private Button btnSendEvent;
    
        delegate void UpdateViewDelegate();
        #endregion

        //static 
            MyClient Client = new MyClient();

        public Form1()
        {
            this.InitializeComponent();
        }

        private void OnLoad(object sender, EventArgs e)
        {
            this.Text = "photon test";
            Client.OnUpdate = this.UpdateView;
        }
        
        private void UpdateView()
        {
            if (this.listRooms.InvokeRequired)
            {
                UpdateViewDelegate d = new UpdateViewDelegate(this.UpdateView);
                this.Invoke(d);
            }
            else
            {
                btnCreateRoom.Enabled = true;
                btnLeaveRoom.Enabled = false;
                btnSendEvent.Enabled = false;

                this.lobby_room.Text = "In lobby";

                //this.Text = Client.State.ToString();
                this.CurrentState.Text = Client.State.ToString();

                this.labelNameConectedRoom.Text = "...";
                this.PlayerCount.Text = "0";
                this.PropsPlayer.Text = "non prop";

                this.listRooms.Items.Clear();
                foreach (var v in Client.RoomInfoList.Values)
                {
                    this.listRooms.Items.Add(v);
                }

                this.label3.Text = Client.RoomInfoList.Values.Count.ToString();

                this.listPlayers.Items.Clear();

                // display the room's properties first
                if (Client != null && Client.CurrentRoom != null)
                {
                    btnCreateRoom.Enabled = false;
                    btnLeaveRoom.Enabled = true;
                    btnSendEvent.Enabled = true;

                    this.lobby_room.Text = "In room";

                    // display room's name, player count, event count (something this demo's DemoClient does) and properties
                    this.labelNameConectedRoom.Text = Client.CurrentRoom.Name;
                    this.PlayerCount.Text = Client.CurrentRoom.PlayerCount.ToString();

                    this.PropsPlayer.Text = SupportClass.DictionaryToString(Client.CurrentRoom.CustomProperties);
                    this.labelEventReceived.Text = Client.ReceivedCountMeEvents.ToString();

                    // list all players (by keys, which are most likely sorted)
                    foreach (int playerId in Client.CurrentRoom.Players.Keys)
                    {
                        this.listPlayers.Items.Add(Client.CurrentRoom.Players[playerId]);
                    }
                }
            }
        }
    
        private void InitializeComponent()
        {
            this.btnCreateRoom = new System.Windows.Forms.Button();
            this.listRooms = new System.Windows.Forms.ListBox();
            this.listPlayers = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.labelNameConectedRoom = new System.Windows.Forms.Label();
            this.btnLeaveRoom = new System.Windows.Forms.Button();
            this.lobby_room = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.CurrentState = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.PlayerCount = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.PropsPlayer = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.labelEventReceived = new System.Windows.Forms.Label();
            this.btnSendEvent = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnCreateRoom
            // 
            this.btnCreateRoom.Location = new System.Drawing.Point(348, 12);
            this.btnCreateRoom.Name = "btnCreateRoom";
            this.btnCreateRoom.Size = new System.Drawing.Size(75, 23);
            this.btnCreateRoom.TabIndex = 0;
            this.btnCreateRoom.Text = "CreateRoom";
            this.btnCreateRoom.UseVisualStyleBackColor = true;
            this.btnCreateRoom.Click += new System.EventHandler(this.OnCreateRoom);
            // 
            // listRooms
            // 
            this.listRooms.FormattingEnabled = true;
            this.listRooms.Location = new System.Drawing.Point(12, 76);
            this.listRooms.Name = "listRooms";
            this.listRooms.Size = new System.Drawing.Size(298, 238);
            this.listRooms.TabIndex = 3;
            this.listRooms.DoubleClick += new System.EventHandler(this.OnSelectRoomToJoin);
            // 
            // listPlayers
            // 
            this.listPlayers.FormattingEnabled = true;
            this.listPlayers.Location = new System.Drawing.Point(345, 154);
            this.listPlayers.Name = "listPlayers";
            this.listPlayers.Size = new System.Drawing.Size(298, 160);
            this.listPlayers.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 57);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "List Rooms:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(345, 57);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(97, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Connected to room";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(77, 57);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(13, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "0";
            // 
            // labelNameConectedRoom
            // 
            this.labelNameConectedRoom.AutoSize = true;
            this.labelNameConectedRoom.Location = new System.Drawing.Point(449, 57);
            this.labelNameConectedRoom.Name = "labelNameConectedRoom";
            this.labelNameConectedRoom.Size = new System.Drawing.Size(16, 13);
            this.labelNameConectedRoom.TabIndex = 8;
            this.labelNameConectedRoom.Text = "...";
            // 
            // btnLeaveRoom
            // 
            this.btnLeaveRoom.Enabled = false;
            this.btnLeaveRoom.Location = new System.Drawing.Point(429, 12);
            this.btnLeaveRoom.Name = "btnLeaveRoom";
            this.btnLeaveRoom.Size = new System.Drawing.Size(75, 23);
            this.btnLeaveRoom.TabIndex = 9;
            this.btnLeaveRoom.Text = "Leave room";
            this.btnLeaveRoom.UseVisualStyleBackColor = true;
            this.btnLeaveRoom.Click += new System.EventHandler(this.OnLeaveRoom);
            // 
            // lobby_room
            // 
            this.lobby_room.AutoSize = true;
            this.lobby_room.Location = new System.Drawing.Point(9, 33);
            this.lobby_room.Name = "lobby_room";
            this.lobby_room.Size = new System.Drawing.Size(44, 13);
            this.lobby_room.TabIndex = 10;
            this.lobby_room.Text = "In lobby";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(98, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "Current client state:";
            // 
            // CurrentState
            // 
            this.CurrentState.AutoSize = true;
            this.CurrentState.Location = new System.Drawing.Point(113, 9);
            this.CurrentState.Name = "CurrentState";
            this.CurrentState.Size = new System.Drawing.Size(45, 13);
            this.CurrentState.TabIndex = 12;
            this.CurrentState.Text = "no state";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(345, 80);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(44, 13);
            this.label5.TabIndex = 13;
            this.label5.Text = "Players:";
            // 
            // PlayerCount
            // 
            this.PlayerCount.AutoSize = true;
            this.PlayerCount.Location = new System.Drawing.Point(449, 80);
            this.PlayerCount.Name = "PlayerCount";
            this.PlayerCount.Size = new System.Drawing.Size(13, 13);
            this.PlayerCount.TabIndex = 14;
            this.PlayerCount.Text = "0";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(345, 106);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(37, 13);
            this.label6.TabIndex = 15;
            this.label6.Text = "Props:";
            // 
            // PropsPlayer
            // 
            this.PropsPlayer.AutoSize = true;
            this.PropsPlayer.Location = new System.Drawing.Point(449, 106);
            this.PropsPlayer.Name = "PropsPlayer";
            this.PropsPlayer.Size = new System.Drawing.Size(49, 13);
            this.PropsPlayer.TabIndex = 16;
            this.PropsPlayer.Text = "non prop";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(345, 132);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(82, 13);
            this.label7.TabIndex = 17;
            this.label7.Text = "Event received:";
            // 
            // labelEventReceived
            // 
            this.labelEventReceived.AutoSize = true;
            this.labelEventReceived.Location = new System.Drawing.Point(449, 132);
            this.labelEventReceived.Name = "labelEventReceived";
            this.labelEventReceived.Size = new System.Drawing.Size(13, 13);
            this.labelEventReceived.TabIndex = 18;
            this.labelEventReceived.Text = "0";
            // 
            // btnSendEvent
            // 
            this.btnSendEvent.Enabled = false;
            this.btnSendEvent.Location = new System.Drawing.Point(510, 12);
            this.btnSendEvent.Name = "btnSendEvent";
            this.btnSendEvent.Size = new System.Drawing.Size(75, 23);
            this.btnSendEvent.TabIndex = 19;
            this.btnSendEvent.Text = "Event";
            this.btnSendEvent.UseVisualStyleBackColor = true;
            this.btnSendEvent.Click += new System.EventHandler(this.OnSendEvent);
            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(658, 326);
            this.Controls.Add(this.btnSendEvent);
            this.Controls.Add(this.labelEventReceived);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.PropsPlayer);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.PlayerCount);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.CurrentState);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lobby_room);
            this.Controls.Add(this.btnLeaveRoom);
            this.Controls.Add(this.labelNameConectedRoom);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.listPlayers);
            this.Controls.Add(this.listRooms);
            this.Controls.Add(this.btnCreateRoom);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Hello";
            this.Load += new System.EventHandler(this.OnLoad);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void OnCreateRoom(object sender, EventArgs e)
        {
            if (Client.State == ClientState.JoinedLobby)
            {
                // make up some custom properties (key is a string for those)
                Hashtable customGameProperties = new Hashtable() { { "map", "blue" }, { "units", 2 } };

                // tells the master to create the room and pass on our locally set properties of "this" player
                // the last parameter makes the custom prop "map" show up in the lobby. "units" won't be in the shown in the lobby
                Client.OpCreateRoom("Room_" + rand.Next(0, 10), new RoomOptions() { MaxPlayers = 4, CustomRoomProperties = customGameProperties, CustomRoomPropertiesForLobby = new string[] { "map" } }, null);
            }
        }

        private void OnLeaveRoom(object sender, EventArgs e)
        {
            Client.OpLeaveRoom();
        }

        private void OnSelectRoomToJoin(object sender, EventArgs e)
        {
            if (Client.State == ClientState.JoinedLobby)
            {
                RoomInfo selected = this.listRooms.SelectedItem as RoomInfo;
                if (selected == null)
                {
                    return;
                }

                Client.OpJoinRoom(selected.Name);
            }
        }

        private void OnSendEvent(object sender, EventArgs e)
        {
            // to send an event, "raise" it. apply any code (here 3) and set any content (or even null)
            Hashtable eventContent = new Hashtable();
            eventContent[(byte)10] = "my data";                     // using bytes as event keys is most efficient

            Client.loadBalancingPeer.OpRaiseEvent((byte)3, eventContent, false, null); //DemoPlayer.DemoEventCode.CountMe, eventContent, false, null);    // this is received by OnEvent()
        }
    }
}
