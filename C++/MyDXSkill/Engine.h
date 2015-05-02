
#pragma once
#include <iostream>
using namespace std;

// dx9
#include <d3d9.h>
#include <d3dx9.h>

class Engine
{
	IDirect3D9* pD3D;
	IDirect3DDevice9* pD3DDevice;
	HWND hWnd;

	bool InitWnd(int Width, int Height);
	bool InitDX();
	void LoopMain();
	void Render();
	
	static LRESULT CALLBACK WindowProc(HWND hwnd, UINT uMsg, WPARAM wParam, LPARAM lParam);

public:
	Engine();
	Engine(int Width, int Height);
};


