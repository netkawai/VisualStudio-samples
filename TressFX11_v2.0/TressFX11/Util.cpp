//--------------------------------------------------------------------------------------
// File: Util.cpp
// 
// Copyright � AMD Corporation. All rights reserved.
//--------------------------------------------------------------------------------------

#pragma once
#include "..\\DXUT\\Core\\DXUT.h"
#include "util.h"

#include <iostream>
#include <fstream>

typedef double StatsCounterType;
 
#define RAY_EPSILON 1e-3f
#define INFINITY FLT_MAX
#define Assert(condition, message) (condition? (void)0 : MessageBoxW(0, L"Error", message, 0))
 
#if defined(WIN32)
#define memalign(a,b) _aligned_malloc(b, a)
#elif defined(__APPLE__)
#define memalign(a,b) valloc(b)
#elif defined(__OpenBSD__)
#define memalign(a,b) malloc(b)
#endif


void *AllocAligned(size_t size) 
{
#ifndef L1_CACHE_LINE_SIZE
#define L1_CACHE_LINE_SIZE 64
#endif
    return memalign(L1_CACHE_LINE_SIZE, size);
}

 
void FreeAligned(void *ptr) 
{
#ifdef WIN32 // NOBOOK
    _aligned_free(ptr);
#else // NOBOOK
    free(ptr);
#endif // NOBOOK
}

float Log2(float x) 
{
    static float invLog2 = 1.f / logf(2.f);
    return logf(x) * invLog2;
}

BBox Union(const BBox &b, const Float3 &p)
{
    BBox ret = b;
    ret.pMin.x = min(b.pMin.x, p.x);
    ret.pMin.y = min(b.pMin.y, p.y);
    ret.pMin.z = min(b.pMin.z, p.z);
    ret.pMax.x = max(b.pMax.x, p.x);
    ret.pMax.y = max(b.pMax.y, p.y);
    ret.pMax.z = max(b.pMax.z, p.z);
    return ret;
}

BBox Union(const BBox &b, const BBox &b2)
{
    BBox ret;
    ret.pMin.x = min(b.pMin.x, b2.pMin.x);
    ret.pMin.y = min(b.pMin.y, b2.pMin.y);
    ret.pMin.z = min(b.pMin.z, b2.pMin.z);
    ret.pMax.x = max(b.pMax.x, b2.pMax.x);
    ret.pMax.y = max(b.pMax.y, b2.pMax.y);
    ret.pMax.z = max(b.pMax.z, b2.pMax.z);
    return ret;
}

inline Float3 Normalize(const Float3 &v)
{
    return v / v.Length();
}

inline float Distance(const Float3 &p1, const Float3 &p2)
{
    return (p1-p2).Length();
}

inline Float3 Cross(const Float3 &v1, const Float3 &v2)
{
    return Float3((v1.y * v2.z) - (v1.z * v2.y),
                  (v1.z * v2.x) - (v1.x * v2.z),
                  (v1.x * v2.y) - (v1.y * v2.x));
}

inline float Dot(const Float3 &v1, const Float3 &v2)
{
    return v1.x * v2.x + v1.y * v2.y + v1.z * v2.z;
}

Float3 TransformPoint(const Matrix4x4 &m, const Float3 &pt)
{
    float x = pt.x, y = pt.y, z = pt.z;
    float xp = m.m[0][0]*x + m.m[0][1]*y + m.m[0][2]*z + m.m[0][3];
    float yp = m.m[1][0]*x + m.m[1][1]*y + m.m[1][2]*z + m.m[1][3];
    float zp = m.m[2][0]*x + m.m[2][1]*y + m.m[2][2]*z + m.m[2][3];
    float wp = m.m[3][0]*x + m.m[3][1]*y + m.m[3][2]*z + m.m[3][3];

    Assert(wp != 0, L"Transformed w value equals zero!");

    return Float3(xp, yp, zp)/wp;
}

void BBox::BoundingSphere(Float3 *c, float *rad) const 
{
    *c = pMin * .5f + pMax * .5f;
    *rad = Inside(*c) ? Distance(*c, pMax) : 0.f;
}

Material::Material()
{
    strName[0] = 0;
    strTexture[0] = 0;

    this->vAmbient = D3DXVECTOR3( 172/255.0f, 120/255.0f, 83/255.0f )*0.1f;
    this->vDiffuse = D3DXVECTOR3( 172/255.0f, 120/255.0f, 83/255.0f )*0.3f;
    this->vSpecular = D3DXVECTOR3( 0.02f, 0.02f, 0.02f );

    nShininess = 1;
    fAlpha = 1.0;

    bSpecular = false;

    this->pTexture9 = 0;
    this->pTextureRV11 = 0;
}
Material::~Material()
{}
