//--------------------------------------------------------------------------------------
// File: TresFXSimulation.hlsl
//
// Physics simulation of hair using compute shaders 
// 
// Copyright � AMD Corporation. All rights reserved.
//--------------------------------------------------------------------------------------

//constants that change frame to frame
cbuffer ConstBufferCS_Per_Frame : register( b0 )
{
    row_major float4x4 g_ModelTransformForHead;
    row_major float4x4 g_ModelPrevInvTransformForHead;

    float4 g_ModelRotateForHead; // quaternion

	float4 g_Wind;
	float4 g_Wind1;
	float4 g_Wind2;
	float4 g_Wind3;

    int g_NumLengthConstraintIterations;
    int g_bCollision;

    float g_GravityMagnitude;
    float g_TimeStep;
        
    float g_Damping0;
    float g_StiffnessForLocalShapeMatching0;
    float g_StiffnessForGlobalShapeMatching0;
    float g_GlobalShapeMatchingEffectiveRange0;
    
    float g_Damping1;
    float g_StiffnessForLocalShapeMatching1;
    float g_StiffnessForGlobalShapeMatching1;
    float g_GlobalShapeMatchingEffectiveRange1;

    float g_Damping2;
    float g_StiffnessForLocalShapeMatching2;
    float g_StiffnessForGlobalShapeMatching2;
    float g_GlobalShapeMatchingEffectiveRange2;
    
    float g_Damping3;
    float g_StiffnessForLocalShapeMatching3;
    float g_StiffnessForGlobalShapeMatching3;	
    float g_GlobalShapeMatchingEffectiveRange3;
}

cbuffer ConstBufferCS_Collision_Capsule : register( b3 )
{
	float3	g_cc0_center;
	float	g_cc0_radius;
	float3	g_cc1_center;
	float	g_cc1_radius;
	float3	g_cc2_center;
	float	g_cc2_radius;
	float	g_cc0_radius2;
	float	g_cc1_radius2;
	float	g_cc2_radius2;
	float	pad;
}


RWStructuredBuffer<float4> g_HairVertexPositions		: register(u0); 
RWStructuredBuffer<float4> g_HairVertexPositionsPrev	: register(u1); 
RWStructuredBuffer<float4> g_HairVertexTangents			: register(u2); 
RWStructuredBuffer<float4> g_InitialHairPositions		: register(u3);
RWStructuredBuffer<float4> g_GlobalRotations			: register(u4); 
RWStructuredBuffer<float4> g_LocalRotations				: register(u5); 

Buffer<int>		g_HairVerticesOffsetsSRV	: register(t0); 
Buffer<float>	g_HairRestLengthSRV			: register(t1);
Buffer<int>		g_HairStrandType			: register(t2); 
Buffer<float3>	g_HairRefVecsInLocalFrame	: register(t3); 

// If you change the value below, you must change it in TressFXMesh.h as well. 
#define THREAD_GROUP_SIZE 64

groupshared float4 sharedPos[THREAD_GROUP_SIZE];
groupshared float  sharedLength[THREAD_GROUP_SIZE];

//--------------------------------------------------------------------------------------
// 
//	Helper Functions for the main simulation shaders
//
//--------------------------------------------------------------------------------------
bool IsMovable(float4 particle)
{
    if ( particle.w > 0 )
        return true;
    return false;      
}

float2 ConstraintMultiplier(float4 particle0, float4 particle1)
{
    if (IsMovable(particle0)) 
    {
        if (IsMovable(particle1))
            return float2(0.5, 0.5);
        else
            return float2(1, 0);
    }
    else 
    {
        if (IsMovable(particle1))
            return float2(0, 1);
        else
            return float2(0, 0);
    }    
}

float4 MakeQuaternion(float angle_radian, float3 axis)
{
    // create quaternion using angle and rotation axis
    float4 quaternion;
    float halfAngle = 0.5f * angle_radian;
    float sinHalf = sin(halfAngle);

    quaternion.w = cos(halfAngle);
    quaternion.xyz = sinHalf * axis.xyz;

    return quaternion;
}

float4 InverseQuaternion(float4 q)
{
    float lengthSqr = q.x*q.x + q.y*q.y + q.z*q.z + q.w*q.w;

    if ( lengthSqr < 0.001 )
        return float4(0, 0, 0, 1.0f);

    q.x = -q.x / lengthSqr;
    q.y = -q.y / lengthSqr;
    q.z = -q.z / lengthSqr;
    q.w = q.w / lengthSqr;

    return q;
}

float3 MultQuaternionAndVector(float4 q, float3 v)
{
    float3 uv, uuv;
    float3 qvec = float3(q.x, q.y, q.z);
    uv = cross(qvec, v);
    uuv = cross(qvec, uv);
    uv *= (2.0f * q.w);
    uuv *= 2.0f;

    return v + uv + uuv;
}

float4 MultQuaternionAndQuaternion(float4 qA, float4 qB)
{
    float4 q;

    q.w = qA.w * qB.w - qA.x * qB.x - qA.y * qB.y - qA.z * qB.z;
    q.x = qA.w * qB.x + qA.x * qB.w + qA.y * qB.z - qA.z * qB.y;
    q.y = qA.w * qB.y + qA.y * qB.w + qA.z * qB.x - qA.x * qB.z;
    q.z = qA.w * qB.z + qA.z * qB.w + qA.x * qB.y - qA.y * qB.x;
    
    return q;
}

void ApplyDistanceConstraint(inout float4 pos0, inout float4 pos1, float targetDistance, float stiffness = 1.0)
{
    float3 delta = pos1.xyz - pos0.xyz;
    float distance = max(length(delta), 1e-7);
    float stretching = 1 - targetDistance / distance;
    delta = stretching * delta;
    float2 multiplier = ConstraintMultiplier(pos0, pos1);
    
    pos0.xyz += multiplier[0] * delta * stiffness;
    pos1.xyz -= multiplier[1] * delta * stiffness;
}


//--------------------------------------------------------------------------------------
// 
//	Integrate
//
//  Uses Verlet integration to calculate the new position for the current time step
//
//--------------------------------------------------------------------------------------
float4 Integrate(float4 curPosition, float4 oldPosition, float4 initialPos, float4 force, int globalVertexIndex, int localVertexIndex, int numVerticesInTheStrand, float dampingCoeff = 1.0f)
{  
    float4 outputPos = curPosition;

    force.xyz += g_GravityMagnitude * float3(0, -1.0f, 0);
    outputPos.xyz = curPosition.xyz + (1.0 - dampingCoeff)*(curPosition.xyz - oldPosition.xyz) + force.xyz*g_TimeStep*g_TimeStep; 
    
    return outputPos;  
}



struct CollisionCapsule
{
	float4 p1; // xyz = position 1 of capsule, w = radius
	float4 p2; // xyz = position 2 of capsule, w = radius * radius
};

//--------------------------------------------------------------------------------------
// 
//	CapsuleCollision
//
//  Moves the position based on collision with capsule
//
//--------------------------------------------------------------------------------------
float3 CapsuleCollision(float4 curPosition, float4 oldPosition, CollisionCapsule cc, float friction = 0.4f)
{
	float3 newPos = curPosition.xyz;
	const float radius = cc.p1.w;
	const float radius2 = cc.p2.w;
		
	if ( !IsMovable(curPosition) )
		return newPos;
			
	float3 segment = cc.p2.xyz - cc.p1.xyz;
	float3 delta1 = curPosition.xyz - cc.p1.xyz;
	float3 delta2 = cc.p2.xyz - curPosition.xyz;
		
	float dist1 = dot(delta1, segment);
	float dist2 = dot(delta2, segment);
		
	// colliding with sphere 1
	if ( dist1 < 0.f )
	{
		if ( dot(delta1, delta1) < radius2 )
		{
			float3 n = normalize(delta1);
			newPos = radius * n + cc.p1.xyz;
		}
		return newPos;
	}
		
	// colliding with sphere 2
	if ( dist2 < 0.f )
	{
		if ( dot(delta2, delta2) < radius2 )
		{
			float3 n = normalize(-delta2);
			newPos = radius * n + cc.p2.xyz;
		}
		return newPos;
	}
		
	// colliding with middle cylinder
	float3 x = (dist1 * cc.p2.xyz + dist2 * cc.p1.xyz) / (dist1 + dist2);
	float3 delta = curPosition.xyz - x;
		
	if ( dot(delta, delta) < radius2 )
	{
		float3 n = normalize(delta);
		float3 vec = curPosition.xyz - oldPosition.xyz;
		float3 segN = normalize(segment);
		float3 vecTangent = dot(vec, segN) * segN;
		float3 vecNormal = vec - vecTangent;
		newPos.xyz = oldPosition.xyz + friction * vecTangent + (vecNormal + radius * n - delta);
	}
	return newPos;
}

//--------------------------------------------------------------------------------------
// 
//	UpdateFinalVertexPositions
//
//  Updates the  hair vertex positions based on the physics simulation
//
//--------------------------------------------------------------------------------------
void UpdateFinalVertexPositions(float4 oldPosition, float4 newPosition, int globalVertexIndex, int localVertexIndex, int numVerticesInTheStrand)
{ 
	if ( localVertexIndex < numVerticesInTheStrand )
	{
		g_HairVertexPositionsPrev[globalVertexIndex] = oldPosition;        
		g_HairVertexPositions[globalVertexIndex] = newPosition;
	}        
}

//--------------------------------------------------------------------------------------
// 
//	SkipSimulateHair
//
//  Compute shader to skip the physics simulation and just transform the hair according
//  to the head transformation.
//
//  The number of vertices in each strand must be less than or equal to THREAD_GROUP_SIZE 
//	because each thread group computes one strand. 
//
//--------------------------------------------------------------------------------------
[numthreads(THREAD_GROUP_SIZE, 1, 1)]
void SkipSimulateHair(uint GIndex : SV_GroupIndex,
                  uint3 GId : SV_GroupID,
                  uint3 DTid : SV_DispatchThreadID)
{
    int offset = 0;
    int strandType = 0;

    uint globalStrandIndex = GId.x;

    if( globalStrandIndex > 0 ) 
    {
        offset =  g_HairVerticesOffsetsSRV.Load(globalStrandIndex - 1);
    }

    uint numVerticesInTheStrand = g_HairVerticesOffsetsSRV.Load(globalStrandIndex) - offset;
 
    uint localVertexIndex = GIndex;
    uint globalVertexIndex = offset + localVertexIndex;

	if (localVertexIndex >= numVerticesInTheStrand )
		return;

	float4 oldPosition = g_HairVertexPositionsPrev[globalVertexIndex];
	float4 newPosition = g_HairVertexPositions[globalVertexIndex];
	
	oldPosition.xyz = mul(float4( oldPosition.xyz, 1), g_ModelPrevInvTransformForHead).xyz;
	oldPosition.xyz = mul(float4( oldPosition.xyz, 1), g_ModelTransformForHead).xyz;
	
	newPosition.xyz = mul(float4( newPosition.xyz, 1), g_ModelPrevInvTransformForHead).xyz;
	newPosition.xyz = mul(float4( newPosition.xyz, 1), g_ModelTransformForHead).xyz;
	
	// Update final global position buffers
	UpdateFinalVertexPositions(oldPosition, newPosition, globalVertexIndex, localVertexIndex, numVerticesInTheStrand);
		
	return;
}

//--------------------------------------------------------------------------------------
// 
//	IntegrationAndGlobalShapeConstraints
//
//  Compute shader to simulate the gravitational force with integration and to maintain the
//  global shape constraints.
//
// One thread computes one vertex.
//
//--------------------------------------------------------------------------------------
[numthreads(THREAD_GROUP_SIZE, 1, 1)]
void IntegrationAndGlobalShapeConstraints(uint GIndex : SV_GroupIndex,
                  uint3 GId : SV_GroupID,
                  uint3 DTid : SV_DispatchThreadID)
{
    int offset = 0;
    int strandType = 0;

	uint globalStrandIndex = 0;
	uint localVertexIndex = 0; 
	uint indexForSharedMem = GIndex;
	uint numOfStrandsPerThreadGroup = 2;
	uint maxPossibleVertsInStrand = (THREAD_GROUP_SIZE / numOfStrandsPerThreadGroup);

	if ( GIndex < maxPossibleVertsInStrand )
	{
		globalStrandIndex = 2 * GId.x;
		localVertexIndex = GIndex;
	}
	else
	{
		globalStrandIndex = 2 * GId.x + 1;
		localVertexIndex = GIndex - maxPossibleVertsInStrand;
	}
	
    if( globalStrandIndex > 0 ) 
    {
        offset =  g_HairVerticesOffsetsSRV.Load(globalStrandIndex - 1);
        strandType = g_HairStrandType.Load(globalStrandIndex);
    }

	uint globalVertexIndex = offset + localVertexIndex;
    uint numVerticesInTheStrand = g_HairVerticesOffsetsSRV.Load(globalStrandIndex) - offset;

	if ( numVerticesInTheStrand > maxPossibleVertsInStrand )
		numVerticesInTheStrand = maxPossibleVertsInStrand;

    float4 currentPos = float4(0, 0, 0, 0); // position when this step starts. In other words, a position from the last step. 
    float4 initialPos = float4(0, 0, 0, 0); // rest position

    //------------------------------
    // Copy data into shared memory 
    //------------------------------
    if (localVertexIndex < numVerticesInTheStrand )
    {
        currentPos = sharedPos[indexForSharedMem] = g_HairVertexPositions[globalVertexIndex];
        initialPos = g_InitialHairPositions[globalVertexIndex];
        initialPos.xyz = mul(float4( initialPos.xyz, 1), g_ModelTransformForHead).xyz;
    }
        
    GroupMemoryBarrierWithGroupSync();

    //-----------
    // Integrate
    //-----------
    if ( localVertexIndex < numVerticesInTheStrand ) 
    {  
        float dampingCoeff = 0;

        if ( strandType == 0 )
            dampingCoeff = g_Damping0;
        else if ( strandType == 1 ) 
            dampingCoeff = g_Damping1;
        else if ( strandType == 2 ) 
            dampingCoeff = g_Damping2;
        else if ( strandType == 3 )
            dampingCoeff = g_Damping3;

        float4 oldPos = g_HairVertexPositionsPrev[globalVertexIndex];
        float4 force = float4(0, 0, 0, 0);

        if ( IsMovable(currentPos) )  
            sharedPos[indexForSharedMem] = Integrate(currentPos, oldPos, initialPos, force, globalVertexIndex, localVertexIndex, numVerticesInTheStrand, dampingCoeff); 
        else
            sharedPos[indexForSharedMem] = initialPos;
    }    

    //--------------------------------------------
    // Global shaping matching style enforcement
    //--------------------------------------------
    float stiffnessForGlobalShapeMatching = 0;
    float globalShapeMatchingEffectiveRange = 0;

    if ( strandType == 0 ) 
    {
        stiffnessForGlobalShapeMatching = g_StiffnessForGlobalShapeMatching0;
        globalShapeMatchingEffectiveRange = g_GlobalShapeMatchingEffectiveRange0;
    }
    else if ( strandType == 1 ) 
    {
        stiffnessForGlobalShapeMatching = g_StiffnessForGlobalShapeMatching1;
        globalShapeMatchingEffectiveRange = g_GlobalShapeMatchingEffectiveRange1;
    }
    else if ( strandType == 2 ) 
    {
        stiffnessForGlobalShapeMatching = g_StiffnessForGlobalShapeMatching2;
        globalShapeMatchingEffectiveRange = g_GlobalShapeMatchingEffectiveRange2;
    }
    else if ( strandType == 3 ) 
    {
        stiffnessForGlobalShapeMatching = g_StiffnessForGlobalShapeMatching3;
        globalShapeMatchingEffectiveRange = g_GlobalShapeMatchingEffectiveRange3;
    }
    
    if ( stiffnessForGlobalShapeMatching > 0 && globalShapeMatchingEffectiveRange )
    {
        if ( IsMovable(sharedPos[indexForSharedMem]) )  
        {
            if ( (float)localVertexIndex < globalShapeMatchingEffectiveRange * (float)numVerticesInTheStrand )
            {
                float factor = stiffnessForGlobalShapeMatching;
                float3 del = factor * (initialPos - sharedPos[indexForSharedMem]).xyz;
                sharedPos[indexForSharedMem].xyz += del;
            }
        }
    }

	//---------------------------------------
    // update global position buffers
    //---------------------------------------
    UpdateFinalVertexPositions(currentPos, sharedPos[indexForSharedMem], globalVertexIndex, localVertexIndex, numVerticesInTheStrand);
}

//--------------------------------------------------------------------------------------
// 
//	LocalShapeConstraints
//
//  Compute shader to maintain the local shape constraints.
//
// One thread computes one strand.
//
//--------------------------------------------------------------------------------------
[numthreads(THREAD_GROUP_SIZE, 1, 1)]
void LocalShapeConstraints(uint GIndex : SV_GroupIndex,
                  uint3 GId : SV_GroupID,
                  uint3 DTid : SV_DispatchThreadID)
{
    uint globalStrandIndex = THREAD_GROUP_SIZE*GId.x;
	globalStrandIndex += GIndex;

	int offset = 0;
    int strandType = 0;

	if ( globalStrandIndex > 0 )
    {
        offset =  g_HairVerticesOffsetsSRV.Load(globalStrandIndex - 1);
        strandType = g_HairStrandType.Load(globalStrandIndex);
    }

    uint numVerticesInTheStrand = g_HairVerticesOffsetsSRV.Load(globalStrandIndex) - offset;

	// stiffness for local shape constraints
	float stiffnessForLocalShapeMatching = 0;

	if ( strandType == 2 ) 
		stiffnessForLocalShapeMatching = g_StiffnessForLocalShapeMatching2;
	else if ( strandType == 3 ) 
		stiffnessForLocalShapeMatching = g_StiffnessForLocalShapeMatching3;
	else if ( strandType == 1 ) 
		stiffnessForLocalShapeMatching = g_StiffnessForLocalShapeMatching1;
	else if ( strandType == 0 ) 
		stiffnessForLocalShapeMatching = g_StiffnessForLocalShapeMatching0;

	// 1.0 for stiffness makes things unstable sometimes.
	if ( stiffnessForLocalShapeMatching >= 0.95f )
		stiffnessForLocalShapeMatching = 0.95f;

	//--------------------------------------------
	// Local shape constraint for bending/twisting 
	//--------------------------------------------    
	{
		float4 pos_minus_one = g_HairVertexPositions[offset];
		float4 pos = g_HairVertexPositions[offset+1];
		float4 pos_plus_one;
		uint globalVertexIndex = 0;
		float4 rotGlobal = g_GlobalRotations[offset];
	
		for ( uint localVertexIndex = 1; localVertexIndex < numVerticesInTheStrand-1; localVertexIndex++ )
		{
			globalVertexIndex = offset + localVertexIndex;
			pos_plus_one = g_HairVertexPositions[globalVertexIndex+1];

			//--------------------------------
			// Update position i and i_plus_1
			//--------------------------------
			float4 rotGlobalWorld = MultQuaternionAndQuaternion(g_ModelRotateForHead, rotGlobal); 

			float3 orgPos_i_plus_1_InLocalFrame_i = g_HairRefVecsInLocalFrame[globalVertexIndex+1];	
			float3 orgPos_i_plus_1_InGlobalFrame = MultQuaternionAndVector(rotGlobalWorld, orgPos_i_plus_1_InLocalFrame_i) + pos.xyz;

			float3 del = stiffnessForLocalShapeMatching * 0.5f * (orgPos_i_plus_1_InGlobalFrame - pos_plus_one.xyz).xyz;

			if ( IsMovable(pos) )
				pos.xyz -= del.xyz;

			if ( IsMovable(pos_plus_one) )
				pos_plus_one.xyz += del.xyz;
	
			//---------------------------
			// Update local/global frames
			//---------------------------
			float4 invRotGlobalWorld = InverseQuaternion(rotGlobalWorld);	
			float3 vec = normalize(pos_plus_one.xyz - pos.xyz);		
    
			float3 x_i_plus_1_frame_i = normalize(MultQuaternionAndVector(invRotGlobalWorld, vec));
			float3 e = float3(1.0f, 0, 0);
			float3 rotAxis = cross(e, x_i_plus_1_frame_i);
    
			if ( length(rotAxis) > 0.001 )
			{
				float angle_radian = acos(dot(e, x_i_plus_1_frame_i));
				rotAxis = normalize(rotAxis);

				float4 localRot = MakeQuaternion(angle_radian, rotAxis);
				rotGlobal = MultQuaternionAndQuaternion(rotGlobal, localRot);
			}	

			g_HairVertexPositions[globalVertexIndex].xyz = pos.xyz;			
			g_HairVertexPositions[globalVertexIndex+1].xyz = pos_plus_one.xyz;

			pos_minus_one = pos;
			pos = pos_plus_one;
		}	  
	}

	return;
}

//--------------------------------------------------------------------------------------
// 
//	LengthConstriantsAndWind
//
//  Compute shader to move the vertex position based on wind and maintains the lenght constraints.
//
// One thread computes one vertex.
//
//--------------------------------------------------------------------------------------
[numthreads(THREAD_GROUP_SIZE, 1, 1)]
void LengthConstriantsAndWind(uint GIndex : SV_GroupIndex,
                  uint3 GId : SV_GroupID,
                  uint3 DTid : SV_DispatchThreadID)
{
	int offset = 0;

	uint globalStrandIndex = 0;
	uint localVertexIndex = 0; 
	uint indexForSharedMem = GIndex;
	uint numOfStrandsPerThreadGroup = 2;
	uint maxPossibleVertsInStrand = (THREAD_GROUP_SIZE / numOfStrandsPerThreadGroup);
	uint offsetForLocalVertexIndex = 0;

	// With offsetForLocalVertexIndex, we can convert localVertexIndex into indexForSharedMem by doing below.
	// indexForSharedMem = localVertexIndex + offsetForLocalVertexIndex

	if ( GIndex < maxPossibleVertsInStrand )
	{
		globalStrandIndex = 2 * GId.x;
		localVertexIndex = GIndex;
		offsetForLocalVertexIndex = 0;
	}
	else
	{
		globalStrandIndex = 2 * GId.x + 1;
		localVertexIndex = GIndex - maxPossibleVertsInStrand;
		offsetForLocalVertexIndex = maxPossibleVertsInStrand;
	}
	
    if( globalStrandIndex > 0 ) 
    {
        offset =  g_HairVerticesOffsetsSRV.Load(globalStrandIndex - 1);
    }

	uint globalVertexIndex = offset + localVertexIndex;
    uint numVerticesInTheStrand = g_HairVerticesOffsetsSRV.Load(globalStrandIndex) - offset;

	if ( numVerticesInTheStrand > maxPossibleVertsInStrand )
		numVerticesInTheStrand = maxPossibleVertsInStrand;
				
    //------------------------------
    // Copy data into shared memory
    //------------------------------
    if (localVertexIndex < numVerticesInTheStrand )
    {
        sharedPos[indexForSharedMem] = g_HairVertexPositions[globalVertexIndex];
        sharedLength[indexForSharedMem] = g_HairRestLengthSRV[globalVertexIndex]; 
    }
        
    GroupMemoryBarrierWithGroupSync();

	//------------
	// Wind
	//------------
	if ( g_Wind.x != 0 || g_Wind.y != 0 || g_Wind.z != 0 )
	{  
		float4 force = float4(0, 0, 0, 0);

		float frame = g_Wind.w;

		if ( localVertexIndex >= 2 && localVertexIndex < numVerticesInTheStrand-1 )
		{
			// combining four winds.
			float a = ((float)(globalStrandIndex % 20))/20.0f;
			float3	w = a*g_Wind.xyz + (1.0f-a)*g_Wind1.xyz + a*g_Wind2.xyz + (1.0f-a)*g_Wind3.xyz;

			float3 v = sharedPos[indexForSharedMem].xyz - sharedPos[indexForSharedMem+1].xyz;
			float3 force = -cross(cross(v, w), v);
			sharedPos[indexForSharedMem].xyz += force*g_TimeStep*g_TimeStep; 				
		}
	}

	GroupMemoryBarrierWithGroupSync();

	// We do the length constraints in LocalShapeConstraints. 
	// Below code is vertex level parallel process and uses shared memory. So it is better than doing it in LocalShapeConstraints.
	// But we need one directional position update to prevent exessive stretchy problem under fast head movement. 
	//----------------------------
	// Enforce length constraints
	//----------------------------
	uint a = floor(numVerticesInTheStrand/2.0f);
	uint b = floor((numVerticesInTheStrand-1)/2.0f); 
	
	for ( int iterationE=0; iterationE < g_NumLengthConstraintIterations; iterationE++ )
	{
		uint sharedIndex = localVertexIndex*2 + offsetForLocalVertexIndex;

		if( localVertexIndex < a )
			ApplyDistanceConstraint(sharedPos[sharedIndex], sharedPos[sharedIndex+1], sharedLength[sharedIndex].x);

		GroupMemoryBarrierWithGroupSync();

		if( localVertexIndex < b )
			ApplyDistanceConstraint(sharedPos[sharedIndex+1], sharedPos[sharedIndex+2], sharedLength[sharedIndex+1].x);

		GroupMemoryBarrierWithGroupSync();
	}

	//---------------------------------------
    // update global position buffers
    //---------------------------------------
	if ( localVertexIndex < numVerticesInTheStrand )
	    g_HairVertexPositions[globalVertexIndex] = sharedPos[indexForSharedMem];
	
	return;
}

//--------------------------------------------------------------------------------------
// 
//	CollisionAndTangents
//
//  Compute shader to simulate collisions and calculate the new tangent after the vertex has moved.
//
// One thread computes one vertex.
//
//--------------------------------------------------------------------------------------
[numthreads(THREAD_GROUP_SIZE, 1, 1)]
void CollisionAndTangents(uint GIndex : SV_GroupIndex,
                  uint3 GId : SV_GroupID,
                  uint3 DTid : SV_DispatchThreadID)
{
	int offset = 0;

	uint globalStrandIndex = 0;
	uint localVertexIndex = 0; 
	uint indexForSharedMem = GIndex;
	uint numOfStrandsPerThreadGroup = 2;
	uint maxPossibleVertsInStrand = (THREAD_GROUP_SIZE / numOfStrandsPerThreadGroup);
	
	if ( GIndex < maxPossibleVertsInStrand )
	{
		globalStrandIndex = 2 * GId.x;
		localVertexIndex = GIndex;
	}
	else
	{
		globalStrandIndex = 2 * GId.x + 1;
		localVertexIndex = GIndex - maxPossibleVertsInStrand;
	}
	
    if( globalStrandIndex > 0 ) 
    {
        offset =  g_HairVerticesOffsetsSRV.Load(globalStrandIndex - 1);
    }

	uint globalVertexIndex = offset + localVertexIndex;
    uint numVerticesInTheStrand = g_HairVerticesOffsetsSRV.Load(globalStrandIndex) - offset;

	if ( numVerticesInTheStrand > maxPossibleVertsInStrand )
		numVerticesInTheStrand = maxPossibleVertsInStrand;
		
    //------------------------------
    // Copy data into shared memory
    //------------------------------
    if (localVertexIndex < numVerticesInTheStrand )
    {
        sharedPos[indexForSharedMem] = g_HairVertexPositions[globalVertexIndex];
    }

	float4 oldPos = g_HairVertexPositionsPrev[globalVertexIndex];
        
    GroupMemoryBarrierWithGroupSync();
	
    //-------------------------------------------------
    // Collision handling hard-code collision shapes
    //-------------------------------------------------
    if ( g_bCollision > 0 )
    {
        {
            float3 centerSphere = g_cc0_center;
            centerSphere = mul(float4( centerSphere.xyz, 1), g_ModelTransformForHead).xyz;
            float radius = g_cc0_radius;

			CollisionCapsule cc;
			cc.p1.xyz = centerSphere;
			cc.p1.w = radius;
			cc.p2.xyz = centerSphere + float3(0.0,1.0,0.0);
			cc.p2.w = g_cc0_radius2;
			sharedPos[indexForSharedMem].xyz = CapsuleCollision(sharedPos[indexForSharedMem], oldPos, cc);
        }

        {
            float3 centerSphere = g_cc1_center;
            centerSphere = mul(float4( centerSphere.xyz, 1), g_ModelTransformForHead).xyz;
            float radius = g_cc1_radius;

			CollisionCapsule cc;
			cc.p1.xyz = centerSphere;
			cc.p1.w = radius;
			cc.p2.xyz = centerSphere + float3(0.0,1.0,0.0);
			cc.p2.w = g_cc1_radius2;
			sharedPos[indexForSharedMem].xyz = CapsuleCollision(sharedPos[indexForSharedMem], oldPos, cc);
        }

        {
            float3 centerSphere = g_cc2_center;
            centerSphere = mul(float4( centerSphere.xyz, 1), g_ModelTransformForHead).xyz;
            float radius = g_cc2_radius;

			CollisionCapsule cc;
			cc.p1.xyz = centerSphere;
			cc.p1.w = radius;
			cc.p2.xyz = centerSphere + float3(0.0,1.0,0.0);
			cc.p2.w =  g_cc2_radius2;
			sharedPos[indexForSharedMem].xyz = CapsuleCollision(sharedPos[indexForSharedMem], oldPos, cc);
        }
    }

    GroupMemoryBarrierWithGroupSync();
    
    //-------------------
    // Compute tangent
    //-------------------
    if ( localVertexIndex < numVerticesInTheStrand-1  ) 
    {
        float3 tangent = sharedPos[indexForSharedMem+1].xyz - sharedPos[indexForSharedMem].xyz;
        g_HairVertexTangents[globalVertexIndex].xyz = normalize(tangent);
    }
	
    //---------------------------------------
    // update global position buffers
    //---------------------------------------
	if ( localVertexIndex < numVerticesInTheStrand )
	    g_HairVertexPositions[globalVertexIndex] = sharedPos[indexForSharedMem];

	return;
}
