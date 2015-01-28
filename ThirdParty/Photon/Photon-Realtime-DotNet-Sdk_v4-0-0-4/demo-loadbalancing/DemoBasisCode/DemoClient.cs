// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DemoClient.cs" company="Exit Games GmbH">
//   Copyright (c) Exit Games GmbH.  All rights reserved.
// </copyright>
// <summary>
//   The DemoClient class wraps up usage of a LoadBalancingClient, event handling and simple game logic. To make it work,
//   it must be integrated in a "UpdateLoop", which regularly calls Update().
//   A PhotonPeer, which is the basis of the underlying LoadBalancingPeer is not thread safe, so make sure Update() 
//   is only called by one thread.
//
//   This sample should show how to get a player's position across to other players in the same room.
//   Each running instance will connect to PhotonCloud (with a local player / Peer), go into the same room and
//   move around.
//   Players have positions (updated regularly), name and color (updated only when someone joins).
//   This class encapsulates the (simple!) logic for the Realtime Demo run in der PhotonCloud via LoadBalancing. It can be used on several
//   DotNet platforms (DotNet, Unity3D and Silverlight).
// </summary>
// <author>developer@exitgames.com</author>
// --------------------------------------------------------------------------------------------------------------------
namespace ExitGames.Client.DemoBasisCode
{
    using System;
    using System.Collections;
    using System.Threading;
    using ExitGames.Client.Photon;
    using ExitGames.Client.Photon.LoadBalancing;

    /// <summary>
    /// The DemoClient class wraps up usage of a LoadBalancingClient, event handling and simple game logic.
    /// </summary>
    public class DemoClient : LoadBalancingClient
    {
        #region Fields
        private readonly Thread updateThread;

        // networking / timing related settings
        internal int intervalDispatch = 50;                 // interval between DispatchIncomingCommands() calls
        internal int lastDispatch = Environment.TickCount;
        internal int intervalSend = 50;                     // interval between SendOutgoingCommands() calls
        internal int lastSend = Environment.TickCount;
        internal int intervalMove = 500;                    // interval for auto-movement - each movement creates an OpRaiseEvent
        internal int lastMove = Environment.TickCount;
        
        // update control variables for UI
        internal int lastUiUpdate = Environment.TickCount;
        private int intervalUiUpdate = 1000;  // the UI update interval. this demo has a low update interval, cause we update also on event and status change
        public Action OnUpdate { get; set; }

        public int ReceivedCountMeEvents { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="DemoClient"/> class. 
        /// </summary>
        public DemoClient(bool createGameLoopThread) : base()
        {
            if (createGameLoopThread)
            {
                this.updateThread = new Thread(this.UpdateLoop);
                this.updateThread.IsBackground = true;
                this.updateThread.Start();
            }

            this.PlayerName = "Player_" + (SupportClass.ThreadSafeRandom.Next() % 1000);
            this.LocalPlayer.SetCustomProperties(new Hashtable() { { "class", "tank" + (SupportClass.ThreadSafeRandom.Next() % 99) } });

            // insert your game's AppID (replace <InsertYourAppIdHere>). hosting yourself: use any name. using Photon Cloud: use your cloud subscription's appID
            this.AppId = "c30c1067-8bdd-4f00-b9fb-72f07377ddc1"; //"<InsertYourAppIdHere>";
            this.MasterServerAddress = "app-eu.exitgamescloud.com:5055";
            this.AppVersion = "1.0";
            this.Connect(); // this demo sets some important values before calling Connect(). Alternatively it could call the overload with several parameters
        }

        /// <summary>
        /// Overriding CreatePlayer from LoadBalancingClient to return the DemoPlayer which holds 
        /// the needed position and color data. 
        /// </summary>
        protected override Player CreatePlayer(string actorName, int actorNumber, bool isLocal, Hashtable actorProperties)
        {
            DemoPlayer tmpPlayer = null;
            if (this.CurrentRoom != null)
            {
                tmpPlayer = (DemoPlayer)this.CurrentRoom.GetPlayer(actorNumber);
            }

            if (tmpPlayer == null)
            {
                tmpPlayer = new DemoPlayer(actorName, actorNumber, isLocal);
                tmpPlayer.CacheProperties(actorProperties);

                if (this.CurrentRoom != null)
                {
                    this.CurrentRoom.StorePlayer(tmpPlayer);
                }
            }
            else
            {
                this.DebugReturn(DebugLevel.ERROR, "Player already listed: " + actorNumber);
            }

            return tmpPlayer;
        }

        #endregion

        #region Send data & update

        /// <summary>Endless loop to be run by background thread (ends when app exits).</summary>
        public void UpdateLoop()
        {
            while (true)
            {
                this.Update();
                Thread.Sleep(10);
            }
        }

        /// <summary>
        /// Update must be called by a gameloop (a single thread), so it can handle
        /// automatic movement and networking.
        /// </summary>
        public virtual void Update()
        {
            if (Environment.TickCount - this.lastDispatch > this.intervalDispatch)
            {
                this.lastDispatch = Environment.TickCount;
                this.loadBalancingPeer.DispatchIncomingCommands();
            }

            if (Environment.TickCount - this.lastSend > this.intervalSend)
            {
                this.lastSend = Environment.TickCount;
                this.loadBalancingPeer.SendOutgoingCommands(); // will send pending, outgoing commands
            }

            if (Environment.TickCount - this.lastMove > this.intervalMove)
            {
                this.lastMove = Environment.TickCount;
                if (this.State == ClientState.Joined)
                {
                    ((DemoPlayer)LocalPlayer).MoveRandom();
                    this.SendPosition();
                }
            }

            // Update call for windows phone UI-Thread
            if (Environment.TickCount - this.lastUiUpdate > this.intervalUiUpdate)
            {
                this.lastUiUpdate = Environment.TickCount;
                if (this.OnUpdate != null)
                {
                    this.OnUpdate();
                }
            }
        }

        /// <summary>
        /// Send an event to all other players telling them you just have attacked 
        /// and their health must be decreased by one. 
        /// </summary>
        public void SendPosition()
        {
            // dont move if player does not have a number or peer is not connected
            if (this.LocalPlayer == null || this.LocalPlayer.ID == 0)
            {
                return;
            }

            ((DemoPlayer)this.LocalPlayer).SendEvMove(this.loadBalancingPeer);
        }

        /// <summary>
        /// Will create and queue the operation OpRaiseEvent with local player's color and name (not position).
        /// Actually sent by a call to SendOutgoingCommands().
        /// At this point, we could also use properties (so we don't have to re-send this data when someone joins).
        /// </summary>
        public void SendPlayerInfo()
        {
            // dont move if player does not have a number or Peer is not connected
            if (this.LocalPlayer == null || this.LocalPlayer.ID == 0)
            {
                return;
            }

            ((DemoPlayer)this.LocalPlayer).SendPlayerInfo(this.loadBalancingPeer);
        }

        #endregion

        #region Event handling

        /// <summary>
        /// Photon library callback for state changes (connect, disconnect, etc.)
        /// Processed within PhotonPeer.DispatchIncomingCommands()!
        /// </summary>
        public override void OnStatusChanged(StatusCode statusCode)
        {
            base.OnStatusChanged(statusCode);

            if (this.OnUpdate != null)
            {
                this.OnUpdate();
            }
        }

        /// <summary>
        /// Called by Photon lib for each incoming event (player- and position-data in this demo, as well as joins and leaves).
        /// Processed within PhotonPeer.DispatchIncomingCommands()!
        /// </summary>
        public override void OnEvent(EventData photonEvent)
        {
            switch (photonEvent.Code)
            {
                case (byte)DemoPlayer.DemoEventCode.PlayerInfo:

                    // get the player that raised this event
                    int actorNr = (int)photonEvent[ParameterCode.ActorNr];
                    DemoPlayer p;
                    p = (DemoPlayer)this.CurrentRoom.GetPlayer(actorNr);

                    // this is a custom event, which is defined by this application.
                    // if player is known (and it should be known!), update info
                    if (p != null)
                    {
                        p.SetInfo((Hashtable)photonEvent[(byte)ParameterCode.CustomEventContent]);
                    }
                    else
                    {
                        this.DebugReturn("did not find player to set info: " + actorNr);
                    }

                    break;

                case (byte)DemoPlayer.DemoEventCode.PlayerMove:

                    // get the player that raised this event
                    actorNr = (int)photonEvent[ParameterCode.ActorNr];
                    p = (DemoPlayer)this.CurrentRoom.GetPlayer(actorNr);

                    // this is a custom event, which is defined by this application.
                    // if player is known (and it should be known) update position)
                    if (p != null)
                    {
                        p.SetPosition((Hashtable)photonEvent[(byte)ParameterCode.CustomEventContent]);
                    }
                    else
                    {
                        this.DebugReturn("did not find player to move: " + actorNr);
                    }

                    break;

                case (byte)DemoPlayer.DemoEventCode.CountMe:
                    // this event has no content. we just count it if it's sent
                    this.ReceivedCountMeEvents++;
                    break;

                case EventCode.Join:

                    // the new peer does not have our info, so send it again);
                    // everything else is handeled by the base-class
                    ((DemoPlayer)LocalPlayer).SendPlayerInfo(this.loadBalancingPeer);
                    break;
            }

            base.OnEvent(photonEvent);
            if (this.OnUpdate != null)
            {
                this.OnUpdate();
            }
        }

        #endregion

        #region Helper methods

        /// <summary>
        /// Write to console for debugging output. 
        /// </summary>
        private void DebugReturn(string debugStr)
        {
            Console.Out.WriteLine(debugStr);
        } 

        #endregion
    }
}
