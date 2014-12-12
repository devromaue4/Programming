//--------------------------------------------------------------------------------------
// File: Test.fx
//
// Copyright (c) . All rights reserved.
//--------------------------------------------------------------------------------------

//
// vs shader
//
float4 VS(float4 Pos : POSITION) : SV_POSITION
{
	return Pos;
}


//
// ps shader
//
float4 PS( float4 Pos : SV_POSITION ) : SV_Target
{
	return float4(1.0f, 1.0f, 0.0f, 1.0f); 
}