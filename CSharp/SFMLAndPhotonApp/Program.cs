using System;
//using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using SFML;
using SFML.Graphics;
using SFML.Window;
using ExitGames.Client.Photon;
using ExitGames.Client.Photon.LoadBalancing;


namespace SFMLAndPhotonApp
{
    class Program
    {
        private static Random rand = new Random();

        const int H = 12;
        const int W = 40;

        static PhotonClient phClient;

        static void Main(string[] args)
        {
            int g_WndWidth = 800,
                g_WndHeight = 400;
            float g_fOffsetX = 0,
                  g_fOffsetY = 0;
 
            String[] TileMap = new String[H]
            {
                "BBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBB",
                "B                               B      B",
                "B                               B      B",
                "B                               B      B",
                "B                               B      B",
                "B         0000    0000       BBBB      B",
                "B                               B      B",
                "BBB                       B     B      B",
                "B              BB               BB     B",
                "B              BB                      B",
                "B   B          BB      BB              B",
                "BBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBB",
            };

            // Copy String to StringBuilder ( String readonly )
            StringBuilder[] sb = new StringBuilder[H];
            for (int i = 0; i < H; i++)
                sb[i] = new StringBuilder(TileMap[i]);       

            // Create the main window
            RenderWindow window = new RenderWindow(new VideoMode((uint)g_WndWidth, (uint)g_WndHeight),
                "Testing SFML.Net and Photon Realtime");

            //PhotonClient 
                phClient = new PhotonClient();
            
            // Setup event handlers
            window.Closed += OnClosed;
            window.KeyPressed += OnKeyPressed;

            RectangleShape RecShape = new RectangleShape(new Vector2f(32, 32));
            //Texture tx = new Texture("..\\..\\Data\\fang.png");
            //Player player = new Player(tx);
            
            Text txt = new Text("Test Text", new Font("..\\..\\Data\\sansation.ttf"));
            txt.Color = Color.Red;
            txt.Position = new Vector2f(20, 10);
            txt.Scale = new Vector2f(0.6f, 0.6f);

            Text txtListRoom = new Text("List room:", new Font("..\\..\\Data\\sansation.ttf"));
            txtListRoom.Color = Color.Red;
            txtListRoom.Position = new Vector2f(10, 200);
            txtListRoom.Scale = new Vector2f(0.6f, 0.6f);
            
            int LastTime = Environment.TickCount;

            while (window.IsOpen())
            {
                int fTime = Environment.TickCount - LastTime;
                LastTime = Environment.TickCount;
                //fTime *= 1.2f;         

                // Process events
                window.DispatchEvents();

                //if (Keyboard.IsKeyPressed(Keyboard.Key.Left))
                //    phClient.player.dx = -0.1f;
                //if (Keyboard.IsKeyPressed(Keyboard.Key.Right))
                //    phClient.player.dx = 0.1f;
                //if (Keyboard.IsKeyPressed(Keyboard.Key.Up))
                //{
                //    if (phClient.player.bOnGround)
                //    {
                //        phClient.player.dy = -0.33f;
                //        phClient.player.bOnGround = false;
                //    }
                //}

                // Update
                //player.Update(1.5f * fTime, g_fOffsetX, g_fOffsetY, sb);

                //if (player.FRect.Left > g_WndWidth / 2)
                //    g_fOffsetX = player.FRect.Left - g_WndWidth / 2;
                //if (player.FRect.Top > g_WndHeight / 2)
                //    g_fOffsetY = player.FRect.Top - g_WndHeight / 2;

                phClient.player.Update(1.5f * fTime, g_fOffsetX, g_fOffsetY, sb);

                //if (phClient.player.FRect.Left > g_WndWidth / 2)
                //    g_fOffsetX = phClient.player.FRect.Left - g_WndWidth / 2;
                //if (phClient.player.FRect.Top > g_WndHeight / 2)
                //    g_fOffsetY = phClient.player.FRect.Top - g_WndHeight / 2;

                // Get list rooms
                txtListRoom.DisplayedString = "List room:";
                foreach (var v in phClient.RoomInfoList.Values)
                {
                    txtListRoom.DisplayedString += "\n";
                    txtListRoom.DisplayedString += v.ToString();
                }

                txt.DisplayedString = string.Format("Help:\nCreate room 'C'\nLeave room 'L'" +
                                                    "\nClient state: {0}" +
                                                    "\nNum Room: {1}", phClient.State.ToString(), phClient.RoomInfoList.Count);

                // Clear the window
                window.Clear(Color.White);
                
                for (int i = 0; i < H; i++)
                {
                    for (int j = 0; j < W; j++)
                    {
                        if (sb[i][j] == 'B')
                            RecShape.FillColor = Color.Black;
                        if (sb[i][j] == '0')
                            RecShape.FillColor = Color.Green;
                        if (sb[i][j] == ' ')
                            continue;

                        RecShape.Position = new Vector2f((float)j * 32 - g_fOffsetX, (float)i * 32 - g_fOffsetY);

                        window.Draw(RecShape);
                    }
                }

                //window.Draw(player.SSprite);
                //window.Draw(player.txtPlayerName);

                window.Draw(phClient.player.SSprite);
                window.Draw(phClient.player.txtPlayerName);

                window.Draw(txtListRoom);
                window.Draw(txt);               

                // Finally, display the rendered frame on screen
                window.Display();  
            }
        }

        static void OnKeyPressed(object sender, KeyEventArgs e)
        {
            // Escape key : exit
            if (e.Code == Keyboard.Key.Escape)
                ((RenderWindow)sender).Close();

            if (e.Code == Keyboard.Key.Right)
                phClient.player.dx = 0.1f;
            if (e.Code == Keyboard.Key.Left)
                phClient.player.dx = -0.1f;  

            if (e.Code == Keyboard.Key.C)
            {
                if (phClient.State == ClientState.JoinedLobby)
                {
                    // make up some custom properties (key is a string for those)
                    //Hashtable customGameProperties = new Hashtable() { { "map", "blue" }, { "units", 2 } };

                    // tells the master to create the room and pass on our locally set properties of "this" player
                    // the last parameter makes the custom prop "map" show up in the lobby. "units" won't be in the shown in the lobby
                    //phClient.OpCreateRoom("Room_" + rand.Next(0, 10), new RoomOptions() { MaxPlayers = 4, CustomRoomProperties = customGameProperties, CustomRoomPropertiesForLobby = new string[] { "map" } }, null);
                    //phClient.OpCreateRoom("Room_" + rand.Next(0, 10), new RoomOptions() { MaxPlayers = 4 }, null);
       
                }
            }
        }

        static void OnClosed(object sender, EventArgs e)
        {
            ((RenderWindow)sender).Close();
        }
    }
}
