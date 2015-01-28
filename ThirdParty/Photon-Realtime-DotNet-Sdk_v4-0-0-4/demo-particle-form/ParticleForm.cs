// --------------------------------------------------------------------------------------------------------------------
// <copyright company="Exit Games GmbH">
//   Exit Games GmbH, 2012
// </copyright>
// <summary>
//   Windows Forms GUI for the "Particle" Photon Demo.
//   The base logic is referenced from another project.
// </summary>
// <author>developer@exitgames.com</author>
// --------------------------------------------------------------------------------------------------------------------

namespace ExitGames.Client.DemoParticle
{
    using ExitGames.Client.Photon.LoadBalancing;
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Threading;
    using System.Windows.Forms;

    /// <summary>A Windows Forms based GUI that runs the "Particle" demo logic in a thread and displays the players and output.</summary>
    /// <remarks>
    /// In this form, we define the server to use and the Photon Cloud "AppId" (if the Cloud should be used).
    /// We also establish a "game loop" running the actual game code. This is not done in the game logic code 
    /// as different platforms have different ways to implement something that resembles a Game Loop.
    /// E.g.: Unity3d has a Update method that's called each frame. Windows Forms don't have that.
    /// 
    /// As the GameLogic is nicely encapsulated, we can run multiple clients easily. That is nice as showcase and
    /// practical for testing in one process. Use + and - to control the number of clients running. 
    /// Only one can be controlled.
    /// 
    /// Aside from that, this is just a frontend - anything this does, is more or less cosmetic.
    /// The actual logic and Photon-using code is in the "demo-particle-logic" project.
    /// 
    /// Keep in mind, this might not be the best performing code (but that's not the task of this demo).
    /// </remarks>
    public partial class ParticleForm : Form
    {
        private string appId = "c30c1067-8bdd-4f00-b9fb-72f07377ddc1";//"<your appid>";
        private string startAddress = "app-eu.exitgamescloud.com:5055";    // master server
        private string gameVersion = "1.0";

        private GameLogic gameLogic;

        private delegate void InvalidateAllDelegate();

        private InvalidateAllDelegate invalidateAll;

        private Rectangle[] backgroundRectanglesOne = new Rectangle[0];
        private Rectangle[] backgroundRectanglesTwo = new Rectangle[0];

        private bool ShowUserInfo;
        
        private string[] availableRooms;

        private Queue<GameLogic> backgroundGames = new Queue<GameLogic>();

        public ParticleForm()
        {
            InitializeComponent();
            this.invalidateAll = this.InvalidateAll;

            gameLogic = new GameLogic(this.startAddress, this.appId, this.gameVersion);
            gameLogic.Start();

            Thread thread = new Thread(this.GameLogic); // windows forms are event based. we need a game loop despite this
            thread.IsBackground = true; // a background thread automatically ends when the app ends
            thread.Start();
        }

        /// <summary>This is the main game loop of this demo, running in a background thread.</summary>
        /// <remarks>
        /// Per frame the actual game logic must be called. 
        /// We have to make sure this thread is never delayed or stops. That would lead to a disconnect server-side.
        /// </remarks>
        public void GameLogic()
        {
            while (true)
            {
                gameLogic.UpdateLoop();
                lock (this.backgroundGames)
                {
                    foreach (GameLogic game in this.backgroundGames)
                    {
                        game.UpdateLoop();
                    }
                }

                // when anything changed in the game logic, it will "flag" UpdateVisuals as true and the form is repaint
                if (gameLogic.UpdateVisuals)
                {
                    gameLogic.UpdateVisuals = false;    // we repaint now. reset this flag

                    if (this.gameLogic.State == ClientState.JoinedLobby)
                    {
                        Dictionary<string, RoomInfo> rooms = this.gameLogic.RoomInfoList;
                        availableRooms = new string[rooms.Count];
                        this.gameLogic.RoomInfoList.Keys.CopyTo(availableRooms, 0);
                    }

                    try
                    {
                        // as this is a thread, we have to udpate the form via invoke (in the GUI thread context)
                        this.Invoke(this.invalidateAll);
                    }
                    catch
                    {
                        return;
                    }
                }

                Thread.Sleep(1);
            }
        }

        /// <summary>
        /// Paints the grid and players on it to the element that's calling this method.
        /// </summary>
        /// <remarks>
        /// To speed up things, we create lists of Rectangles to be painted frame by frame. 
        /// These are refreshed when the grid changed only.
        /// Per "frame" all players are drawn, no matter if someone else is in the same rectangle.
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnDraw(object sender, PaintEventArgs e)
        {
            this.Text = gameLogic.State.ToString();
            if (this.gameLogic.LocalRoom != null)
            {
                // in room, we show the player count
                this.Text += string.Format("{0}, Players: {1}", this.Text, this.gameLogic.LocalRoom.PlayerCount);
            }

            if (this.gameLogic.State == ClientState.Joined && this.gameLogic.LocalRoom != null)
            {
                Graphics grfx = e.Graphics;
                Font font = new Font("Verdana", 8);
                SolidBrush brush = new SolidBrush(Color.Black);
                Pen pen = new Pen(this.ForeColor);

                int gridSize = this.gameLogic.LocalRoom.GridSize;

                Panel gamePanel = sender as Panel;
                if (gamePanel == null)
                {
                    return;
                }

                int w = (int)(gamePanel.Size.Width / gridSize);
                int h = (int)(gamePanel.Size.Height / gridSize);

                int rectanglesInGrid = gridSize * gridSize;

                if (backgroundRectanglesOne.Length != rectanglesInGrid / 2 || backgroundRectanglesOne[0].Width != w || backgroundRectanglesOne[0].Height != h)
                {
                    int halfGridSize = gridSize / 2;

                    backgroundRectanglesOne = new Rectangle[(gridSize * gridSize) / 2];
                    backgroundRectanglesTwo = new Rectangle[(gridSize * gridSize) / 2];
                    int groupOneIndex = 0;
                    int groupTwoIndex = 0;

                    for (int y = 0; y < gridSize; y++)
                    {
                        for (int x = 0; x < gridSize; x++)
                        {
                            Rectangle newRect = new Rectangle(x * w, y * h, w - 1, h - 1);
                            bool addGroupOne = (x / halfGridSize + y / halfGridSize) % 2 == 1;

                            if (addGroupOne)
                            {
                                backgroundRectanglesOne[groupOneIndex++] = newRect;
                            }
                            else
                            {
                                backgroundRectanglesTwo[groupTwoIndex++] = newRect;
                            }
                        }
                    }
                }

                SolidBrush brushA = new SolidBrush(Color.FromArgb(255, 255, 255));
                SolidBrush brushB = new SolidBrush(Color.FromArgb(240, 240, 240));

                SolidBrush usedBrush = brushA;
                grfx.FillRectangles(usedBrush, backgroundRectanglesOne);
                if (this.gameLogic.UseInterestGroups)
                {
                    usedBrush = brushB;
                }
                grfx.FillRectangles(usedBrush, backgroundRectanglesTwo);

                // now draw the players (all of them)
                lock (this.gameLogic.LocalRoom.Players)
                {
                    foreach (ParticlePlayer p in this.gameLogic.LocalRoom.Players.Values)
                    {
                        if (p.IsLocal)
                        {
                            // draw black rectangle around player's cube
                            grfx.FillRectangle(new SolidBrush(Color.Black), p.PosX * w - 1, p.PosY * h - 1, w + 1, h + 1);
                        }

                        byte alpha = 254;
                        if (!p.IsLocal && p.UpdateAge > 500)
                        {
                            alpha = (p.UpdateAge > 1000) ? (byte)20 : (byte)80;
                        }

                        grfx.FillRectangle(new SolidBrush(IntToColor(p.Color, alpha)), p.PosX * w, p.PosY * h, w - 1, h - 1);
                        if (this.ShowUserInfo)
                        {
                            grfx.DrawString(p.Name + " " + p.ID, font, brush, p.PosX * w - 2, p.PosY * h);
                        }
                    }
                }

                this.labelPlayerInfo.Text = this.gameLogic.LocalPlayer.ToString();
            }
        }

        /// <summary>
        /// Called in the GUI-paint thread context, this actually updates the form.
        /// </summary>
        private void InvalidateAll()
        {
            bool gameIsVisible = this.gameLogic.State != ClientState.JoinedLobby;
            this.panelGame.Visible = gameIsVisible;
            this.panelLobby.Visible = !gameIsVisible;
            
            if (this.gameLogic.State == ClientState.JoinedLobby)
            {
                this.listBoxLobby.Items.Clear();
                this.listBoxLobby.Items.AddRange(availableRooms);
            }

            if (textBoxStats.Visible)
            {
                this.textBoxStats.Text = this.gameLogic.loadBalancingPeer.VitalStatsToString(true);
                this.textBox1.Text = this.gameLogic.Log.ToString();
            }

            this.Invalidate(true);
        }

        #region Key and Button Handling

        /// <summary>
        /// Catches key events to allow arrow and w-a-s-d movement of the local player.
        /// </summary>
        /// <remarks>
        /// This directly affects the local player from the key-input thread, which could cause trouble if other 
        /// threads were accessing the values concurrently.
        /// In this case, we only modify a int and don't care if the display is incorrect (it's fixed next frame).
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnKeyPress(object sender, KeyEventArgs e)
        {
            ParticlePlayer local = this.gameLogic.LocalPlayer;
            if (local == null)
            {
                return;
            }

            e.Handled = true;
            e.SuppressKeyPress = true;
            switch (e.KeyCode)
            {
                case Keys.A:
                case Keys.Left:
                    local.PosX -= 1;
                    break;
                case Keys.D:
                case Keys.Right:
                    local.PosX += 1;
                    break;
                case Keys.W:
                case Keys.Up:
                    local.PosY -= 1;
                    break;
                case Keys.S:
                case Keys.Down:
                    local.PosY += 1;
                    break;
                case Keys.X:
                    this.gameLogic.Disconnect();
                    break;
                case Keys.C:
                    this.gameLogic.Start();
                    break;

                case Keys.Add:
                    GameLogic backgroundPlayer = new GameLogic(this.startAddress, this.appId, this.gameVersion);
                    backgroundPlayer.Start();
                    lock (this.backgroundGames)
                    {
                        backgroundGames.Enqueue(backgroundPlayer);
                    }
                    break;
                case Keys.Subtract:
                    lock (this.backgroundGames)
                    {
                        if (backgroundGames.Count > 0)
                        {
                            GameLogic removedPlayer = backgroundGames.Dequeue();
                            removedPlayer.Disconnect();
                        }
                    }
                    break;
            }

            local.ClampPosition();
            this.gameLogic.UpdateVisuals = true;
        }

        private void OnClickUserInfo(object sender, System.EventArgs e)
        {
            this.ShowUserInfo = !this.ShowUserInfo;
            this.miUserInfo.Checked = this.ShowUserInfo;
        }

        private void OnClickStats(object sender, System.EventArgs e)
        {
            this.panelStats.Visible = !this.panelStats.Visible;
            this.gameLogic.loadBalancingPeer.TrafficStatsEnabled = this.panelStats.Visible;

            this.miStatistics.Checked = this.panelStats.Visible;
            this.splitterMain.Enabled = this.panelStats.Visible;
            this.splitterMain.Visible = this.panelStats.Visible;
        }

        private void OnClickAutoMove(object sender, System.EventArgs e)
        {
            this.gameLogic.MoveInterval.IsEnabled = !this.gameLogic.MoveInterval.IsEnabled;

            if (this.gameLogic.MoveInterval.IsEnabled)
            {
                this.btnAutoMove.Image = global::ExitGames.Client.DemoParticle.Properties.Resources.CircledBorderTriangleRight;
            }
            else
            {
                this.btnAutoMove.Image = global::ExitGames.Client.DemoParticle.Properties.Resources.CircledPause;
            }
        }

        private void OnClickGridSize(object sender, EventArgs e)
        {
            this.gameLogic.ChangeGridSize();
        }

        private void OnClickChangeColor(object sender, EventArgs e)
        {
            this.gameLogic.ChangeLocalPlayercolor();
        }

        private void OnClickInterestGroup(object sender, EventArgs e)
        {
            this.gameLogic.SetUseInterestGroups(!this.gameLogic.UseInterestGroups);

            lock (this.backgroundGames)
            {
                foreach (GameLogic game in backgroundGames)
                {
                    game.SetUseInterestGroups(this.gameLogic.UseInterestGroups);
                }
            }

            if (this.gameLogic.UseInterestGroups)
            {
                this.btnUseGroups.Image = global::ExitGames.Client.DemoParticle.Properties.Resources.Grid3;
            }
            else
            {
                this.btnUseGroups.Image = global::ExitGames.Client.DemoParticle.Properties.Resources.FormRectangle;
            }
        }

        private void OnClickToLobby(object sender, EventArgs e)
        {
            this.gameLogic.JoinRandomGame = false;
            this.gameLogic.AutoJoinLobby = true;
            this.gameLogic.OpLeaveRoom();
        }

        private void OnClickCreateRoom(object sender, EventArgs e)
        {
            this.gameLogic.CreateParticleDemoRoom(DemoConstants.MapType.Sea, 8);
        }

        private void OnClickJoinSelectedRoom(object sender, EventArgs e)
        {
            string roomName = this.listBoxLobby.SelectedItem as string;
            this.gameLogic.OpJoinRoom(roomName);
        }

        private void OnClickCurrentStatisticsToLog(object sender, EventArgs e)
        {
            this.gameLogic.Log.AppendLine(this.gameLogic.loadBalancingPeer.VitalStatsToString(true));
        }

        private void OnClickStatisticsReset(object sender, EventArgs e)
        {
            this.gameLogic.loadBalancingPeer.TrafficStatsReset();
        }

        private void OnClickStatisticsToggle(object sender, EventArgs e)
        {
            this.gameLogic.loadBalancingPeer.TrafficStatsEnabled = !this.gameLogic.loadBalancingPeer.TrafficStatsEnabled;
        }

        #endregion

        #region Helpers

        private static int ColorToInt(Color c)
        {
            return (c.R << 16) + (c.G << 8) + c.B;
        }

        private static Color IntToColor(int i)
        {
            return Color.FromArgb(0xFF, (byte)(i >> 16), (byte)(i >> 8), (byte)i);
        }

        private static Color IntToColor(int i, byte alpha)
        {
            return Color.FromArgb(alpha, (byte)(i >> 16), (byte)(i >> 8), (byte)i);
        }

        #endregion
    }
}
