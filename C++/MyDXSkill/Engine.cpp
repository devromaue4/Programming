#include "GEngine.h"

Engine::Engine() 
	: Engine(800, 600)
{
}

Engine::Engine(int Width, int Height)
{
	InitWnd(Width, Height);
	InitDX();
	LoopMain();
}


LRESULT CALLBACK Engine::WindowProc(HWND hwnd, UINT uMsg, WPARAM wParam, LPARAM lParam)
{
	return DefWindowProc(hwnd, uMsg, wParam, lParam);
}

bool Engine::InitWnd(int Width, int Height)
{
	WNDCLASS wndCls;
	wndCls.style = CS_DBLCLKS;
	wndCls.lpfnWndProc = WindowProc;
	wndCls.cbClsExtra = 0;
	wndCls.cbWndExtra = 0;
	wndCls.hInstance = GetModuleHandle(NULL);
	wndCls.hIcon = NULL;
	wndCls.hCursor = NULL;
	wndCls.hbrBackground = NULL;
	wndCls.lpszMenuName = NULL;
	wndCls.lpszClassName = "ClsDXClass";

	if (!RegisterClass(&wndCls))
		return false;

	RECT rc = { 0, 0, Width, Height };
	AdjustWindowRect(&rc, WS_OVERLAPPEDWINDOW, NULL);

	hWnd = CreateWindow("ClsDXClass", "DXApp", WS_OVERLAPPEDWINDOW,
		0, 0, (rc.right - rc.left), (rc.bottom - rc.top), NULL, NULL, wndCls.hInstance, NULL);
	if (!hWnd)
		return false;

	ShowWindow(hWnd, SW_SHOWDEFAULT);

	return true;
}

bool Engine::InitDX()
{
	pD3D = Direct3DCreate9(D3D_SDK_VERSION);
	if (!pD3D)
		return false;

	RECT rc;
	GetClientRect(hWnd, &rc);

	D3DPRESENT_PARAMETERS pp;
	memset(&pp, 0, sizeof(D3DPRESENT_PARAMETERS));
	pp.BackBufferWidth = rc.right;
	pp.BackBufferHeight = rc.bottom;
	pp.BackBufferFormat = D3DFMT_X8R8G8B8;
	pp.BackBufferCount = 1;
	pp.MultiSampleType = D3DMULTISAMPLE_NONE;
	pp.MultiSampleQuality = 0;
	pp.SwapEffect = D3DSWAPEFFECT_DISCARD;
	pp.hDeviceWindow = hWnd;
	pp.Windowed = TRUE;
	pp.EnableAutoDepthStencil = TRUE;
	pp.AutoDepthStencilFormat = D3DFMT_D24S8;
	pp.Flags = 0;
	if (FAILED(pD3D->CreateDevice(D3DADAPTER_DEFAULT, D3DDEVTYPE_HAL, hWnd,
		D3DCREATE_HARDWARE_VERTEXPROCESSING, &pp, &pD3DDevice)))
		return false;

	return true;
}

void Engine::LoopMain()
{
	MSG msg = { 0 };
	while (msg.message != WM_QUIT)
	{
		if (PeekMessage(&msg, NULL, 0, 0, PM_REMOVE))
		{
			TranslateMessage(&msg);
			DispatchMessage(&msg);
		}
		else
		{
			Render();
		}
	}
}

void Engine::Render()
{
	if (!pD3DDevice)
		return;

	pD3DDevice->Clear(0, NULL, D3DCLEAR_TARGET | D3DCLEAR_ZBUFFER, D3DCOLOR_XRGB(0, 0, 255), 1.0f, 0);

	pD3DDevice->BeginScene();
	pD3DDevice->EndScene();

	pD3DDevice->Present(NULL, NULL, NULL, NULL);
}