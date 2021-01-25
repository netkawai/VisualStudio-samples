#pragma once

#define _WIN32_WINNT 0x0600

#include <streams.h>
#include <stdio.h>
#include <tchar.h>
#include <dshow.h>
#include <videoacc.h>
#include <d3d9.h>
#include <vmr9.h>
#include <initguid.h>
#include <dxva.h>

#include "filter.h"

#pragma comment(lib, "strmiids.lib")

#define SAFE_RELEASE(x) { if (x) { x->Release(); x = NULL; } }
#define SAFE_DELETE(x)	{ if (x) { delete x; x = NULL; } }
#define SAFE_ARRAYDELETE(x) { if (x) { delete[] x; x = NULL; } }

void OutputGUIDName(const GUID * pGUID)
{
	TCHAR *msg = NULL;

	if (*pGUID == DXVA_ProcAmpControlDevice) {
		msg = L"DXVA_ProcAmpControlDevice";
	} else if (*pGUID == DXVA_ProcAmpControlDevice) {
		msg = L"DXVA_ProcAmpControlDevice";
	} else if (*pGUID == DXVA_DeinterlaceBobDevice) {
		msg = L"DXVA_DeinterlaceBobDevice";
	} else if (*pGUID == DXVA_DeinterlaceContainerDevice) {
		msg = L"DXVA_DeinterlaceContainerDevice";
	} else if (*pGUID == DXVA_ModeNone) {
		msg = L"DXVA_ModeNone";
	} else if (*pGUID == DXVA_ModeH261_A) {
		msg = L"DXVA_ModeH261_A";
	} else if (*pGUID == DXVA_ModeH261_B) {
		msg = L"DXVA_ModeH261_B";
	} else if (*pGUID == DXVA_ModeH263_A) {
		msg = L"DXVA_ModeH263_A";
	} else if (*pGUID == DXVA_ModeH263_B) {
		msg = L"DXVA_ModeH263_B";
	} else if (*pGUID == DXVA_ModeH263_C) {
		msg = L"DXVA_ModeH263_C";
	} else if (*pGUID == DXVA_ModeH263_D) {
		msg = L"DXVA_ModeH263_D";
	} else if (*pGUID == DXVA_ModeH263_E) {
		msg = L"DXVA_ModeH263_E";
	} else if (*pGUID == DXVA_ModeH263_F) {
		msg = L"DXVA_ModeH263_F";
	} else if (*pGUID == DXVA_ModeMPEG1_A) {
		msg = L"DXVA_ModeMPEG1_A";
	} else if (*pGUID == DXVA_ModeMPEG2_A) {
		msg = L"DXVA_ModeMPEG2_A";
	} else if (*pGUID == DXVA_ModeMPEG2_B) {
		msg = L"DXVA_ModeMPEG2_B";
	} else if (*pGUID == DXVA_ModeMPEG2_C) {
		msg = L"DXVA_ModeMPEG2_C";
	} else if (*pGUID == DXVA_ModeMPEG2_D) {
		msg = L"DXVA_ModeMPEG2_D";
	} else if (*pGUID == DXVA_ModeH264_A) {
		msg = L"DXVA_ModeH264_A (DXVA_ModeH264_MoComp_NoFGT)";
	} else if (*pGUID == DXVA_ModeH264_B) {
		msg = L"DXVA_ModeH264_B (DXVA_ModeH264_MoComp_FGT)";
	} else if (*pGUID == DXVA_ModeH264_C) {
		msg = L"DXVA_ModeH264_C (DXVA_ModeH264_IDCT_NoFGT)";
	} else if (*pGUID == DXVA_ModeH264_D) {
		msg = L"DXVA_ModeH264_D (DXVA_ModeH264_IDCT_FGT)";
	} else if (*pGUID == DXVA_ModeH264_E) {
		msg = L"DXVA_ModeH264_E (DXVA_ModeH264_VLD_NoFGT)";
	} else if (*pGUID == DXVA_ModeH264_F) {
		msg = L"DXVA_ModeH264_F (DXVA_ModeH264_VLD_FGT)";
	} else if (*pGUID == DXVA_ModeWMV8_A) {
		msg = L"DXVA_ModeWMV8_A (DXVA_ModeWMV8_PostProc)";
	} else if (*pGUID == DXVA_ModeWMV8_B) {
		msg = L"DXVA_ModeWMV8_B (DXVA_ModeWMV8_MoComp)";
	} else if (*pGUID == DXVA_ModeWMV9_A) {
		msg = L"DXVA_ModeWMV9_A (DXVA_ModeWMV9_PostProc)";
	} else if (*pGUID == DXVA_ModeWMV9_B) {
		msg = L"DXVA_ModeWMV9_B (DXVA_ModeWMV9_MoComp)";
	} else if (*pGUID == DXVA_ModeWMV9_C) {
		msg = L"DXVA_ModeWMV9_C (DXVA_ModeWMV9_IDCT)";
	} else if (*pGUID == DXVA_ModeVC1_A) {
		msg = L"DXVA_ModeVC1_A (DXVA_ModeVC1_PostProc)";
	} else if (*pGUID == DXVA_ModeVC1_B) {
		msg = L"DXVA_ModeVC1_B (DXVA_ModeVC1_MoComp)";
	} else if (*pGUID == DXVA_ModeVC1_C) {
		msg = L"DXVA_ModeVC1_C (DXVA_ModeVC1_IDCT)";
	} else if (*pGUID == DXVA_ModeVC1_D) {
		msg = L"DXVA_ModeVC1_D (DXVA_ModeVC1_VLD)";
	} else if (*pGUID == DXVA_NoEncrypt) {
		msg = L"DXVA_NoEncrypt";
	} else {
		msg = L"Unknown GUID";
	}

	wprintf(L" %s", msg);

}

void OutputGUID(const GUID *pGUID)
{
	UCHAR *pWork = (UCHAR *) pGUID;

	wprintf(L"{%08X-", *(DWORD*)pWork);	pWork += sizeof(DWORD);
	wprintf(L"%04X-", *(WORD*)pWork);	pWork += sizeof(WORD);
	wprintf(L"%04X-", *(WORD*)pWork);	pWork += sizeof(WORD);
	for(int i = 0; i < 8; i++ ) {
		wprintf(L"%02X", *pWork);	pWork++;
		if (i == 1) wprintf(L"-");
	}
	wprintf(L"}");

	OutputGUIDName(pGUID);

}

void OutputFormat(const DDPIXELFORMAT *pFormat)
{
	if (pFormat->dwSize != sizeof(DDPIXELFORMAT)) {
		wprintf(L"dwSize is invalid.");
		return;
	}

	wprintf(L"dwFlags = 0x%08X ", pFormat->dwFlags);
	if (pFormat->dwFlags & DDPF_FOURCC) wprintf(L"dwFourCC = 0x%08X ", pFormat->dwFourCC);

}

int _tmain(int argc, _TCHAR* argv[])
{
	HRESULT				hr = S_OK;
	IBaseFilter			*pVmr9 = NULL;
	CPushFilter			*pFilter = NULL;
	IEnumPins			*pPinEnum = NULL;
	IPin				*pInputPin = NULL;
	IPin				*pOutputPin = NULL;
	IAMVideoAccelerator	*pAccelerator = NULL;
	GUID				*pGuidArray	 = NULL;
	DDPIXELFORMAT		*pFormatArray = NULL;

		hr = CoInitializeEx(NULL, COINIT_APARTMENTTHREADED);
	if (FAILED(hr))	{
		wprintf(L"Failed to CoInitializeEx. hr = 0x%08lx\n", hr);
		return -1;
	}

	hr = CoCreateInstance(CLSID_VideoMixingRenderer9, NULL, CLSCTX_INPROC, IID_IBaseFilter, (void **) &pVmr9);
	if (FAILED(hr))	{
		wprintf(L"Failed to create VMR9 instance. hr = 0x%08lx\n", hr);
		goto done;
	}

	pFilter = (CPushFilter *)CPushFilter::CreateInstance(NULL, &hr);
	if (pFilter == NULL || FAILED(hr))	{
		wprintf(L"Failed to create DummyFilter instance. hr = 0x%08lx\n", hr);
		goto done;
	}

	pOutputPin = pFilter->GetPin(0);
	if (pOutputPin == NULL)	{
		wprintf(L"Failed to get output pin. hr = 0x%08lx\n", hr);
		goto done;
	}

	hr = pVmr9->EnumPins(&pPinEnum);
	if (FAILED(hr))	{
		wprintf(L"Failed to enumrate pin. hr = 0x%08lx\n", hr);
		goto done;
	}

	hr = pPinEnum->Next(1, &pInputPin, NULL);
	if (FAILED(hr))	{
		wprintf(L"Failed to get input pin pointer. hr = 0x%08lx\n", hr);
		goto done;
	}

	hr = pInputPin->QueryInterface(IID_IAMVideoAccelerator, (void **)&pAccelerator);
	if (FAILED(hr))	{
		wprintf(L"Failed to get IAMVideoAccelerator interface. hr = 0x%08lx\n", hr);
		goto done;
	}

	hr = pOutputPin->Connect(pInputPin, NULL);
	if (FAILED(hr))	{
		wprintf(L"Failed to connect pins. hr = 0x%08lx\n", hr);
		goto done;
	}

	// get number of supported accelerator GUID
	DWORD	numGuid = 0;
	hr = pAccelerator->GetVideoAcceleratorGUIDs(&numGuid, NULL);
	if (hr != S_OK)	{
		wprintf(L"Failed to get number of supported accelerator GUID. hr = 0x%08lx\n", hr);
		goto done;
	}

	wprintf(L"\nNumber of supported Accelerator GUID is %d.\n", numGuid);

	if (numGuid > 0) {
		pGuidArray = new GUID[numGuid];
		if (pGuidArray == NULL)	{
			wprintf(L"Failed to allocate GUID array.\n");
			goto done;
		}

		// get array of supported accelerator GUID
		hr = pAccelerator->GetVideoAcceleratorGUIDs(&numGuid, (GUID *)pGuidArray);
		if (hr != S_OK)	{
			wprintf(L"Failed to get supported accelerator GUID array. hr = 0x%08lx\n", hr);
			goto done;
		}
		
		LPGUID pGuid = pGuidArray;
		for(DWORD i = 0; i < numGuid; i++) {

			wprintf(L"#%02d ", i); OutputGUID(pGuid); wprintf(L"\n");

			DWORD	numFormat = 0;
			hr = pAccelerator->GetUncompFormatsSupported(pGuid, &numFormat, NULL);
			if (hr != S_OK)	{
				wprintf(L"Failed to get number of supported PixelFormat. hr = 0x%08lx\n", hr);
				goto done;
			}
			wprintf(L"\tNumber of supported Format is %d.\n", numFormat);

			if (numFormat > 0) {
				pFormatArray = new DDPIXELFORMAT[numFormat];
				if (pFormatArray == NULL)	{
					wprintf(L"Failed to allocate PixelFoamrt array.\n");
					goto done;
				} 
				hr = pAccelerator->GetUncompFormatsSupported(pGuid, &numFormat, pFormatArray);
				if (hr != S_OK)	{
					wprintf(L"Failed to get supported PixelFormat array. hr = 0x%08lx\n", hr);
					goto done;
				} 
				LPDDPIXELFORMAT pFormat = pFormatArray;
				for (DWORD j = 0; j < numFormat; j++) {
					wprintf(L"\t#%02d ", j); OutputFormat(pFormat); wprintf(L"\n");
					pFormat++;
				}
				SAFE_ARRAYDELETE(pFormatArray);
			}
			pGuid++;
		}
	}	// numGuid >0


done:
	SAFE_ARRAYDELETE(pGuidArray);
	SAFE_ARRAYDELETE(pFormatArray);

	// need to excute Disconnect() on both pin
	hr = pInputPin->Disconnect();
	hr = pOutputPin->Disconnect();

	SAFE_RELEASE(pAccelerator);
	SAFE_RELEASE(pOutputPin);
	SAFE_RELEASE(pPinEnum);
	SAFE_RELEASE(pInputPin);
	SAFE_RELEASE(pFilter);
	SAFE_RELEASE(pVmr9);

	CoUninitialize();   

	return 0;
}