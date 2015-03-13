#ifndef _BOX_H
#define _BOX_H


#define GL_CLAMP_TO_EDGE 0x812F


#include <SFML/Graphics.hpp>
#include <SFML/OpenGL.hpp>
#include <iostream>

//const float PI = 3.141592653;
//bool ArrayMap[1000][1000][1000];
//float size = 20.0f;

GLuint LoadTexture(std::string fileName)
{
	// Load an OpenGL texture.
	// We could directly use a sf::Texture as an OpenGL texture (with its Bind() member function),
	// but here we want more control on it (generate mipmaps, ...) so we create a new one from an image
	GLuint texture = 0;
	sf::Image image;
	if (!image.loadFromFile(fileName))
		return -1;

	image.flipVertically();

	glGenTextures(1, &texture);
	glBindTexture(GL_TEXTURE_2D, texture);
	gluBuild2DMipmaps(GL_TEXTURE_2D, GL_RGBA, image.getSize().x, image.getSize().y, GL_RGBA, GL_UNSIGNED_BYTE, image.getPixelsPtr());
	glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MAG_FILTER, GL_LINEAR);
	glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MIN_FILTER, GL_LINEAR_MIPMAP_LINEAR);

	glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_WRAP_S, GL_CLAMP_TO_EDGE);
	glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_WRAP_T, GL_CLAMP_TO_EDGE);

	return texture;
}


//void CreateBox(GLuint box[], int size)
void CreateBox(const GLuint* box, float size)
{
	glBindTexture(GL_TEXTURE_2D, box[0]);
	glBegin(GL_QUADS);
	// back
	glTexCoord2f(0, 0); glVertex3f(-size, -size, -size);
	glTexCoord2f(1, 0); glVertex3f(size, -size, -size);
	glTexCoord2f(1, 1); glVertex3f(size, size, -size);
	glTexCoord2f(0, 1); glVertex3f(-size, size, -size);
	glEnd();

	glBindTexture(GL_TEXTURE_2D, box[1]);
	glBegin(GL_QUADS);
	// front
	glTexCoord2f(0, 0); glVertex3f(size, -size, size);
	glTexCoord2f(1, 0); glVertex3f(-size, -size, size);
	glTexCoord2f(1, 1); glVertex3f(-size, size, size);
	glTexCoord2f(0, 1); glVertex3f(size, size, size);
	glEnd();

	glBindTexture(GL_TEXTURE_2D, box[2]);
	glBegin(GL_QUADS);
	// left
	glTexCoord2f(0, 0); glVertex3f(-size, -size, size);
	glTexCoord2f(1, 0); glVertex3f(-size, -size, -size);
	glTexCoord2f(1, 1); glVertex3f(-size, size, -size);
	glTexCoord2f(0, 1); glVertex3f(-size, size, size);
	glEnd();

	glBindTexture(GL_TEXTURE_2D, box[3]);
	glBegin(GL_QUADS);
	// right
	glTexCoord2f(0, 0); glVertex3f(size, -size, -size);
	glTexCoord2f(1, 0); glVertex3f(size, -size, size);
	glTexCoord2f(1, 1); glVertex3f(size, size, size);
	glTexCoord2f(0, 1); glVertex3f(size, size, -size);
	glEnd();

	glBindTexture(GL_TEXTURE_2D, box[4]);
	glBegin(GL_QUADS);
	// bottom
	glTexCoord2f(0, 0); glVertex3f(-size, -size, size);
	glTexCoord2f(1, 0); glVertex3f(size, -size, size);
	glTexCoord2f(1, 1); glVertex3f(size, -size, -size);
	glTexCoord2f(0, 1); glVertex3f(-size, -size, -size);
	glEnd();

	glBindTexture(GL_TEXTURE_2D, box[5]);
	glBegin(GL_QUADS);
	// top
	glTexCoord2f(0, 0); glVertex3f(-size, size, -size);
	glTexCoord2f(1, 0); glVertex3f(size, size, -size);
	glTexCoord2f(1, 1); glVertex3f(size, size, size);
	glTexCoord2f(0, 1); glVertex3f(-size, size, size);
	glEnd();
}

extern bool ArrayMap[1000][1000][1000];
bool check(int x, int y, int z)
{
	if ((x < 0) || (x >= 1000) ||
		(y < 0) || (y >= 1000) ||
		(z < 0) || (z >= 1000))
		return false;

	return ArrayMap[x][y][z];
}


#endif
