using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ExitGames.Client.Photon;
using ExitGames.Client.Photon.LoadBalancing;

namespace TestPhotonApp
{
    public class MyClient : LoadBalancingClient
    {
        private readonly Thread updateThread;

        // networking / timing related settings
        internal int intervalDispatch = 50;                 // interval between DispatchIncomingCommands() calls
        internal int lastDispatch = Environment.TickCount;
        internal int intervalSend = 50;                     // interval between SendOutgoingCommands() calls
        internal int lastSend = Environment.TickCount;

        // update control variables for UI
        internal int lastUiUpdate = Environment.TickCount;
        private int intervalUiUpdate = 1000;  // the UI update interval. this demo has a low update interval, cause we update also on event and status change

        public Action OnUpdate { get; set; }

        public int ReceivedCountMeEvents { get; set; }

        public MyClient() : base()
        {
            this.updateThread = new Thread(this.UpdateLop);
            this.updateThread.IsBackground = true;
            this.updateThread.Start();

            this.PlayerName = "Player_" + (SupportClass.ThreadSafeRandom.Next()%1000);
            this.AppId = "c30c1067-8bdd-4f00-b9fb-72f07377ddc1";
            this.MasterServerAddress = "app-eu.exitgamescloud.com:5055";
            this.AppVersion = "1.0";
            this.Connect();
        }

        public void UpdateLop()
        {
            while(true)
            {
                this.Update();
                Thread.Sleep(10);
            }
        }

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
                this.loadBalancingPeer.SendOutgoingCommands();
            }


            if (Environment.TickCount - this.lastUiUpdate > this.intervalUiUpdate)
            {
                this.lastUiUpdate = Environment.TickCount;
                if (this.OnUpdate != null)
                {
                    this.OnUpdate();
                }
            }
        }

        public override void OnEvent(EventData photonEvent)
        {
            switch (photonEvent.Code)
            {
                case (byte)3: //DemoPlayer.DemoEventCode.CountMe:
                    // this event has no content. we just count it if it's sent
                    this.ReceivedCountMeEvents++;
                    break;
            }

            base.OnEvent(photonEvent);
            if (this.OnUpdate != null)
            {
                this.OnUpdate();
            }
        }
    }
}
