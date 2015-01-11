#include <SFML/Graphics.hpp>
//using namespace sf;

int g_WndWidth = 800,
    g_WndHeight = 400;
float g_Ground = 300;
float g_fOffsetX = 0, 
      g_fOffsetY = 0;
const int H = 12;
const int W = 40;

sf::String TileMap[H] = 
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
		FRect = sf::FloatRect(0, 0, 40, 50);
		dx = dy = 0.0f;
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

		//x
		Collision(0);

		if (!bOnGround)
			dy = dy + 0.0005f * fTime;

		FRect.top += dy * fTime;

		bOnGround = false;

		//y
		Collision(1);

		fCurrentFrame += 0.005f * fTime;
		if (fCurrentFrame > 6)
			fCurrentFrame -= 6;

		if(dx > 0)
			SSprite.setTextureRect(sf::IntRect(40 * int(fCurrentFrame), 244, 40, 50));
		if(dx < 0)
			SSprite.setTextureRect(sf::IntRect(40 * int(fCurrentFrame) + 40, 244, -40, 50));

		SSprite.setPosition(FRect.left - g_fOffsetX, FRect.top - g_fOffsetY);

		dx = 0;
	}

	void Collision(int dir)
	{
		for (int i = int(FRect.top/32); i < (FRect.top+FRect.height)/32; i++)
			for (int j = int(FRect.left/32); j < (FRect.left+FRect.width)/32; j++)
			{
				if (TileMap[i][j] == 'B')
				{
					if ((dx > 0) && (dir == 0))
						FRect.left = float(j * 32 - FRect.width);
					if ((dx < 0) && (dir == 0))
						FRect.left = float(j * 32 + 32);

					if ((dy > 0) && (dir == 1))
					{ 
						FRect.top = i * 32 - FRect.height;
						dy = 0;
						bOnGround = true;
					}
					if ((dy < 0) && (dir == 1))
					{
						FRect.top = float(i * 32 + 32);
						dy = 0;
					}	
				}

				if (TileMap[i][j] == '0')
					TileMap[i][j] = ' ';	
			}
	}
};


int main()
{
	sf::RenderWindow AppWindow(sf::VideoMode(g_WndWidth, g_WndHeight), "Test - SFML");

	sf::CircleShape Circle(100.0f);
	Circle.setFillColor(sf::Color::Green);

	sf::Texture tx;
	tx.loadFromFile("..\\Data\\fang.png");

	float fCurrentFrame = 0.0f;

	Player player(tx);

	sf::Clock clock;

	sf::RectangleShape RecShape(sf::Vector2f(32, 32));

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
				player.dy = -0.33f;
				player.bOnGround = false;
			}
		}

		player.Update(time);

		if (player.FRect.left > g_WndWidth / 2)
			g_fOffsetX = player.FRect.left - g_WndWidth/2;
		if (player.FRect.top > g_WndHeight / 2)
			g_fOffsetY = player.FRect.top - g_WndHeight / 2;
		
		AppWindow.clear(sf::Color::White);

		for (int i = 0; i < H; i++)
		{
			for (int j = 0; j < W; j++)
			{
				if (TileMap[i][j] == 'B')
					RecShape.setFillColor(sf::Color::Black);
				if (TileMap[i][j] == '0')
					RecShape.setFillColor(sf::Color::Green);
				if (TileMap[i][j] == ' ')
					continue;

				RecShape.setPosition(float(j * 32) - g_fOffsetX, float(i * 32) - g_fOffsetY);
				AppWindow.draw(RecShape);
			}
		}

		AppWindow.draw(player.SSprite);
		AppWindow.display();
	}

	return 0;
}