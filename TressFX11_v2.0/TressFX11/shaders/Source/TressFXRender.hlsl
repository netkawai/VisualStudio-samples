//--------------------------------------------------------------------------------------
// File: TressFXRender.hlsl
//
// Hair rendering functions. 
// 
// Copyright � AMD Corporation. All rights reserved.
//--------------------------------------------------------------------------------------

#include "TressFXInclude.hlsl"

//--------------------------------------------------------------------------------------
//
//		Defines for TressFX rendering
//
//--------------------------------------------------------------------------------------
#define PI 3.1415926
#define e 2.71828183

#define KBUFFER_SIZE	8

// techniques: has to match the define in cpp
#define SHADOW_SDSM		2
#define SHADOW_NONE		0

#define SM_EPSILON 0.01
#define KERNEL_SIZE 5
#define NULLPOINTER 0xFFFFFFFF

// These defines are used for conditionally including code
#define SIMPLESHADING			// no specular term
#define SIMPLESHADOWING			// no filtering except PCF
#define SUPERSIMPLESHADING		// constant diffuse (doesn't change with light angle)
//#define COLORDEBUG			// shows the number of overlapping hair fragments with color

#if (KBUFFER_SIZE <= 16)
	#define ALU_INDEXING			// avoids using an indexed array for better performance
#endif

//--------------------------------------------------------------------------------------
// TressFX Structures
//--------------------------------------------------------------------------------------
struct KBuffer_STRUCT
{
    uint2	depthAndPackedtangent;
};

struct HAIR_VERTEX
{
    float3 pos;
    float3 tangent;
    float4 texcoord;
};

struct PS_INPUT_HAIR_AA
{
    float4 Position	: SV_POSITION;
    float4 Tangent	: Tangent;
    float4 p0p1		: TEXCOORD0;
};



//--------------------------------------------------------------------------------------
//
//		Vertex Shader Code
//
//--------------------------------------------------------------------------------------

//--------------------------------------------------------------------------------------
// VS_GenerateHairSM
//
// Vertex shader for generating the shadow map
//--------------------------------------------------------------------------------------
VS_OUTPUT_SM VS_GenerateHairSM( uint vertexId : SV_VertexID )
{
    VS_OUTPUT_SM Output;
    
    float4 pos = g_HairVertexPositions[vertexId];

    // Transform the position from object space to homogeneous projection space
    Output.Position = mul( float4(pos.xyz, 1), g_mViewProjLight );
    
    Output.WorldPos = pos.xyz;
    
    return Output;    
}

//--------------------------------------------------------------------------------------
// VS_RenderHair
//
// Vertex shader for rendering the hair strands without AA
//--------------------------------------------------------------------------------------
PS_INPUT_HAIR_AA VS_RenderHair( uint vertexId : SV_VertexID )
{

    // Access the current line segment
    uint index = vertexId / 2;  // vertexId is actually the indexed vertex id when indexed triangles are used

    // Get updated positions and tangents from simulation result
    float3 t = g_HairVertexTangents[index].xyz;
    float3 v = g_HairVertexPositions[index].xyz;

    // Get hair strand thickness
    float ratio = ( g_bThinTip > 0 ) ? g_HairThicknessCoeffs[index] : 1.0;

    // Calculate right and projected right vectors
    float3 right      = normalize( cross( t, normalize(v - g_vEye) ) );
    float2 proj_right = normalize( mul( float4(right, 0), g_mViewProj ).xy );

    // g_bExpandPixels should be set to 0 at minimum from the CPU side; this would avoid the below test
    float expandPixels = (g_bExpandPixels < 0 ) ? 0.0 : 0.71;

    PS_INPUT_HAIR_AA Output = (PS_INPUT_HAIR_AA)0;

    float fDirCoef = (vertexId & 0x01) ? -1.0 : 1.0;
    float3 temp     = v +  fDirCoef * right * ratio * g_FiberRadius;
    Output.Position = mul(float4(temp, 1.f), g_mViewProj);
    Output.Position = (Output.Position/Output.Position.w) + fDirCoef * float4(proj_right * expandPixels/ g_WinSize.y, 0.f, 0.f);
    Output.Tangent  = float4(t, ratio);
    Output.p0p1     = float4( v.xy, g_HairVertexPositions[index+1].xy );

    return Output;
}

//--------------------------------------------------------------------------------------
// VS_RenderHair_StrandCopies
//
// Vertex shader for rendering the hair strands with randomized copies and without AA
//--------------------------------------------------------------------------------------
PS_INPUT_HAIR_AA VS_RenderHair_StrandCopies( uint vertexId : SV_VertexID, uint instanceId : SV_InstanceID )
{
    // Access the current line segment
    uint index = vertexId / 2;  // vertexId is actually the indexed vertex id when indexed triangles are used

    // Identifies which copy of the strand this is (0 is the original, 1 is the first copy, etc.)
	uint randOffsetIndex = instanceId;
	
	// Getting a random offset value
	uint seedRand = ((index / 64 + 1) * (randOffsetIndex + 1)) % 512;
	float2 seedTexcorrd = float2( float(seedRand % 512), float(seedRand / 512) ) * (1.f/512.f);
	float3 randOffset = float(randOffsetIndex) * g_txNoise.SampleLevel(g_samLinearWrap, seedTexcorrd, 0).xyz;
	randOffset = 2.0f * randOffset - 1.0f; // so we have random offsets between -1 and 1

    // Get updated positions and tangents from simulation result
    float3 t = g_HairVertexTangents[index].xyz;
    float3 v = randOffset + g_HairVertexPositions[index].xyz;  // We apply a random offset to each vertex when multiple strands are requested

    // Get hair strand thickness
    float ratio = ( g_bThinTip > 0 ) ? g_HairThicknessCoeffs[index] : 1.0;

    // Calculate right and projected right vectors
    float3 right      = normalize( cross( t, normalize(v - g_vEye) ) );
    float2 proj_right = normalize( mul( float4(right, 0), g_mViewProj ).xy );

    // g_bExpandPixels should be set to 0 at minimum from the CPU side; this would avoid the below test
    float expandPixels = (g_bExpandPixels < 0 ) ? 0.0 : 0.71;

    PS_INPUT_HAIR_AA Output = (PS_INPUT_HAIR_AA)0;

    float fDirCoef = (vertexId & 0x01) ? -1.0 : 1.0;
    float3 temp     = v +  fDirCoef * right * ratio * g_FiberRadius;
    Output.Position = mul(float4(temp, 1.f), g_mViewProj);
    Output.Position = (Output.Position/Output.Position.w) + fDirCoef * float4(proj_right * expandPixels/ g_WinSize.y, 0.f, 0.f);
    Output.Tangent  = float4(t, ratio);
    Output.p0p1     = float4( v.xy, randOffset.xy + g_HairVertexPositions[index+1].xy );

    return Output;
}

//--------------------------------------------------------------------------------------
// VS_RenderHair_AA
//
// Vertex shader for rendering the hair strands with AA
//--------------------------------------------------------------------------------------
PS_INPUT_HAIR_AA VS_RenderHair_AA( uint vertexId : SV_VertexID )
{
    // Access the current line segment
    uint index = vertexId / 2;  // vertexId is actually the indexed vertex id when indexed triangles are used

    // Get updated positions and tangents from simulation result
    float3 t = g_HairVertexTangents[index].xyz;
    float3 v = g_HairVertexPositions[index].xyz;

    // Get hair strand thickness
    float ratio = ( g_bThinTip > 0 ) ? g_HairThicknessCoeffs[index] : 1.0;

    // Calculate right and projected right vectors
    float3 right      = normalize( cross( t, normalize(v - g_vEye) ) );
    float2 proj_right = normalize( mul( float4(right, 0), g_mViewProj ).xy );

    // g_bExpandPixels should be set to 0 at minimum from the CPU side; this would avoid the below test
    float expandPixels = (g_bExpandPixels < 0 ) ? 0.0 : 0.71;

	// Calculate the negative and positive offset screenspace positions
	float4 hairEdgePositions[2]; // 0 is negative, 1 is positive
	hairEdgePositions[0] = float4(v +  -1.0 * right * ratio * g_FiberRadius, 1.0);
	hairEdgePositions[1] = float4(v +   1.0 * right * ratio * g_FiberRadius, 1.0);
	hairEdgePositions[0] = mul(hairEdgePositions[0], g_mViewProj);
	hairEdgePositions[1] = mul(hairEdgePositions[1], g_mViewProj);
	hairEdgePositions[0] = hairEdgePositions[0]/hairEdgePositions[0].w;
	hairEdgePositions[1] = hairEdgePositions[1]/hairEdgePositions[1].w;

    // Write output data
    PS_INPUT_HAIR_AA Output = (PS_INPUT_HAIR_AA)0;
    float fDirIndex = (vertexId & 0x01) ? -1.0 : 1.0;
    Output.Position = (fDirIndex==-1.0 ? hairEdgePositions[0] : hairEdgePositions[1]) + fDirIndex * float4(proj_right * expandPixels / g_WinSize.y, 0.0f, 0.0f);
    Output.Tangent  = float4(t, ratio);
    Output.p0p1     = float4( hairEdgePositions[0].xy, hairEdgePositions[1].xy );

    return Output;
}

//--------------------------------------------------------------------------------------
// VS_RenderHair_AA_StrandCopies
//
// Vertex shader for rendering the hair strands with randomized copies and AA
//--------------------------------------------------------------------------------------
PS_INPUT_HAIR_AA VS_RenderHair_AA_StrandCopies( uint vertexId : SV_VertexID, uint instanceId : SV_InstanceID )
{
    // Access the current line segment
    uint index = vertexId / 2;  // vertexId is actually the indexed vertex id when indexed triangles are used

    // Identifies which copy of the strand this is (0 is the original, 1 is the first copy, etc.)
	uint randOffsetIndex = instanceId;
	
	// Getting a random offset value
	uint seedRand = ((index / 64 + 1) * (randOffsetIndex + 1)) % 512;
	float2 seedTexcorrd = float2( float(seedRand % 512), float(seedRand / 512) ) * (1.f/512.f);
	float3 randOffset = float(randOffsetIndex) * g_txNoise.SampleLevel(g_samLinearWrap, seedTexcorrd, 0).xyz;
	randOffset = 2.0f * randOffset - 1.0f; // so we have random offsets between -1 and 1

    // Get updated positions and tangents from simulation result
    float3 t = g_HairVertexTangents[index].xyz;
    float3 v = randOffset + g_HairVertexPositions[index].xyz;  // We apply a random offset to each vertex when multiple strands are requested

    // Get hair strand thickness
    float ratio = ( g_bThinTip > 0 ) ? g_HairThicknessCoeffs[index] : 1.0;

    // Calculate right and projected right vectors
    float3 right      = normalize( cross( t, normalize(v - g_vEye) ) );
    float2 proj_right = normalize( mul( float4(right, 0), g_mViewProj ).xy );

    // g_bExpandPixels should be set to 0 at minimum from the CPU side; this would avoid the below test
    float expandPixels = (g_bExpandPixels < 0 ) ? 0.0 : 0.71;

	// Calculate the negative and positive offset screenspace positions
	float4 hairEdgePositions[2]; // 0 is negative, 1 is positive
	hairEdgePositions[0] = float4(v +  -1.0 * right * ratio * g_FiberRadius, 1.0);
	hairEdgePositions[1] = float4(v +   1.0 * right * ratio * g_FiberRadius, 1.0);
	hairEdgePositions[0] = mul(hairEdgePositions[0], g_mViewProj);
	hairEdgePositions[1] = mul(hairEdgePositions[1], g_mViewProj);
	hairEdgePositions[0] = hairEdgePositions[0]/hairEdgePositions[0].w;
	hairEdgePositions[1] = hairEdgePositions[1]/hairEdgePositions[1].w;

    // Write output data
    PS_INPUT_HAIR_AA Output = (PS_INPUT_HAIR_AA)0;
    float fDirIndex = (vertexId & 0x01) ? -1.0 : 1.0;
    Output.Position = (fDirIndex==-1.0 ? hairEdgePositions[0] : hairEdgePositions[1]) + fDirIndex * float4(proj_right * expandPixels / g_WinSize.y, 0.0f, 0.0f);
    Output.Tangent  = float4(t, ratio);
    Output.p0p1     = float4( hairEdgePositions[0].xy, hairEdgePositions[1].xy );

    return Output;
}

//--------------------------------------------------------------------------------------
//
//		Pixel Shader Code
//
//--------------------------------------------------------------------------------------

//--------------------------------------------------------------------------------------
// ComputeHairShading
//
// Hair shading using dual highlight approach and Kajiya lighting model
// dual highlight: marschner 03 
// kajiya model: kajiya 84
//--------------------------------------------------------------------------------------
float3 ComputeHairShading(float3 iPos, float3 iTangent, float4 iTex, float amountLight)
{
    float3 baseColor = g_MatBaseColor.xyz;
    float rand_value = 1;
    
    if(abs(iTex.x) + abs(iTex.y) >1e-5) // if texcoord is available, use texture map
        rand_value = g_txNoise.SampleLevel(g_samLinearWrap, iTex.xy, 0).x;
    
    // define baseColor and Ka Kd Ks coefficient for hair
    float Ka = g_MatKValue.x, Kd = g_MatKValue.y, 
          Ks1 = g_MatKValue.z, Ex1 = g_MatKValue.w,
          Ks2 = g_fHairKs2, Ex2 = g_fHairEx2;

    float3 lightPos = g_PointLightPos.xyz;
    float3 vLightDir = normalize(lightPos - iPos.xyz);
    float3 vEyeDir = normalize(g_vEye.xyz - iPos.xyz);
    float3 tangent = normalize(iTangent);

    // in Kajiya's model: diffuse component: sin(t, l)
    float cosTL = (dot(tangent, vLightDir));
    float sinTL = sqrt(1 - cosTL*cosTL);
    float diffuse = sinTL; // here sinTL is apparently larger than 0

    float alpha = (rand_value * 10) * PI/180; // tiled angle (5-10 dgree)

    // in Kajiya's model: specular component: cos(t, rl) * cos(t, e) + sin(t, rl)sin(t, e)
    float cosTRL = -cosTL;
    float sinTRL = sinTL;
    float cosTE = (dot(tangent, vEyeDir));
    float sinTE = sqrt(1- cosTE*cosTE);

    // primary highlight: reflected direction shift towards root (2 * Alpha)
    float cosTRL_root = cosTRL * cos(2 * alpha) - sinTRL * sin(2 * alpha);
    float sinTRL_root = sqrt(1 - cosTRL_root * cosTRL_root);
    float specular_root = max(0, cosTRL_root * cosTE + sinTRL_root * sinTE);

    // secondary highlight: reflected direction shifted toward tip (3*Alpha)
    float cosTRL_tip = cosTRL*cos(-3*alpha) - sinTRL*sin(-3*alpha);
    float sinTRL_tip = sqrt(1 - cosTRL_tip * cosTRL_tip);
    float specular_tip = max(0, cosTRL_tip * cosTE + sinTRL_tip * sinTE);

    float3 vColor = Ka * g_AmbientLightColor.xyz * baseColor + // ambient
                    amountLight * g_PointLightColor.xyz * (
                    Kd * diffuse * baseColor + // diffuse
                    Ks1 * pow(specular_root, Ex1)  + // primary hightlight r
                    Ks2 * pow(specular_tip, Ex2) * baseColor); // secondary highlight rtr 

   return vColor;
}

//--------------------------------------------------------------------------------------
// SimpleHairShading
//
// Low quality, but faster hair shading
//--------------------------------------------------------------------------------------
float3 SimpleHairShading(float3 iPos, float3 iTangent, float4 iTex, float amountLight)
{
    
    float3 baseColor = g_MatBaseColor.xyz;
 	float Kd = g_MatKValue.y;
   
#ifdef SUPERSIMPLESHADING
	float3 vColor = amountLight * Kd * baseColor;
#else
    // define baseColor and Ka Kd Ks coefficient for hair
    float Ka = g_MatKValue.x;
	float Ks1 = g_MatKValue.z;
	float Ex1 = g_MatKValue.w;
	float Ks2 = g_fHairKs2;
	float Ex2 = g_fHairEx2;

    float3 lightPos = g_PointLightPos.xyz;
    float3 vLightDir = normalize(lightPos - iPos.xyz);
    float3 tangent = normalize(iTangent);

    // in Kajiya's model: diffuse component: sin(t, l)
    float cosTL = (dot(tangent, vLightDir));
    float sinTL = sqrt(1 - cosTL*cosTL);
    float diffuse = sinTL; // here sinTL is apparently larger than 0

    float3 vColor = Ka * g_AmbientLightColor.xyz * baseColor +							// ambient
                    amountLight * g_PointLightColor.xyz * (Kd * diffuse * baseColor);	// diffuse
#endif

    return vColor;
}

//--------------------------------------------------------------------------------------
// ComputeCoverage
//
// Calculate the pixel coverage of a hair strand by computing the hair width
//--------------------------------------------------------------------------------------
float ComputeCoverage(float2 p0, float2 p1, float2 pixelLoc)
{
	// p0, p1, pixelLoc are in d3d clip space (-1 to 1)x(-1 to 1)

	// Scale positions so 1.f = half pixel width
	p0 *= g_WinSize.xy;
	p1 *= g_WinSize.xy;
	pixelLoc *= g_WinSize.xy;

	float p0dist = length(p0 - pixelLoc);
	float p1dist = length(p1 - pixelLoc);
	float hairWidth = length(p0 - p1);
    
	// will be 1.f if pixel outside hair, 0.f if pixel inside hair
	float outside = any( float2(step(hairWidth, p0dist), step(hairWidth, p1dist)) );
	
	// if outside, set sign to -1, else set sign to 1
	float sign = outside > 0.f ? -1.f : 1.f;
	
	// signed distance (positive if inside hair, negative if outside hair)
	float relDist = sign * saturate( min(p0dist, p1dist) );
	
	// returns coverage based on the relative distance
	// 0, if completely outside hair edge
	// 1, if completely inside hair edge
	return (relDist + 1.f) * 0.5f;
}

//--------------------------------------------------------------------------------------
// ComputeShadow
//
// Computes the shadow using a simplified deep shadow map technique for the hair and
// PCF for scene objects. It uses multiple taps to filter over a (KERNEL_SIZE x KERNEL_SIZE)
// kernel for high quality results.
//--------------------------------------------------------------------------------------
float ComputeShadow(float3 worldPos, float alpha, int iTechSM)
{

	if( iTechSM == SHADOW_NONE )
		return 1;

	float4 projPosLight = mul(float4(worldPos,1), g_mViewProjLight);
	float2 texSM = float2(projPosLight.x/projPosLight.w+1, -projPosLight.y/projPosLight.w+1)*0.5;
	float depth = projPosLight.z/projPosLight.w;
	float epsilon = depth * SM_EPSILON;
	float depth_fragment = projPosLight.w;

	// for shadow casted by scene objs, use PCF shadow
	float total_weight = 0;
	float amountLight_hair = 0;	

	total_weight = 0;
	[unroll] for (int dx = (1-KERNEL_SIZE)/2; dx <= KERNEL_SIZE/2; dx++) 
	{ 
		[unroll] for (int dy = (1-KERNEL_SIZE)/2; dy <= KERNEL_SIZE/2; dy++) 
		{ 
			float size = 2.4;
			float sigma = (KERNEL_SIZE/2.0)/size; // standard deviation, when kernel/2 > 3*sigma, it's close to zero, here we use 1.5 instead
			float exp = -1* (dx*dx + dy*dy)/ (2* sigma * sigma);
			float weight = 1/(2*PI*sigma*sigma) * pow(e, exp);

			// shadow casted by hair: simplified deep shadow map
			float depthSMHair = g_txSMHair.SampleLevel( g_samPointClamp, texSM, 0, int2(dx, dy) ).x; //z/w

			float depth_smPoint = g_fNearLight/(1 - depthSMHair*(g_fFarLight - g_fNearLight)/g_fFarLight);

			float depth_range = max(0, depth_fragment-depth_smPoint); 
			float numFibers =  depth_range/(g_FiberSpacing*g_FiberRadius);

			// if occluded by hair, there is at least one fiber
			[flatten]if (depth_range > 1e-5)
				numFibers += 1;
			amountLight_hair += pow(abs(1-alpha), numFibers)*weight;

			total_weight += weight;
		}
	}
	amountLight_hair /= total_weight;

	float amountLight_scene = g_txSMScene.SampleCmpLevelZero(g_samShadow, texSM, depth-epsilon);

	return (amountLight_hair * amountLight_scene);

}

//--------------------------------------------------------------------------------------
// ComputeSimpleShadow
//
// Computes the shadow using a simplified deep shadow map technique for the hair and
// PCF for scene objects. This function only uses one sample, so it is faster but
// not as good quality as ComputeShadow
//--------------------------------------------------------------------------------------
float ComputeSimpleShadow(float3 worldPos, float alpha, int iTechSM)
{
	if( iTechSM == SHADOW_NONE )
	{
		return 1;
	}

	float4 projPosLight = mul(float4(worldPos,1), g_mViewProjLight);

	float2 texSM = 0.5 * float2(projPosLight.x/projPosLight.w+1.0, -projPosLight.y/projPosLight.w+1.0);
	float depth = projPosLight.z/projPosLight.w;
	float epsilon = depth * SM_EPSILON;
	float depth_fragment = projPosLight.w;

	// shadow casted by scene
	float amountLight_scene = g_txSMScene.SampleCmpLevelZero(g_samShadow, texSM, depth-epsilon);

	// shadow casted by hair: simplified deep shadow map
	float depthSMHair = g_txSMHair.SampleLevel( g_samPointClamp, texSM, 0 ).x; //z/w

	float depth_smPoint = g_fNearLight/(1 - depthSMHair*(g_fFarLight - g_fNearLight)/g_fFarLight);

	float depth_range = max(0, depth_fragment-depth_smPoint); 
	float numFibers =  depth_range/(g_FiberSpacing*g_FiberRadius);

	// if occluded by hair, there is at least one fiber
    [flatten]if (depth_range > 1e-5)
		numFibers += 1.0;
	float amountLight_hair = pow(abs(1-alpha), numFibers);

	return amountLight_scene * amountLight_hair;
}

//--------------------------------------------------------------------------------------
// Helper functions for packing and unpacking the stored tangent and coverage
//--------------------------------------------------------------------------------------
uint PackFloat4IntoUint(float4 vValue)
{
    return ( (uint(vValue.x*255)& 0xFFUL) << 24 ) | ( (uint(vValue.y*255)& 0xFFUL) << 16 ) | ( (uint(vValue.z*255)& 0xFFUL) << 8) | (uint(vValue.w * 255)& 0xFFUL);
}

float4 UnpackUintIntoFloat4(uint uValue)
{
    return float4( ( (uValue & 0xFF000000)>>24 ) / 255.0, ( (uValue & 0x00FF0000)>>16 ) / 255.0, ( (uValue & 0x0000FF00)>>8 ) / 255.0, ( (uValue & 0x000000FF) ) / 255.0);
}

uint PackTangentAndCoverage(float3 tangent, float coverage)
{
    return PackFloat4IntoUint( float4(tangent.xyz*0.5 + 0.5, coverage) );
}

float3 GetTangent(uint packedTangent)
{
    return 2.0 * UnpackUintIntoFloat4(packedTangent).xyz - 1.0;
}

float GetCoverage(uint packedCoverage)
{
    return UnpackUintIntoFloat4(packedCoverage).w;
}

//--------------------------------------------------------------------------------------
// StoreFragments_Hair 
//
// Stores the necessary fragment data into the per pixel linked list
//		LinkedListHeadUAV is the UAV that stores an offset to the head of the pixel's linked list
//		LinkedListUAV is the UAV that contains the actual linked list
//
//--------------------------------------------------------------------------------------
void StoreFragments_Hair(uint2 address, float3 tangent, float coverage, float depth)
{
    // Retrieve current pixel count and increase counter
    uint uPixelCount = LinkedListUAV.IncrementCounter();
    uint uOldStartOffset;

    // Exchange indices in LinkedListHead texture corresponding to pixel location 
    InterlockedExchange(LinkedListHeadUAV[address], uPixelCount, uOldStartOffset);  // link head texture

    // Append new element at the end of the Fragment and Link Buffer
    PPLL_STRUCT Element;
	Element.TangentAndCoverage = PackTangentAndCoverage(tangent, coverage);
	Element.depth = asuint(depth);
    Element.uNext = uOldStartOffset;
    LinkedListUAV[uPixelCount] = Element; // buffer that stores the fragments
}

//--------------------------------------------------------------------------------------
// PS_ABuffer_Hair 
//
// This is the first pass pixel shader for rendering hair geometry into an A-buffer. 
// It keeps all of the layers of the transparent hair in a UAV per-pixel linked list.
//
// Coverage for antialiasing is calculated and stored in the linked list along with geometry 
// and attributes necessary for lighting and shadows which are calculated in a subsequent 
// pass by rendering a full screen quad.
//
//--------------------------------------------------------------------------------------
[earlydepthstencil]
float4 PS_ABuffer_Hair( PS_INPUT_HAIR_AA In) : SV_Target
{ 
    // Render AA Line, calculate pixel coverage
    float4 proj_pos = float4(   2*In.Position.x*g_WinSize.z - 1.0,  // g_WinSize.z = 1.0/g_WinSize.x
                                1 - 2*In.Position.y*g_WinSize.w,    // g_WinSize.w = 1.0/g_WinSize.y 
                                1, 
                                1);

    float4 original_pos = mul(proj_pos, g_mInvViewProj);
    
    float curve_scale = 1;
    if (g_bThinTip > 0 )
        curve_scale = In.Tangent.w;
    
    float fiber_radius = curve_scale * g_FiberRadius;
	
	float coverage = 1.f;
	if(g_bUseCoverage)
	{	
        coverage = ComputeCoverage(In.p0p1.xy, In.p0p1.zw, proj_pos.xy);
	}

	coverage *= g_FiberAlpha;

    // only store fragments with non-zero alpha value
    if (coverage > g_alphaThreshold) // ensure alpha is at least as much as the minimum alpha value
    {
        StoreFragments_Hair(In.Position.xy, In.Tangent.xyz, coverage, In.Position.z);
    }
    // output a mask RT for final pass    
    return float4(1, 0, 0, 0);
}


#ifdef ALU_INDEXING
//--------------------------------------------------------------------------------------
// 
// Helper functions for storing the k-buffer into non-indexed registers for better 
// performance. For this code to work, KBUFFER_SIZE must be <= 16.
//
//--------------------------------------------------------------------------------------

uint GetUintFromIndex_Size16(uint4 V03, uint4 V47, uint4 V811, uint4 V1215, uint uIndex)
{
    uint u;
    u = uIndex==1 ? V03.y : V03.x;
    u = uIndex==2 ? V03.z : u;
    u = uIndex==3 ? V03.w : u;
    u = uIndex==4 ? V47.x : u;
    u = uIndex==5 ? V47.y : u;
    u = uIndex==6 ? V47.z : u;
    u = uIndex==7 ? V47.w : u;
    u = uIndex==8 ? V811.x : u;
    u = uIndex==9 ? V811.y : u;
    u = uIndex==10 ? V811.z : u;
    u = uIndex==11 ? V811.w : u;
    u = uIndex==12 ? V1215.x : u;
    u = uIndex==13 ? V1215.y : u;
    u = uIndex==14 ? V1215.z : u;
    u = uIndex==15 ? V1215.w : u;
    return u;
}

void StoreUintAtIndex_Size16(inout uint4 V03, inout uint4 V47, inout uint4 V811, inout uint4 V1215, uint uIndex, uint uValue)
{
    V03.x = (uIndex==0) ? uValue : V03.x;
    V03.y = (uIndex==1) ? uValue : V03.y;
    V03.z = (uIndex==2) ? uValue : V03.z;
    V03.w = (uIndex==3) ? uValue : V03.w;
    V47.x = (uIndex==4) ? uValue : V47.x;
    V47.y = (uIndex==5) ? uValue : V47.y;
    V47.z = (uIndex==6) ? uValue : V47.z;
    V47.w = (uIndex==7) ? uValue : V47.w;
    V811.x = (uIndex==8) ? uValue : V811.x;
    V811.y = (uIndex==9) ? uValue : V811.y;
    V811.z = (uIndex==10) ? uValue : V811.z;
    V811.w = (uIndex==11) ? uValue : V811.w;
    V1215.x = (uIndex==12) ? uValue : V1215.x;
    V1215.y = (uIndex==13) ? uValue : V1215.y;
    V1215.z = (uIndex==14) ? uValue : V1215.z;
    V1215.w = (uIndex==15) ? uValue : V1215.w;
}
#endif

//--------------------------------------------------------------------------------------
// PS_KBuffer_Hair
//
// Second pass pixel shader which selects the nearest k fragments (k-buffer) and renders
// then in depth order for correct transparent blending of the hair. It uses the per-pixel 
// linked list to access all of the layers of hair fragment so that the nearest k fragments
// are rendered at a higher quality than the remaining fragments. The lighting and shadows 
// are calculated in screen space using the tangent and depth stored in the per-pixel linked
// list, similar to deferred shading.
//
//--------------------------------------------------------------------------------------
[earlydepthstencil]
float4 PS_KBuffer_Hair(VS_OUTPUT_SCREENQUAD In): SV_Target
{
    float4 fcolor = float4(0,0,0,1);
    float amountLight;
    float lightIntensity;
	float4 fragmentColor;
	float4 vWorldPosition;
	float3 vTangent;
	float coverage;
	uint tangentAndCoverage;

	// get the start of the linked list from the head pointer
	uint pointer = LinkedListHeadSRV[In.vPosition.xy];

    // A local Array to store the top k fragments(depth and color), where k = KBUFFER_SIZE
#ifdef ALU_INDEXING

    uint4 kBufferDepthV03, kBufferDepthV47, kBufferDepthV811, kBufferDepthV1215;
    uint4 kBufferPackedTangentV03, kBufferPackedTangentV47, kBufferPackedTangentV811, kBufferPackedTangentV1215;
    kBufferDepthV03 = uint4(asuint(100000.0f), asuint(100000.0f), asuint(100000.0f), asuint(100000.0f)); 
    kBufferDepthV47 = uint4(asuint(100000.0f), asuint(100000.0f), asuint(100000.0f), asuint(100000.0f)); 
    kBufferDepthV811 = uint4(asuint(100000.0f), asuint(100000.0f), asuint(100000.0f), asuint(100000.0f)); 
    kBufferDepthV1215 = uint4(asuint(100000.0f), asuint(100000.0f), asuint(100000.0f), asuint(100000.0f)); 
    kBufferPackedTangentV03 = uint4(0,0,0,0);
    kBufferPackedTangentV47 = uint4(0,0,0,0);
    kBufferPackedTangentV811 = uint4(0,0,0,0);
    kBufferPackedTangentV1215 = uint4(0,0,0,0);

    // Get the first k elements in the linked list
    int nNumFragments = 0;
    for(int p=0; p<KBUFFER_SIZE; p++)
    {
        if (pointer != NULLPOINTER)
        {
            StoreUintAtIndex_Size16(kBufferDepthV03, kBufferDepthV47, kBufferDepthV811, kBufferDepthV1215, p, LinkedListSRV[pointer].depth);
            StoreUintAtIndex_Size16(kBufferPackedTangentV03, kBufferPackedTangentV47, kBufferPackedTangentV811, kBufferPackedTangentV1215, p, LinkedListSRV[pointer].TangentAndCoverage);
            pointer = LinkedListSRV[pointer].uNext;
#ifdef COLORDEBUG
            nNumFragments++;
#endif
        }
    }

#else

    KBuffer_STRUCT kBuffer[KBUFFER_SIZE];

	[unroll]for(int t=0; t<KBUFFER_SIZE; t++)
	{
        kBuffer[t].depthAndPackedtangent.x = asuint(100000.0f);	// must be larger than the maximum possible depth value
        kBuffer[t].depthAndPackedtangent.y = 0;
	}

    // Get the first k elements in the linked list
    int nNumFragments = 0;
    for(int p=0; p<KBUFFER_SIZE; p++)
    {
        if (pointer != NULLPOINTER)
        {
            kBuffer[p].depthAndPackedtangent.x	= LinkedListSRV[pointer].depth;
            kBuffer[p].depthAndPackedtangent.y	= LinkedListSRV[pointer].TangentAndCoverage;
            pointer								= LinkedListSRV[pointer].uNext;
#ifdef COLORDEBUG
            nNumFragments++;
#endif
        }
    }

#endif
	
    // Go through the rest of the linked list, and keep the closest k fragments, but not in sorted order.
    [allow_uav_condition]
    for(int l=0; l < g_iMaxFragments; l++)
    {
        if(pointer == NULLPOINTER)	break;

#ifdef COLORDEBUG
        nNumFragments++;
#endif

        int id = 0;
        float max_depth = 0;

		// find the furthest node in array
        [unroll]for(int i=0; i<KBUFFER_SIZE; i++)
        {	
#ifdef ALU_INDEXING
            float fDepth = asfloat(GetUintFromIndex_Size16(kBufferDepthV03, kBufferDepthV47, kBufferDepthV811, kBufferDepthV1215, i));
#else
			float fDepth = asfloat(kBuffer[i].depthAndPackedtangent.x);
#endif
            if(max_depth < fDepth)
            {
                max_depth = fDepth;
                id = i;
            }
        }

        uint nodePackedTangent = LinkedListSRV[pointer].TangentAndCoverage;
		uint nodeDepth         = LinkedListSRV[pointer].depth;
		float fNodeDepth       = asfloat(nodeDepth);

        // If the node in the linked list is nearer than the furthest one in the local array, exchange the node 
        // in the local array for the one in the linked list.
        if (max_depth > fNodeDepth)
        {
#ifdef ALU_INDEXING
            uint tmp								= GetUintFromIndex_Size16(kBufferDepthV03, kBufferDepthV47, kBufferDepthV811, kBufferDepthV1215, id);
            StoreUintAtIndex_Size16(kBufferDepthV03, kBufferDepthV47, kBufferDepthV811,  kBufferDepthV1215, id, nodeDepth);
            fNodeDepth								= asfloat(tmp);
            tmp										= GetUintFromIndex_Size16(kBufferPackedTangentV03, kBufferPackedTangentV47, kBufferPackedTangentV811, kBufferPackedTangentV1215, id);
            StoreUintAtIndex_Size16(kBufferPackedTangentV03, kBufferPackedTangentV47, kBufferPackedTangentV811, kBufferPackedTangentV1215, id, nodePackedTangent);
			nodePackedTangent						= tmp;
#else
            uint tmp								= kBuffer[id].depthAndPackedtangent.x;
            kBuffer[id].depthAndPackedtangent.x	= nodeDepth;
            fNodeDepth								= asfloat(tmp);
            tmp										= kBuffer[id].depthAndPackedtangent.y;
            kBuffer[id].depthAndPackedtangent.y	= nodePackedTangent;
			nodePackedTangent						= tmp;
#endif
        }

        // Do simple shading and out of order blending for nodes that are not part of the k closest fragments
        vWorldPosition = mul(float4(In.vPosition.xy, fNodeDepth, 1), g_mInvViewProjViewport);
		vWorldPosition.xyz /= vWorldPosition.www;

#ifdef SIMPLESHADOWING
		amountLight = ComputeSimpleShadow(vWorldPosition.xyz, g_HairShadowAlpha, g_iTechSM);
#else
        amountLight = ComputeShadow(vWorldPosition.xyz, g_HairShadowAlpha, g_iTechSM);
#endif

        fragmentColor.w = GetCoverage(nodePackedTangent);
        vTangent = GetTangent(nodePackedTangent);
		
#ifdef SIMPLESHADING
        fragmentColor.xyz = SimpleHairShading( vWorldPosition.xyz, vTangent, float4(0,0,0,0), amountLight);
#else
		fragmentColor.xyz = ComputeHairShading( vWorldPosition.xyz, vTangent, float4(0,0,0,0), amountLight);
#endif
        
        // Blend the fragment color
        fcolor.xyz = mad(-fcolor.xyz, fragmentColor.w, fcolor.xyz) + fragmentColor.xyz * fragmentColor.w;
		fcolor.w = mad(-fcolor.w, fragmentColor.w, fcolor.w);

        // Retrieve next node pointer
        pointer = LinkedListSRV[pointer].uNext;
    }


    // Blend the k nearest layers of fragments from back to front, where k = KBUFFER_SIZE
    for(int j=0; j<KBUFFER_SIZE; j++)
    {
        int id = 0;
        float max_depth = 0;
		float initialized = 1;

		// find the furthest node in the array
        for(int i=0; i<KBUFFER_SIZE; i++)
        {
#ifdef ALU_INDEXING
            float fDepth = asfloat(GetUintFromIndex_Size16(kBufferDepthV03, kBufferDepthV47, kBufferDepthV811, kBufferDepthV1215, i));
#else
			float fDepth = asfloat(kBuffer[i].depthAndPackedtangent.x);
#endif
            if(max_depth < fDepth)
            {
                max_depth = fDepth;
                id = i;
            }
        }


        // take this node out of the next search
#ifdef ALU_INDEXING
        uint nodePackedTangent = GetUintFromIndex_Size16(kBufferPackedTangentV03, kBufferPackedTangentV47, kBufferPackedTangentV811, kBufferPackedTangentV1215, id);
        uint nodeDepth         = GetUintFromIndex_Size16(kBufferDepthV03, kBufferDepthV47, kBufferDepthV811, kBufferDepthV1215, id);

        StoreUintAtIndex_Size16(kBufferDepthV03, kBufferDepthV47, kBufferDepthV811, kBufferDepthV1215, id, 0);
#else
		uint nodePackedTangent = kBuffer[id].depthAndPackedtangent.y;
        uint nodeDepth         = kBuffer[id].depthAndPackedtangent.x;

		// take this node out of the next search
        kBuffer[id].depthAndPackedtangent.x = 0;
#endif

		// Use high quality shading for the nearest k fragments
		float fDepth = asfloat(nodeDepth);
        vWorldPosition = mul(float4(In.vPosition.xy, fDepth, 1), g_mInvViewProjViewport);
		vWorldPosition.xyz /= vWorldPosition.www;

		amountLight = ComputeShadow(vWorldPosition.xyz, g_HairShadowAlpha, g_iTechSM); 

        // Get tangent and coverage
        vTangent        = GetTangent(nodePackedTangent);
        fragmentColor.w = GetCoverage(nodePackedTangent);

        // Shading
		fragmentColor.xyz = ComputeHairShading( vWorldPosition.xyz, vTangent, float4(0,0,0,0), amountLight);

		// Blend the fragment color
        fcolor.xyz = mad(-fcolor.xyz, fragmentColor.w, fcolor.xyz) + fragmentColor.xyz * fragmentColor.w;
		fcolor.w = mad(-fcolor.w, fragmentColor.w, fcolor.w);
    }


#ifdef COLORDEBUG
    fcolor.xyz = float3(0,1,0);
    if (nNumFragments>32) fcolor.xyz = float3(1,1,0);
    if (nNumFragments>64) fcolor.xyz = float3(1,0.5,0);
    if (nNumFragments>128) fcolor.xyz = float3(1,0,0);
#endif

    return fcolor;
}
