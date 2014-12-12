#pragma once
#pragma warning(disable:4005) // redefinition dx headers (for vs2013)

#include <d3d11.h>
#include <d3dx11.h>
#include <d3dcompiler.h>
#include <xnamath.h>


class RenderD3D11
{
	D3D_FEATURE_LEVEL       m_featureLevel;
	ID3D11Device*           m_pd3dDevice;
	ID3D11DeviceContext*    m_pImmediateContext;
	IDXGISwapChain*         m_pSwapChain;
	ID3D11RenderTargetView* m_pRenderTargetView;

public:
	RenderD3D11();
	~RenderD3D11();

	bool Init(HWND hWnd, UINT Width, UINT Height);
	void Release();

	ID3D11Device*           GetDevice()           { return m_pd3dDevice; }
	ID3D11DeviceContext*    GetImmediateContext() { return m_pImmediateContext; }
	IDXGISwapChain*         GetSwapChain()        { return m_pSwapChain; }
	ID3D11RenderTargetView* GetRenderTargetView() { return m_pRenderTargetView; }
};


extern RenderD3D11* GRenderD3D11;
