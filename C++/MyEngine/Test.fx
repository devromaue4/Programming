//--------------------------------------------------------------------------------------
// File: Test.fx
//
// Copyright (c) . All rights reserved.
//--------------------------------------------------------------------------------------

//--------------------------------------------------------------------------------------
// Constant Buffer Variables
//--------------------------------------------------------------------------------------
cbuffer ConstantBuffer : register(b0)
{
	matrix World;
	matrix View;
	matrix Projection;
}

//--------------------------------------------------------------------------------------
struct VS_OUTPUT
{
	float4 Pos : SV_POSITION;
	float4 Color : COLOR0;
};

//
// vs shader
//
VS_OUTPUT VS(float4 Pos : POSITION, float4 Color : COLOR)
{
	VS_OUTPUT Output = (VS_OUTPUT)0;
	Output.Pos = mul(Pos, World);
	Output.Pos = mul(Output.Pos, View);
	Output.Pos = mul(Output.Pos, Projection);
	Output.Color = Color;
	return Output;
}


//
// ps shader
//
float4 PS(VS_OUTPUT Input) : SV_Target
{
	return Input.Color;
}