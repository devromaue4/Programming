using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML;
using SFML.Graphics;
using SFML.Window;

namespace TestSFML
{
    class Player
    {
        public Sprite    SSprite;
        FloatRect FRect;

        bool bOnGround;

        float dx, dy;
        float fCurrentFrame;

        public Player(Texture tx)
        {
            SSprite = new Sprite(tx);
            FRect = new FloatRect(0, 0, 40, 50);
		    dx = dy = 0.0f;
		    fCurrentFrame = 0.0f;

		    // pos
		    FRect.Left = 100;
		    FRect.Top = 50;// 100;

		    //dx = 0.01f; // for init sprite
		    //dy = 0.0f;
        }

        public void Update(float fTime)
        {
            fCurrentFrame += 0.005f;// *fTime;
            if (fCurrentFrame > 6)
                fCurrentFrame -= 6;

            SSprite.TextureRect = new IntRect((int)40 * (int)fCurrentFrame, 244, 40, 50);
            SSprite.Position = new Vector2f(FRect.Left, FRect.Top); 
            
            // c++ code
            //SSprite.TextureRect(sf::IntRect(40 * int(fCurrentFrame) + 40, 244, -40, 50));     
            //SSprite.setPosition(FRect.left - g_fOffsetX, FRect.top - g_fOffsetY);

        }

    }


    class Program
    {
        static void Main(string[] args)
        {
            // Create the main window
            RenderWindow window = new RenderWindow(new VideoMode(800, 600), "Testing SFML.Net");
            //window.SetVerticalSyncEnabled(true);

            Texture tx = new Texture("..\\..\\Data\\fang.png");

            Player player = new Player(tx);

            // Setup event handlers
            window.Closed += new EventHandler(OnClosed);
            //window.KeyPressed

            while (window.IsOpen())
            {
                // Process events
                window.DispatchEvents();

                player.Update(0);

                // Clear the window
                window.Clear(new Color(255, 128, 0));

                window.Draw(player.SSprite);

                // Finally, display the rendered frame on screen
                window.Display();
            }
        }

        static void OnClosed(object sender, EventArgs e)
        {
            RenderWindow window = (RenderWindow)sender;
            window.Close();
        }
    }
}
