#include <SFML/Graphics.hpp>
#include <SFML/OpenGL.hpp>
#include <time.h>
#include "Box.h"


UINT  g_wndWidth = 1280,
	  g_wndHeight = 720;
FLOAT size = 20.0f;

// cam
float g_angleX,
      g_angleY;
//float PI = 3.141592f;
//const float PI = 3.141592653;
const double PI = 3.141592653;
bool ArrayMap[1000][1000][1000];


class Player
{
public:
	float x, y, z;
	float dx, dy, dz;
	float w, h, d;
	bool onGround;
	float speed;

	Player(float x_, float y_, float z_)
	{
		x = x_;
		y = y_;
		z = z_;
		dx = dy = dz = 0;
		w = 5.0f; h = 20.0f; d = 5.0f; 
		speed = 5.0f;
		onGround = false;
	}

	void KeyBoard()
	{
		if (sf::Keyboard::isKeyPressed(sf::Keyboard::Space))
			if (onGround)
			{
				onGround = false;
				dy = 12;
			}

		if (sf::Keyboard::isKeyPressed(sf::Keyboard::W))
		{
			dx =- (float)sin(g_angleX / 180 * PI) * speed;
			//dy = tan(g_angleY / 180 * PI) * speed;
			dz =- (float)cos(g_angleX / 180 * PI) * speed;
		}
		if (sf::Keyboard::isKeyPressed(sf::Keyboard::S))
		{
			dx = (float)sin(g_angleX / 180 * PI) * speed;
			//dy = -tan(g_angleY / 180 * PI) * speed;
			dz = (float)cos(g_angleX / 180 * PI) * speed;
		}
		if (sf::Keyboard::isKeyPressed(sf::Keyboard::D))
		{
			dx = (float)sin((g_angleX + 90) / 180 * PI) * speed;
			dz = (float)cos((g_angleX + 90) / 180 * PI) * speed;
		}
		if (sf::Keyboard::isKeyPressed(sf::Keyboard::A))
		{
			dx = (float)sin((g_angleX - 90) / 180 * PI) * speed;
			dz = (float)cos((g_angleX - 90) / 180 * PI) * speed;
		}
	}

	void Collision(float Dx, float Dy, float Dz)
	{
		for (int X = int((x - w) / size); X < (x + w) / size; X++)
		for (int Y = int((y - h) / size); Y < (y + h) / size; Y++)
		for (int Z = int((z - d) / size); Z < (z + d) / size; Z++)
					if (check(X, Y, Z))
					{
						if (Dx > 0)   x = X*size - w;
						if (Dx < 0)   x = X*size + size + w;
						if (Dy > 0)   y = Y*size - h;
						if (Dy < 0) { y = Y*size + size + h; onGround = true; dy = 0; }
						if (Dz > 0)   z = Z*size - d;
						if (Dz < 0)   z = Z*size + size + d;
					}
	}

	void Update(float time)
	{
		if (!onGround)
			dy -= 1.5f*time;

		onGround = false;

		x += dx * time;
		Collision(dx, 0, 0);
		y += dy * time;
		Collision(0, dy, 0);
		z += dz * time;
		Collision(0, 0, dz);

		dx = dz = 0;
	}
};


int main()
{
	//sf::ContextSettings contextSettings;
	//contextSettings.depthBits = 32;

	sf::RenderWindow window(sf::VideoMode(g_wndWidth, g_wndHeight), "My GL Micraft by Roman P.");
	//window.setVerticalSyncEnabled(true);
	
	// Enable Z-buffer read and write
	glEnable(GL_DEPTH_TEST);
	glDepthMask(GL_TRUE);
	glClearDepth(1.f);
	//glDisable(GL_LIGHTING);
	// Setup a perspective projection
	glMatrixMode(GL_PROJECTION);
	glLoadIdentity();
	//GLfloat ratio = static_cast<float>(window.getSize().x) / window.getSize().y;
	//glFrustum(-ratio, ratio, -1.f, 1.f, 1.f, 500.f);
	float asp = (float)g_wndWidth / g_wndHeight;
	gluPerspective(90.0, asp, 1.0f, 2000.0f);
	// Bind the texture
	glEnable(GL_TEXTURE_2D);
	ShowCursor(FALSE);
	
	//srand((unsigned int)time(0));
	//for (int x = 0; x < 20; x++)
	//	for (int y = 0; y < 20; y++)
	//		for (int z = 0; z < 20; z++)
	//		{
	//			if ((y == 0) || (rand() % 100 == 1))
	//				ArrayMap[x][y][z] = 1;
	//		}

	sf::Image img;
	img.loadFromFile("../Content/heightmap.png");
	for (int x = 0; x < 256; x++)
		for (int z = 0; z < 256; z++)
		{
			int c = img.getPixel(x, z).r / 15;
			for (int y = 0; y < c; y++)
				if (y > c - 3)
					ArrayMap[x][y][z] = true;
		}

	sf::Texture tcursor;
	if (!tcursor.loadFromFile("../Content/cursor.png"))
		return EXIT_FAILURE;
	sf::Sprite cursor(tcursor);
	cursor.setOrigin(8, 8);
	cursor.setPosition((float)g_wndWidth / 2, (float)g_wndHeight / 2);

	// Create some text to draw on top of our OpenGL object
	sf::Font font;
	if (!font.loadFromFile("../Content/sansation.ttf"))
		return EXIT_FAILURE;
	sf::Text text("Micraft by Roman P. demo", font);
	text.setColor(sf::Color(255, 255, 255, 170));
	text.setPosition(50.f, 20.f);

	GLuint box[6];
	box[0] = LoadTexture("../Content/grassBox/side.jpg");
	box[1] = LoadTexture("../Content/grassBox/side.jpg");
	box[2] = LoadTexture("../Content/grassBox/side.jpg"); 
	box[3] = LoadTexture("../Content/grassBox/side.jpg");
	box[4] = LoadTexture("../Content/grassBox/bottom.jpg");
	box[5] = LoadTexture("../Content/grassBox/top.jpg");

	GLuint SkyBox[6];
	SkyBox[0] = LoadTexture("../Content/skybox2/skybox_front.bmp");
	SkyBox[1] = LoadTexture("../Content/skybox2/skybox_back.bmp");
	SkyBox[2] = LoadTexture("../Content/skybox2/skybox_left.bmp"); 
	SkyBox[3] = LoadTexture("../Content/skybox2/skybox_right.bmp");
	SkyBox[4] = LoadTexture("../Content/skybox2/skybox_bottom.bmp");
	SkyBox[5] = LoadTexture("../Content/skybox2/skybox_top.bmp");

	window.setActive();

	// Configure the viewport (the same size as the window)
	glViewport(0, 0, window.getSize().x, window.getSize().y);

	// Create a clock for measuring the time elapsed
	sf::Clock clock;
	Player player(100, 200, 100);
	bool mLeft = false, mRight = false; // mouse buttons

	// Start game loop
	while (window.isOpen())
	{
		float time = (float)clock.getElapsedTime().asMilliseconds();
		clock.restart();
		time = time / 50;
		if (time > 3) time = 3;

		// Process events
		sf::Event event;
		while (window.pollEvent(event))
		{
			// Close window: exit
			if (event.type == sf::Event::Closed)
				window.close();

			// Escape key: exit
			if ((event.type == sf::Event::KeyPressed) && (event.key.code == sf::Keyboard::Escape))
				window.close();

			if (event.type == sf::Event::MouseButtonPressed)
			{
				if (event.key.code == sf::Mouse::Right) mRight = true;
				if (event.key.code == sf::Mouse::Left)  mLeft = true;
			} 

			// Adjust the viewport when the window is resized
			if (event.type == sf::Event::Resized)
				glViewport(0, 0, event.size.width, event.size.height);
		}

		// Clear the depth buffer
		glClear(GL_DEPTH_BUFFER_BIT);

		player.KeyBoard();
		player.Update(time);

		POINT mouseXY;
		GetCursorPos(&mouseXY);
		int xt = window.getPosition().x + (window.getSize().x / 2);
		int yt = window.getPosition().y + (window.getSize().y / 2);

		g_angleX += (xt - mouseXY.x) / 4; // 4 - чувствительность
		g_angleY += (yt - mouseXY.y) / 4;

		if (g_angleY < -89.0f) { g_angleY = -89.0f; }
		if (g_angleY > 89.0f)  { g_angleY = 89.0f; }
		SetCursorPos(xt, yt);
		
		if (mRight || mLeft)
		{
			float x = player.x;
			float y = player.y + player.h / 2;
			float z = player.z;

			int X, Y, Z, oldX, oldY, oldZ;
			int dist = 0;

			while (dist < 120) // радиус действия
			{
				dist++;
				x += -sin(g_angleX / 180 * PI); X = int(x / size);
				y +=  tan(g_angleY / 180 * PI); Y = int(y / size);
				z += -cos(g_angleX / 180 * PI); Z = int(z / size);

				if (check(X, Y, Z))
				{
					if (mLeft)
					{
						ArrayMap[X][Y][Z] = false;
						break;
					}
					else
					{
						ArrayMap[oldX][oldY][oldZ] = true;
						break;
					}
				}

				oldX = X; oldY = Y; oldZ = Z;
			}
		}

		mLeft = mRight = false;

		// Apply some transformations
		glMatrixMode(GL_MODELVIEW);
		glLoadIdentity();
		gluLookAt(player.x, player.y + player.h/2, player.z,
			player.x - sin(g_angleX / 180 * PI),
			player.y + player.h / 2 + (tan(g_angleY / 180 * PI)),
			player.z - cos(g_angleX / 180 * PI), 0, 1, 0);
		
		// Draw boxes
		int R = 30;// радиус видимости
		int X = int(player.x / size);
		int Y = int(player.y / size);
		int Z = int(player.z / size);

		for (int x = X-R; x < X+R; x++)
			for (int y = 0; y < 25; y++)
				for (int z = Z-R; z < Z+R; z++)
				{
					//if (!ArrayMap[x][y][z])
					//	continue;

					if (!check(x, y, z)) continue;

					glTranslatef(size*x + size / 2, size*y + size / 2, size*z + size / 2);
					CreateBox(box, size/2);
					glTranslatef(-size*x - size / 2, -size*y - size / 2, -size*z - size / 2);
				}

		// Draw skybox
		glTranslatef(player.x, player.y, player.z);
		CreateBox(SkyBox, 1000);
		glTranslatef(-player.x, -player.y, -player.z);

		// Draw some text on top of our OpenGL object
		window.pushGLStates();
		window.draw(text);
		// рисуем курсор
		window.draw(cursor);      
		window.popGLStates();

		// Finally, display the rendered frame on screen
		window.display();
	}

	// Don't forget to destroy our texture
	glDeleteTextures(6, box);
	glDeleteTextures(6, SkyBox);

	return 0;
}
