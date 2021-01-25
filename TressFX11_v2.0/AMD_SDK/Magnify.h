//--------------------------------------------------------------------------------------
// File: Magnify.h
//
// Magnify class definition. This class magnifies a region of a given surface, and renders a scaled 
// sprite at the given position on the screen.
//
// Author: Jon Story
// 
// Copyright � AMD Corporation. All rights reserved.
//--------------------------------------------------------------------------------------


#ifndef _MAGNIFY_H_
#define _MAGNIFY_H_

namespace AMD
{

class Magnify
{
public:

    // Constructor / destructor
    Magnify();
    ~Magnify();

    // Hooks for the DX SDK Framework
    HRESULT OnCreateDevice( ID3D11Device* pd3dDevice );
    void OnDestroyDevice();
    void OnResizedSwapChain( const DXGI_SURFACE_DESC* pBackBufferSurfaceDesc );

    // Set methods
    void SetPixelRegion( int nPixelRegion );
    void SetScale( int nScale );
    void SetDepthRangeMin( float fDepthRangeMin );
    void SetDepthRangeMax( float fDepthRangeMax );
    void SetSourceResource( ID3D11Resource* pSourceResource, DXGI_FORMAT Format, 
        int nWidth, int nHeight, int nSamples );
    void SetSubSampleIndex( int nSubSampleIndex );

    // Captures a region, at the current cursor position, for magnification
    void Capture( POINT& Point );

    // Render the magnified region, at the capture location
    void RenderBackground();
    void RenderMagnifiedRegion();

private:

    // Private methods
    void SetPosition( int nPositionX, int nPositionY );
    void CreateInternalResources();

private:

    // Magnification settings
    int     m_nPositionX;
    int     m_nPositionY;
    int     m_nPixelRegion;
    int     m_nHalfPixelRegion;
    int     m_nScale;
    float   m_fDepthRangeMin;
    float   m_fDepthRangeMax;
    int     m_nBackBufferWidth;
    int     m_nBackBufferHeight;
    int     m_nSubSampleIndex;

    // Helper class for plotting the magnified region
    Sprite  m_Sprite;

    // Source resource data
    ID3D11Resource*             m_pSourceResource;
    ID3D11Texture2D*            m_pResolvedSourceResource;
    ID3D11Texture2D*            m_pCopySourceResource;
    ID3D11ShaderResourceView*   m_pResolvedSourceResourceSRV;
    ID3D11ShaderResourceView*   m_pCopySourceResourceSRV;
    ID3D11ShaderResourceView*   m_pSourceResourceSRV1;
    DXGI_FORMAT                 m_SourceResourceFormat;
    int                         m_nSourceResourceWidth;
    int                         m_nSourceResourceHeight;
    int                         m_nSourceResourceSamples;
    DXGI_FORMAT                 m_DepthFormat; 
    DXGI_FORMAT                 m_DepthSRVFormat;
    bool                        m_bDepthFormat;
};

}; // namespace AMD

#endif // _MAGNIFY_H_


//--------------------------------------------------------------------------------------
// EOF
//--------------------------------------------------------------------------------------
