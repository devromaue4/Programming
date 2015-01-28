using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML;
using SFML.Graphics;
using SFML.Window;
using ExitGames.Client.Photon;

namespace SFMLAndPhotonApp
{
    class Player
    {
        #region Fields and Constructor
        public Sprite SSprite;
        public FloatRect FRect;
        public Text txtPlayerName;
        public bool bOnGround;
        public float dx, dy;    
        float fCurrentFrame;

        public Player(Texture tx)
        {
            SSprite = new Sprite(tx);
            FRect = new FloatRect(0, 0, 40, 50);
            bOnGround = true;
            dx = dy = 0.0f;
            fCurrentFrame = 0.0f;

            // pos
            FRect.Left = (float)(SupportClass.ThreadSafeRandom.Next() % 700); //100;
            FRect.Top = 50;

            txtPlayerName = new Text("Player_0", new Font("..\\..\\Data\\sansation.ttf"));
            txtPlayerName.Position = new Vector2f(FRect.Left, FRect.Top);
            txtPlayerName.Scale = new Vector2f(0.6f, 0.6f);
            txtPlayerName.Color = Color.Blue;

           // dx = 500;// (float)(SupportClass.ThreadSafeRandom.Next() % 1000);
            dx = 0.01f; // for init sprite
            //dy = 0.0f;
        }

        public Player() : this(new Texture("..\\..\\Data\\fang.png")) { }
        #endregion

        public void Update(float fTime, float fOffsetX, float fOffsetY, StringBuilder[] tileMap)
         {
             FRect.Left += dx * fTime;

             // x
             //Collision(0, tileMap);

             if (!bOnGround)
                 dy = dy + 0.0005f * fTime;

             FRect.Top += dy * fTime;

             //bOnGround = false;

             // y
             //Collision(1, tileMap);

             fCurrentFrame += 0.005f * fTime;
             if (fCurrentFrame > 6)
                 fCurrentFrame -= 6;

             if (dx > 0)
                 SSprite.TextureRect = new IntRect(40 * (int)fCurrentFrame, 244, 40, 50);
             if (dx < 0)
                 SSprite.TextureRect = new IntRect(40 * (int)fCurrentFrame + 40, 244, -40, 50);

             float xp = FRect.Left;// -fOffsetX;
             float yp = FRect.Top;// -fOffsetY;
             SSprite.Position = new Vector2f(xp, yp);
             txtPlayerName.Position = new Vector2f(xp - 10.0f, yp - 22.0f);

             dx = 0;
         }

         void Collision(int dir, StringBuilder[] tileMap)
         {
             for (int i = (int)FRect.Top / 32; i < (FRect.Top + FRect.Height) / 32; i++)
                 for (int j = (int)FRect.Left / 32; j < (FRect.Left + FRect.Width) / 32; j++)
                 {
                     if (tileMap[i][j] == 'B')
                     {
                         if ((dx > 0) && (dir == 0))
                             FRect.Left = (float)j * 32 - FRect.Width;
                         if ((dx < 0) && (dir == 0))
                             FRect.Left = (float)j * 32 + 32;
                         
                         if ((dy > 0) && (dir == 1))
                         {
                             FRect.Top = i * 32 - FRect.Height;
                             dy = 0;
                             bOnGround = true;
                         }
                         if ((dy < 0) && (dir == 1))
                         {
                             FRect.Top = (float)i * 32 + 32;
                             dy = 0;
                         }
                     }
                     
                     if (tileMap[i][j] == '0')
                         tileMap[i][j] = ' ';
                 }
         }
    }
}
