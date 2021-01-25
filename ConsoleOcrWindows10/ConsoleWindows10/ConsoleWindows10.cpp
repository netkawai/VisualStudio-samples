// ConsoleWindows10.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"
using namespace std;

using namespace ABI::Windows::Globalization;
using namespace ABI::Windows::Media::Ocr;
#pragma comment(lib, "runtimeobject.lib")

int enableVirtualTerminal()
{
	HANDLE hOut = GetStdHandle(STD_OUTPUT_HANDLE);
	if (hOut == INVALID_HANDLE_VALUE) {
		fprintf(stderr, "INVALID_HANDLE_VALUE: %d", GetLastError());
		return GetLastError();
	}

	DWORD consoleMode = 0;
	if (!GetConsoleMode(hOut, &consoleMode)) {
		fprintf(stderr, "GetConsoleMode failed: %d", GetLastError());
		return GetLastError();
	}

	consoleMode |= ENABLE_VIRTUAL_TERMINAL_PROCESSING;
	if (!SetConsoleMode(hOut, consoleMode)) {
		fprintf(stderr, "SetConsoleMode failed: %d", GetLastError());
		return GetLastError();
	}
	printf("Output after ENABLE_VIRTUAL_TERMINAL_PROCESSING is enabled\n");
}


int main()
{

	static_assert(sizeof(std::wstring::value_type) == sizeof(std::u16string::value_type),
		"std::wstring and std::u16string are expected to have the same character size");

	enableVirtualTerminal();

	//SetConsole
	printf("\033[35m color-test \033[39m  test end\n");

	::RoInitialize(RO_INIT_MULTITHREADED);

	HANDLE hOut = ::GetStdHandle(STD_OUTPUT_HANDLE);

//    Microsoft::WRL::ComPtr<Windows::Globalization::Calender>
//	Calendar^ calendar = ref new Calendar();
//	calendar->SetToNow();
//	wcout << "It's now " << calendar->HourAsPaddedString(2)->Data() << L":" <<
//		calendar->MinuteAsPaddedString(2)->Data() << L":" <<
//		calendar->SecondAsPaddedString(2)->Data() << endl;
	COORD coord;
	DWORD cWritten;
	char16_t fillChar = u'©';
	coord.X = 0;            // start at 0th cell
	coord.Y = 6;            //   of sixth row
		::FillConsoleOutputCharacter(hOut,

			(TCHAR)fillChar,
			30 * 20,
			coord,
			&cWritten);

	HSTRING hClassName;
	HRESULT hr;

	u16string className(u"Windows.Globalization.Calendar");

	
	hr = ::WindowsCreateString((PCNZWCH)(className.c_str()), className.size(), &hClassName);
	IInspectable* pInst;
	hr = ::RoActivateInstance(hClassName, &pInst);
	::WindowsDeleteString(hClassName);

	ICalendar* pCalendar;
	hr = pInst->QueryInterface(__uuidof(ICalendar), (void**)&pCalendar);


	pCalendar->SetToNow();
	INT32 hour, minute, second;
	pCalendar->get_Hour(&hour);
	pCalendar->get_Minute(&minute);
	pCalendar->get_Second(&second);


	pCalendar->Release();
	pInst->Release();

	wcout << u"Time: " << hour << u":" << minute << u":" << second << endl;

	IOcrEngineStatics * pOcrEngineStatic;
	IOcrEngine *pOcrEngine;
	ILanguage *lang;
	
	wstring iocrEngineStatic(L"Windows.Media.Ocr.OcrEngine");
	hr = ::WindowsCreateString(iocrEngineStatic.c_str(), iocrEngineStatic.size(), &hClassName);
	hr = ::RoGetActivationFactory(hClassName, __uuidof(IOcrEngineStatics) ,(void **) &pInst);
	::WindowsDeleteString(hClassName);

	hr = pInst->QueryInterface(__uuidof(IOcrEngineStatics), (void**)&pOcrEngineStatic);


	hr = pOcrEngineStatic->TryCreateFromUserProfileLanguages(&pOcrEngine);

	pOcrEngine->get_RecognizerLanguage(&lang);
	HSTRING hDisp;
	lang->get_DisplayName(&hDisp);
	wstring dispName(::WindowsGetStringRawBuffer(hDisp,NULL));

	wcout << u"lang:" << dispName << endl;

	pOcrEngineStatic->Release();
	pInst->Release();

//	ABI::Windows::Foundation::IMemoryBufferFactory *pMemoryFactory;
//	pMemoryFactory->Create(10,&pMemoryFactory);

	::RoUninitialize();

	getchar();
	return 0;
}

