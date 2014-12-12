//--------------------------------------------------------------------------------------
// File: D3D11App.cpp
//
// This application demonstrates simple lighting in the vertex shader
//
// Copyright (c) Microsoft Corporation. All rights reserved.
//--------------------------------------------------------------------------------------


#pragma warning(disable:4100) // unreference parameters

#include <windows.h>
#include "resource.h"
#include <iostream>
#include <vector>
using namespace std;
#include "RenderD3D11.h"


//--------------------------------------------------------------------------------------
// Structures
//--------------------------------------------------------------------------------------
struct VERTEX
{
	XMFLOAT3 Pos;
	XMFLOAT4 Color;
};

struct ConstantBuffer
{
	XMMATRIX mWorld;
	XMMATRIX mView;
	XMMATRIX mProjection;
};

//--------------------------------------------------------------------------------------
// Global Variables
//--------------------------------------------------------------------------------------
HINSTANCE               g_hInst = NULL;
HWND                    g_hWnd = NULL;
ID3D11VertexShader*     g_pVS;
ID3D11PixelShader*      g_pPS;
ID3D11InputLayout*      g_pVertLayout;
ID3D11Buffer*           g_pVBuffer;
ID3D11Buffer*           g_pIBuffer;
ID3D11Buffer*           g_pConstBuffer;
XMMATRIX                g_World;
XMMATRIX                g_View;
XMMATRIX                g_Projection;

//--------------------------------------------------------------------------------------
// Forward declarations
//--------------------------------------------------------------------------------------
HRESULT InitWindow( HINSTANCE hInstance, int nCmdShow );
HRESULT InitDevice();
void CleanupDevice();
LRESULT CALLBACK    WndProc( HWND, UINT, WPARAM, LPARAM );
void Render();


//--------------------------------------------------------------------------------------
// Entry point to the program. Initializes everything and goes into a message processing 
// loop. Idle time is used to render the scene.
//--------------------------------------------------------------------------------------
#ifndef CONSOLE_APP
int WINAPI wWinMain( HINSTANCE hInstance, HINSTANCE hPrevInstance, LPWSTR lpCmdLine, int nCmdShow )
#else
int main()
#endif
{
    UNREFERENCED_PARAMETER( hPrevInstance );
    //UNREFERENCED_PARAMETER( lpCmdLine );
	//UNREFERENCED_PARAMETER(nCmdShow);
	//UNREFERENCED_PARAMETER(hInstance);

	cout << "InitWindow" << endl;
	if (FAILED(InitWindow(GetModuleHandle(NULL), SW_SHOWDEFAULT)))
        return 0;

	cout << "InitDevice" << endl;
    if( FAILED( InitDevice() ) )
    {
        CleanupDevice();
        return 0;
    }

    // Main message loop
    MSG msg = {0};
    while( WM_QUIT != msg.message )
    {
        if( PeekMessage( &msg, NULL, 0, 0, PM_REMOVE ) )
        {
            TranslateMessage( &msg );
            DispatchMessage( &msg );
        }
        else
        {
            Render();
        }
    }

    CleanupDevice();

    return ( int )msg.wParam;
}


//--------------------------------------------------------------------------------------
// Register class and create window
//--------------------------------------------------------------------------------------
HRESULT InitWindow( HINSTANCE hInstance, int nCmdShow )
{
    // Register class
    WNDCLASSEX wcex;
    wcex.cbSize = sizeof( WNDCLASSEX );
    wcex.style = CS_HREDRAW | CS_VREDRAW;
    wcex.lpfnWndProc = WndProc;
    wcex.cbClsExtra = 0;
    wcex.cbWndExtra = 0;
    wcex.hInstance = hInstance;
    wcex.hIcon = LoadIcon( hInstance, ( LPCTSTR )IDI_TUTORIAL1 );
    wcex.hCursor = LoadCursor( NULL, IDC_ARROW );
    wcex.hbrBackground = ( HBRUSH )( COLOR_WINDOW + 1 );
    wcex.lpszMenuName = NULL;
    wcex.lpszClassName = L"MEWindowClass";
    wcex.hIconSm = LoadIcon( wcex.hInstance, ( LPCTSTR )IDI_TUTORIAL1 );
    if( !RegisterClassEx( &wcex ) )
        return E_FAIL;

    // Create window
    g_hInst = hInstance;
    RECT rc = { 0, 0, 640, 480 };
    AdjustWindowRect( &rc, WS_OVERLAPPEDWINDOW, FALSE );
    g_hWnd = CreateWindow( L"MEWindowClass", L"Direct3D11: Create 3D space", WS_OVERLAPPEDWINDOW,
                           CW_USEDEFAULT, CW_USEDEFAULT, rc.right - rc.left, rc.bottom - rc.top, NULL, NULL, hInstance,
                           NULL );
    if( !g_hWnd )
        return E_FAIL;

    ShowWindow( g_hWnd, nCmdShow );

    return S_OK;
}


HRESULT CompileShaderFromFile(WCHAR* sFileName, LPCSTR sEntryPoint, LPCSTR sShaderModel, ID3DBlob** ppBlobOut)
{ 
	HRESULT hr = S_OK;

	DWORD dwShaderFlags = D3DCOMPILE_ENABLE_STRICTNESS;
#if defined( DEBUG ) || defined( _DEBUG )
	dwShaderFlags |= D3DCOMPILE_DEBUG;
#endif

	ID3DBlob* pErrorBlob;
	hr = D3DX11CompileFromFile(sFileName, NULL, NULL, sEntryPoint, sShaderModel, dwShaderFlags, 0, NULL,
		ppBlobOut, &pErrorBlob, NULL);
	if (FAILED(hr))
	{
		if (pErrorBlob != NULL)
			OutputDebugStringA((char*)pErrorBlob->GetBufferPointer());
		if (pErrorBlob)
			pErrorBlob->Release();

		return hr;
	}

	if (pErrorBlob)
		pErrorBlob->Release();

	return S_OK;
}


//--------------------------------------------------------------------------------------
// Create Direct3D device and swap chain
//--------------------------------------------------------------------------------------
HRESULT InitDevice()
{
    HRESULT hr = S_OK;
	
	ID3DBlob* pVSBlob = NULL;
	hr = CompileShaderFromFile(L"Test.fx", "VS", "vs_4_0", &pVSBlob);
	if (FAILED(hr))
	{
		MessageBox(NULL, L"The FX file cannot be compiled.", L"Error", MB_ICONERROR| MB_OK);
		return hr;
	}

	RECT rc;
	GetClientRect(g_hWnd, &rc);
	UINT width = rc.right - rc.left;
	UINT height = rc.bottom - rc.top;
	GRenderD3D11->Init(g_hWnd, width, height);

	hr = GRenderD3D11->GetDevice()->CreateVertexShader(pVSBlob->GetBufferPointer(), pVSBlob->GetBufferSize(), NULL, &g_pVS);
	if( FAILED( hr ) )
	{
		pVSBlob->Release();
		return hr;
	}

	D3D11_INPUT_ELEMENT_DESC layout[] =
	{
		{ "POSITION", 0, DXGI_FORMAT_R32G32B32_FLOAT, 0, 0, D3D11_INPUT_PER_VERTEX_DATA, 0 },
		{ "COLOR", 0, DXGI_FORMAT_R32G32B32A32_FLOAT, 0, 12, D3D11_INPUT_PER_VERTEX_DATA, 0 }
	};
	UINT numElements = ARRAYSIZE(layout);
	
	hr = GRenderD3D11->GetDevice()->CreateInputLayout(layout, numElements, pVSBlob->GetBufferPointer(), pVSBlob->GetBufferSize(),
		&g_pVertLayout);
	pVSBlob->Release();
	if (FAILED(hr))
		return hr;

	GRenderD3D11->GetImmediateContext()->IASetInputLayout(g_pVertLayout);

	//ID3DBlob* pPSBlob = NULL;
	hr = CompileShaderFromFile(L"Test.fx", "PS", "ps_4_0", &pVSBlob);
	if (FAILED(hr))
	{
		MessageBox(NULL, L"The FX file cannot be compiled.", L"Error", MB_ICONERROR | MB_OK);
		return hr;
	}

	hr = GRenderD3D11->GetDevice()->CreatePixelShader(pVSBlob->GetBufferPointer(), pVSBlob->GetBufferSize(), NULL, &g_pPS);
	pVSBlob->Release();
	if (FAILED(hr))
		return hr;

	VERTEX Vertices[] =
	{
		XMFLOAT3(-1.0f, 0.0f, 0.0f), XMFLOAT4(0.0f, 0.0f, 1.0f, 1.0f),
		XMFLOAT3(0.0f, 1.0f, 0.0f), XMFLOAT4(0.0f, 0.0f, 1.0f, 1.0f),
		XMFLOAT3(1.0f, 0.0f, 0.0f), XMFLOAT4(0.0f, 0.0f, 1.0f, 1.0f),
	};

	D3D11_BUFFER_DESC bd;
	ZeroMemory(&bd, sizeof(bd));
	bd.Usage = D3D11_USAGE_DEFAULT;
	bd.ByteWidth = sizeof(VERTEX) * 3;
	bd.BindFlags = D3D11_BIND_VERTEX_BUFFER;
	bd.CPUAccessFlags = 0;
	D3D11_SUBRESOURCE_DATA subData;
	ZeroMemory(&subData, sizeof(subData));
	subData.pSysMem = Vertices;

	hr = GRenderD3D11->GetDevice()->CreateBuffer(&bd, &subData, &g_pVBuffer);
	if (FAILED(hr))
		return hr;

	UINT stride = sizeof(VERTEX);
	UINT offset = 0;
	GRenderD3D11->GetImmediateContext()->IASetVertexBuffers(0, 1, &g_pVBuffer, &stride, &offset);

	GRenderD3D11->GetImmediateContext()->IASetPrimitiveTopology(D3D11_PRIMITIVE_TOPOLOGY_TRIANGLELIST);

	// Create the constant buffer
	bd.Usage = D3D11_USAGE_DEFAULT;
	bd.ByteWidth = sizeof(ConstantBuffer);
	bd.BindFlags = D3D11_BIND_CONSTANT_BUFFER;
	bd.CPUAccessFlags = 0;
	hr = GRenderD3D11->GetDevice()->CreateBuffer(&bd, NULL, &g_pConstBuffer);
	if (FAILED(hr))
		return hr;

	// Initialize the world matrix
	g_World = XMMatrixIdentity();

	// Initialize the view matrix
	XMVECTOR Eye = XMVectorSet(0.0f, 0.0f, -2.0f, 0.0f);
	XMVECTOR At = XMVectorSet(0.0f, 1.0f, 0.0f, 0.0f);
	XMVECTOR Up = XMVectorSet(0.0f, 1.0f, 0.0f, 0.0f);
	g_View = XMMatrixLookAtLH(Eye, At, Up);

	// Initialize the projection matrix
	g_Projection = XMMatrixPerspectiveFovLH(XM_PIDIV2, width / (FLOAT)height, 0.01f, 100000.0f);

    return S_OK;
}


//--------------------------------------------------------------------------------------
// Clean up the objects we've created
//--------------------------------------------------------------------------------------
void CleanupDevice()
{
	GRenderD3D11->Release();
}


//--------------------------------------------------------------------------------------
// Called every time the application receives a message
//--------------------------------------------------------------------------------------
LRESULT CALLBACK WndProc( HWND hWnd, UINT message, WPARAM wParam, LPARAM lParam )
{
    PAINTSTRUCT ps;
    HDC hdc;

    switch( message )
    {
        case WM_PAINT:
            hdc = BeginPaint( hWnd, &ps );
            EndPaint( hWnd, &ps );
            break;

        case WM_DESTROY:
            PostQuitMessage( 0 );
            break;

        default:
            return DefWindowProc( hWnd, message, wParam, lParam );
    }

    return 0;
}


//--------------------------------------------------------------------------------------
// Render a frame
//--------------------------------------------------------------------------------------
void Render()
{
	// Update our time
	static float t = 0.0f;
	static DWORD dwTimeStart = 0;
	DWORD dwTimeCur = GetTickCount();
	if (dwTimeStart == 0)
		dwTimeStart = dwTimeCur;
	t = (dwTimeCur - dwTimeStart) / 1000.0f;

	//
	// Animate the cube
	//
	g_World = XMMatrixRotationY(t);

	// Just clear the backbuffer
	float ClearColor[4] = { 0.0f, 0.125f, 0.3f, 1.0f }; //red,green,blue,alpha
	ID3D11DeviceContext* pImmediateContext = GRenderD3D11->GetImmediateContext();
	pImmediateContext->ClearRenderTargetView(GRenderD3D11->GetRenderTargetView(), ClearColor);

	//
	// Update variables
	//
	ConstantBuffer cb;
	cb.mWorld = XMMatrixTranspose(g_World);
	cb.mView = XMMatrixTranspose(g_View);
	cb.mProjection = XMMatrixTranspose(g_Projection);
	pImmediateContext->UpdateSubresource(g_pConstBuffer, 0, NULL, &cb, 0, 0);

	pImmediateContext->VSSetShader(g_pVS, NULL, 0);
	pImmediateContext->VSSetConstantBuffers(0, 1, &g_pConstBuffer);
	pImmediateContext->PSSetShader(g_pPS, NULL, 0);
	pImmediateContext->Draw(3, 0);

	GRenderD3D11->GetSwapChain()->Present(0, 0);
}


