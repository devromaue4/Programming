#include <SFML/Graphics.hpp>
#include <SFML/Audio.hpp>
//using namespace sf;

int g_WndWidth = 800,
    g_WndHeight = 400;
float g_Ground = 300;
float g_fOffsetX = 0, 
      g_fOffsetY = 0;
const int H = 17;
const int W = 120;
const int SIZE_TILE = 16;

sf::String TileMap[H] = 
{
	"000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000",
	"0                                                                                                                      0",
	"0                                                                                    w                                 0",
	"0                   w                                  w                   w                                           0",
	"0                                      w                                       kk                                      0",
	"0                                                                             k  k    k    k                           0",
	"0                      c                                                      k      kkk  kkk  w                       0",
	"0                                                                       r     k       k    k                           0",
	"0                                                                      rr     k  k                  h                  0",
	"0                                                                     rrr      kk                                      0",
	"0               c    kckck                                           rrrr                                      0       0",
	"0                                      t0                           rrrrr                                      0       0",
	"0G                           t0        00              t0          rrrrrr            G                         0       0",
	"0           d    g       d   00        00              00         rrrrrrr                                      0       0",
	"PPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPP",
	"PPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPP",
	"PPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPP",
};


class Player
{
public:
	float dx, dy;
	sf::FloatRect FRect;
	bool bOnGround;
	sf::Sprite SSprite;
	float fCurrentFrame;

	Player(sf::Texture &Img)
	{
		SSprite.setTexture(Img);
		FRect = sf::FloatRect(0, 0, 16, 16);
		fCurrentFrame = 0.0f;

		// pos
		FRect.left = 100;
		FRect.top = 50;// 100;
		
		dx = 0.01f; // for init sprite
		dy = 0.0f;
	}

	void Update(float fTime)
	{
		FRect.left += dx * fTime;

		// x
		Collision(0);

		if (!bOnGround)
			dy = dy + 0.0005f * fTime;

		FRect.top += dy * fTime;

		bOnGround = false;

		// y
		Collision(1);

		fCurrentFrame += 0.005f * fTime;
		if (fCurrentFrame > 3)
			fCurrentFrame -= 3;

		if(dx > 0)
			SSprite.setTextureRect(sf::IntRect(112 + 30 * int(fCurrentFrame), 144, 16, 16));
		if(dx < 0)
			SSprite.setTextureRect(sf::IntRect(112 + 30 * int(fCurrentFrame) + 16, 144, -16, 16));

		SSprite.setPosition(FRect.left - g_fOffsetX, FRect.top - g_fOffsetY);

		dx = 0;
	}

	void Collision(int dir)
	{
		for (int i = int(FRect.top / SIZE_TILE); i < (FRect.top + FRect.height) / SIZE_TILE; i++)
			for (int j = int(FRect.left / SIZE_TILE); j < (FRect.left + FRect.width) / SIZE_TILE; j++)
			{
				if ((TileMap[i][j] == 'P') || (TileMap[i][j] == 'k') || (TileMap[i][j] == 'r') ||
					(TileMap[i][j] == 't') || (TileMap[i][j] == '0'))
				{
					if ((dx > 0) && (dir == 0))
						FRect.left = float(j * SIZE_TILE - FRect.width);
					if ((dx < 0) && (dir == 0))
						FRect.left = float(j * SIZE_TILE + SIZE_TILE);

					if ((dy > 0) && (dir == 1))
					{ 
						FRect.top = i * SIZE_TILE - FRect.height;
						dy = 0;
						bOnGround = true;
					}
					if ((dy < 0) && (dir == 1))
					{
						FRect.top = float(i * SIZE_TILE + SIZE_TILE);
						dy = 0;
					}	
				}

				if (TileMap[i][j] == 'c')
					TileMap[i][j] = ' ';	
			}
	}
};


class Enemy
{
public:
	float dx, dy;
	sf::FloatRect FRect;
	sf::Sprite SSprite;
	float fCurrentFrame;
	bool bLife;

	void Set(sf::Texture &Img, int x, int y)
	{
		SSprite.setTexture(Img);
		FRect = sf::FloatRect(float(x), float(y), 16, 16);
		fCurrentFrame = 0.0f;
		bLife = true;

		dx = 0.05f; 
		//dy = 0.0f;
	}

	void Update(float fTime)
	{
		FRect.left += dx * fTime;

		Collision();

		fCurrentFrame += fTime * 0.005f;
		if (fCurrentFrame > 2)
			fCurrentFrame -= 2;

		SSprite.setTextureRect(sf::IntRect(18 * int(fCurrentFrame), 0, 16, 16));	

		if (!bLife)
			SSprite.setTextureRect(sf::IntRect(58, 0, 16, 16));

		SSprite.setPosition(FRect.left - g_fOffsetX, FRect.top - g_fOffsetY);
	}

	void Collision()
	{
		for (int i = int(FRect.top / 16); i < (FRect.top + FRect.height) / 16; i++)
		{
			for (int j = int(FRect.left / 16); j < (FRect.left + FRect.width) / 16; j++)
			{
				if ((TileMap[i][j] == 'P') || (TileMap[i][j] == '0'))
				{
					if (dx > 0)
					{
						FRect.left = j * 16 - FRect.width;
						dx *= -1;
					}
					else if (dx < 0)
					{
						FRect.left = float(j * 16 + 16);
						dx *= -1;
					}
				}
			}
		}
	}

};


int main()
{
	sf::RenderWindow AppWindow(sf::VideoMode(g_WndWidth, g_WndHeight), "Mario by Roman P.");

	sf::Texture tx;
	tx.loadFromFile("..\\Data\\Mario_tileset.png");
	//tx.loadFromFile("..\\Data\\fang.png");

	sf::Sprite MapTiles(tx);

	float fCurrentFrame = 0.0f;

	Player player(tx);
	Enemy  enemy;
	enemy.Set(tx, 750, 208);

	sf::SoundBuffer sb;
	sb.loadFromFile("..\\Data\\Jump.ogg");
	sf::Sound soundJump(sb);

	sf::Music music;
	music.openFromFile("..\\Data\\Mario_Theme.ogg");
	music.setLoop(true);
	music.play();

	sf::Clock clock;

	//sf::RectangleShape RecShape(sf::Vector2f(SIZE_TILE, SIZE_TILE));

	g_fOffsetY = player.FRect.top - g_WndHeight/ 2;

	while (AppWindow.isOpen())
	{ 
		float time = (float)clock.getElapsedTime().asMicroseconds();
		clock.restart();

		time = time / 700;

		sf::Event ev;
		while (AppWindow.pollEvent(ev))
		{
			if (ev.type == sf::Event::Closed)
				AppWindow.close();
		}

		if (sf::Keyboard::isKeyPressed(sf::Keyboard::Left))
		{ 
			player.dx = -0.1f;
		}
		if (sf::Keyboard::isKeyPressed(sf::Keyboard::Right))
		{
			player.dx = 0.1f;
		}
		if (sf::Keyboard::isKeyPressed(sf::Keyboard::Up))
		{
			if (player.bOnGround)
			{
				player.dy = -0.25f;
				player.bOnGround = false;
				soundJump.play();
			}
		}

		player.Update(time);
		enemy.Update(time);

		if (player.FRect.intersects(enemy.FRect))
		{
			if (enemy.bLife)
			{
				if (player.dy > 0)
				{ 
					enemy.dx = 0;
					player.dy = -0.2f;
					enemy.bLife = false;
				}
				else
					player.SSprite.setColor(sf::Color::Red);
			}

			//player.SSprite.setColor(sf::Color::Red);
		}

		if (player.FRect.left > g_WndWidth / 2)
			g_fOffsetX = player.FRect.left - g_WndWidth/2;
		//if (player.FRect.top > g_WndHeight/ 2)
		//	g_fOffsetY = player.FRect.top - g_WndHeight / 2;
		
		AppWindow.clear(sf::Color(107, 140, 255, 255));

		// load map
		for (int i = 0; i < H; i++)
		{
			for (int j = 0; j < W; j++)
			{
				if (TileMap[i][j] == 'P')
					MapTiles.setTextureRect(sf::IntRect(96, 112, SIZE_TILE, SIZE_TILE));
				if (TileMap[i][j] == 'r')
					MapTiles.setTextureRect(sf::IntRect(112, 112, SIZE_TILE, SIZE_TILE));
				if (TileMap[i][j] == 'k')
					MapTiles.setTextureRect(sf::IntRect(144, 112, SIZE_TILE, SIZE_TILE));
				if (TileMap[i][j] == 'c')
					MapTiles.setTextureRect(sf::IntRect(127, 112, SIZE_TILE, SIZE_TILE));
				if (TileMap[i][j] == 'd')
					MapTiles.setTextureRect(sf::IntRect(0, 106, 74, 127 - 106));
				if (TileMap[i][j] == 'g')
					MapTiles.setTextureRect(sf::IntRect(0,16*9-5,3*16,16*2+5));
				if (TileMap[i][j] == 'G')  
					MapTiles.setTextureRect(sf::IntRect(145, 222, 222 - 145, 255 - 222));
				if (TileMap[i][j] == 't')  
					MapTiles.setTextureRect(sf::IntRect(0, 47, 32, 95 - 47));
				if (TileMap[i][j] == 'w') 
					MapTiles.setTextureRect(sf::IntRect(99, 224, 140 - 99, 255 - 224));
				if (TileMap[i][j] == 'h')
					MapTiles.setTextureRect(sf::IntRect(96, 6, 108, 106));
				if (TileMap[i][j] == ' ' || TileMap[i][j] == '0')
					continue;

				MapTiles.setPosition(j * SIZE_TILE - g_fOffsetX, i * SIZE_TILE - g_fOffsetY);
				AppWindow.draw(MapTiles);
			}
		}

		AppWindow.draw(player.SSprite);
		AppWindow.draw(enemy.SSprite);
		AppWindow.display();
	}

	return 0;
}