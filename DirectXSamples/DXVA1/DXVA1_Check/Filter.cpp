
#include <streams.h>
#include <videoacc.h>
#include <initguid.h>

#include "filter.h"

// {F2ABC306-ECF1-4de3-AB5C-3E2A25325A60}
DEFINE_GUID(CLSID_PushFilter, 0xf2abc306, 0xecf1, 0x4de3, 0xab, 0x5c, 0x3e, 0x2a, 0x25, 0x32, 0x5a, 0x60);

/**********************************************
 *  CPushPin Class
 **********************************************/

CPushPin::CPushPin(CPushFilter *pFilter, HRESULT *phr) :
	m_pFilter(pFilter),
	CSourceStream(NAME("PushPin"), phr, pFilter, L"Output")
{
}

CPushPin::~CPushPin()
{   
}

HRESULT CPushPin::GetMediaType(CMediaType *pmt)
{
    CAutoLock cAutoLock(m_pFilter->pStateLock());
    CheckPointer(pmt, E_POINTER);

	VIDEOINFO *pvi = (VIDEOINFO *) pmt->AllocFormatBuffer(sizeof(VIDEOINFO));
	if (pvi == NULL) return E_OUTOFMEMORY;
	ZeroMemory(pvi, sizeof(VIDEOINFO));

	pvi->bmiHeader.biCompression	= BI_RGB;
	pvi->bmiHeader.biBitCount		= 32;
	pvi->bmiHeader.biSize			= sizeof(BITMAPINFOHEADER);
	pvi->bmiHeader.biWidth			= 720;
	pvi->bmiHeader.biHeight			= 480;
	pvi->bmiHeader.biPlanes			= 1;
	pvi->bmiHeader.biSizeImage		= GetBitmapSize(&pvi->bmiHeader);
	pvi->bmiHeader.biClrImportant	= 0;

	SetRectEmpty(&(pvi->rcSource));
	SetRectEmpty(&(pvi->rcTarget));

	GUID subType = GetBitmapSubtype(&pvi->bmiHeader);

	pmt->SetType(&MEDIATYPE_Video);
	pmt->SetSubtype(&subType);
	pmt->SetSampleSize(pvi->bmiHeader.biSizeImage);
	pmt->SetTemporalCompression(FALSE);
	pmt->SetFormatType(&FORMAT_VideoInfo);

    return S_OK;
}

HRESULT CPushPin::DecideBufferSize(IMemAllocator *pAlloc, ALLOCATOR_PROPERTIES *pRequest)
{
    CAutoLock cAutoLock(m_pFilter->pStateLock());
    CheckPointer(pAlloc, E_POINTER);
    CheckPointer(pRequest, E_POINTER);

    return S_OK;
}

HRESULT CPushPin::FillBuffer(IMediaSample *pSample)
{
    CAutoLock cAutoLockShared(&m_cSection);
    CheckPointer(pSample, E_POINTER);

    return S_OK;
}

/**********************************************
 *  CPushFilter Class
 **********************************************/

CPushFilter::CPushFilter(IUnknown *pUnk, HRESULT *phr) :
	m_pPin(NULL),
	CSource(NAME("PushFilter"), pUnk, CLSID_PushFilter)
{
    // The pin magically adds itself to our pin array.
    m_pPin = new CPushPin(this, phr);

    if (phr != NULL) {
        if (m_pPin == NULL)
            *phr = E_OUTOFMEMORY;
        else
            *phr = S_OK;
    }  
}

CPushFilter::~CPushFilter()
{
	ASSERT(m_pPin->m_cRef == 0);
	delete m_pPin;
	m_pPin = NULL;
}

CUnknown * WINAPI CPushFilter::CreateInstance(IUnknown *pUnk, HRESULT *phr)
{
    CPushFilter *pNewFilter = new CPushFilter(pUnk, phr);

    if (phr != NULL) {
        if (pNewFilter == NULL) 
            *phr = E_OUTOFMEMORY;
        else
			pNewFilter->AddRef();
            *phr = S_OK;
    }

    return pNewFilter;
}

CPushPin *CPushFilter::GetPin(int n) {

    CAutoLock lock(&m_cStateLock);

	if (n == 0) {
		m_pPin->NonDelegatingAddRef();
		return m_pPin;
	} else {
		return NULL;
	}
}
