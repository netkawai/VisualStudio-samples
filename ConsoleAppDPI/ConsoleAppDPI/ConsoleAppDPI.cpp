// ConsoleAppDPI.cpp : This file contains the 'main' function. Program execution begins and ends there.
//
#define _CRT_SECURE_NO_WARNINGS
#include <windows.h>
#include <stdio.h>


typedef enum _MONITOR_DPI_TYPE {
	MDT_EFFECTIVE_DPI = 0,
	MDT_ANGULAR_DPI = 1,
	MDT_RAW_DPI = 2,
	MDT_DEFAULT = MDT_EFFECTIVE_DPI
} MONITOR_DPI_TYPE;

typedef HRESULT(WINAPI* GetDpiForMonitorFunc)(HMONITOR, MONITOR_DPI_TYPE, UINT*, UINT*);
static GetDpiForMonitorFunc pGetDpiForMonitor;

typedef enum _PROCESS_DPI_AWARENESS {
	PROCESS_DPI_UNAWARE = 0,
	PROCESS_SYSTEM_DPI_AWARE = 1,
	PROCESS_PER_MONITOR_DPI_AWARE = 2
} PROCESS_DPI_AWARENESS;

typedef HRESULT(WINAPI* SPDAPROC)(
	PROCESS_DPI_AWARENESS value
	);

SPDAPROC pSetProcessDpiAwareness;

BOOL CALLBACK MonitorEnumProc(
	HMONITOR monitor,  
	HDC hdc,     
	LPRECT rc, 
	LPARAM data  
)
{
	printf("(%d,%d)-(%d,%d)\n", rc->left, rc->top, rc->right, rc->bottom);
	return TRUE;
}
int main(int argc, char** argv)
{
	pSetProcessDpiAwareness = (SPDAPROC)GetProcAddress(LoadLibrary(L"shcore"), "SetProcessDpiAwareness");
	if (!pSetProcessDpiAwareness) {
		fprintf(stderr, "SetProcessDpiAwareness is unsupported.\n");
		exit(0);
	}
	pGetDpiForMonitor = (GetDpiForMonitorFunc)GetProcAddress(LoadLibrary(L"shcore"), "GetDpiForMonitor");
	if (!pGetDpiForMonitor) {
		fprintf(stderr, "GetDpiForMonitor is unsupported.\n");
		exit(0);
	}
	if (argc < 2) {
		char cmdline[512];
		sprintf(cmdline, "%s %d\n", argv[0], PROCESS_DPI_UNAWARE);
		system(cmdline);
		sprintf(cmdline, "%s %d\n", argv[0], PROCESS_SYSTEM_DPI_AWARE);
		system(cmdline);
		sprintf(cmdline, "%s %d\n", argv[0], PROCESS_PER_MONITOR_DPI_AWARE);
		system(cmdline);
		exit(0);
	}
	unsigned long awareness;
	while (*++argv) {
		awareness = strtoul(*argv, NULL, 10);
	}
	switch (awareness) {
	case PROCESS_DPI_UNAWARE:
		printf("DPI unaware\n");
		break;
	case PROCESS_SYSTEM_DPI_AWARE:
		printf("System DPI aware\n");
		break;
	case PROCESS_PER_MONITOR_DPI_AWARE:
		printf("Per monitor DPI aware\n");
		break;
	}
	pSetProcessDpiAwareness((PROCESS_DPI_AWARENESS)awareness);

	printf("Virtual screen: %d,%d / %dx%d\n",
		GetSystemMetrics(SM_XVIRTUALSCREEN),
		GetSystemMetrics(SM_YVIRTUALSCREEN),
		GetSystemMetrics(SM_CXVIRTUALSCREEN),
		GetSystemMetrics(SM_CYVIRTUALSCREEN));
	EnumDisplayMonitors(NULL, NULL, MonitorEnumProc, 0);
}

