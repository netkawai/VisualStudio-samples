
class CPushPin : public CSourceStream
{
	friend class CPushFilter;

	CPushFilter	* m_pFilter;
	CCritSec m_cSection;

public:

	CPushPin(CPushFilter *pFilter, HRESULT *phr);
	~CPushPin();

	HRESULT GetMediaType(CMediaType *pMediaType);
	HRESULT DecideBufferSize(IMemAllocator *pAlloc, ALLOCATOR_PROPERTIES *pRequest);
	HRESULT FillBuffer(IMediaSample *pSample);
    
    STDMETHODIMP Notify(IBaseFilter *pSelf, Quality q)
    {
        return E_FAIL;
    }

};

class CPushFilter : public CSource
{
	friend class CPushPin;

    CPushPin * m_pPin;

public:
	CPushFilter(IUnknown *pUnk, HRESULT *phr);
    ~CPushFilter();

    static CUnknown * WINAPI CreateInstance(IUnknown *pUnk, HRESULT *phr);  

	CPushPin * GetPin(int n);
};
