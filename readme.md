This is memorandum, not official, only my understanding for .NET Framework and .NET environment for WINDOWS desktop. Until Windows 7 era, it was relatively simple. However after Windows 8 due to failure of Microsoft Smartphone(and app distribution), it became much more complex unnecessary. but there is no other alternative for truly Desktop OS.
This is as of July 2022, here describes only supported versions However .NET Core 3.1 will end soon though, it will be updated in the future.

1. .NET Framework 3.5 with SP (CLR2.0) - C# Language version 7.3 - Legacy
https://docs.microsoft.com/en-us/dotnet/api/?view=netframework-3.5
This is oldest version supported as a part of Windows. Development tools became optional.
If you want to develop with this, you need to turn on from Windows Feature.
There are still some 3rd party components relies on this version. Since this version covered already WinForm and WPF as UI toolkit. If you don't care asynchronous, it is ok.

2. .NET Framework 4.8 downward to 4.6.2 (CLR 4.0) - C# Language version 7.3 - Stable (Visual Studio run on this version)
https://docs.microsoft.com/en-us/dotnet/api/?view=netframework-4.8
This is the most stable major version in Windows. Unfortunately, there are some different APIs between 4.6.2 to 4.7.2. Highly it depends on what functions you need.
Except LTSC or IoT version, normal channel Windows get 4.8 automatically. most cases it works out of box. async and await only supported from this. It is super useful not to block UI thread.

3. .NET Native with .NET Framework version???? - C# Language version 7.3 - Useless
https://docs.microsoft.com/en-us/dotnet/api/?view=dotnet-uwp-10.0
https://docs.microsoft.com/en-us/windows/uwp/launch-resume/how-to-create-and-consume-an-app-service
 From Windows 10, Microsoft introduced a different .NET tool, which is .NET Native.
 This is fundamentally RyuCompiler + RyuJIT without legacy JIT. 
 When Microsoft introduced UWP which is a native app (not run on CLR), since many Microsoft developer are familiar with C#, so they decided to provide C# compiler(combined with JIT) can generate a machine native code. This is Windows-specific, UWP itself introduced totally different API, WinRT or UAP api (There are some similarity or accustom pattern, but different). I am not sure exact C# language version, but Most UWP use C# 7.3 (either .NETCore2 or .NET Framework based because of  Windows specific nature) Currently, Microsoft is abandoning UWP as platform, shift to MSIX-packaged desktop app. There is a possibility to deprecate or obsolete UWP app model. (Neither UI nor Resource API, but App model/rules for runtime broker)

4. .NET 6 above or latest - C# Language 10.0 above - New, Huge runtime libraries, Not system-wide
 https://docs.microsoft.com/en-us/dotnet/api/?view=windowsdesktop-6.0
 https://docs.microsoft.com/en-us/windows/windows-app-sdk/api/winrt/microsoft.ui?view=windows-app-sdk-1.1
 https://docs.microsoft.com/en-us/windows/apps/develop/platform/csharp-winrt/
 Simultaneously, the rise of on-demand backend platform(coined as cloud), Linux kernel became dominant due to mobile popularity. The .NET Framework was intentionally designed in Windows (GAC e.g). It cannot deploy it to other OS. So they re-invested for pure OS independent .NET from Mono (originally developed separately). This CLR can run on any OS and optimized 64bit architecture(x64 and ARM64), no depend on a specific Windows feature API. For supporting cross platform. They completely re-defined a new csproj format for Visual Studio. So even they split UI parts for Windows. They call Windows.Desktop extensions(or packages?). 
 Microsoft decided to stop maintaining two separate .NET environment. So they simply abandoned .NET Framework development, shift all effort toward the new cross-platform minded, they named .NET (no word for platform). So all platform depended implementations are removed from Fundamental class. all those are provided separately, with the same version scheme or separate scheme. WinRT also is separated. So unlike .NET Core(old version), now we need nuget either source codes (generation or written) to consume Windows API. Even then, they created middle libraries to consume WinRT from normal Win32 desktop (They call Windows App SDK, not confusing Windows SDK). As of now, since the Windows App SDK was developed with .NET series, there is no way to use with .NET Framework 4.8(failure of UWP). Windows App SDK API is still not so stable.
 They added some APIs in Windows 11, and implemented a mitigation for Windows 10 to use UWP functions from Win32 app.


Optional
a. (Soon will deprecate) .NET Core 3.1 with Desktop extension (.NET 3.1 has knowledge for WinRT) - C# Language 8.0
 https://docs.microsoft.com/en-us/dotnet/api/?view=windowsdesktop-3.1
 https://docs.microsoft.com/en-us/dotnet/api/windows.ui?view=dotnet-plat-ext-3.1
This is .NET Core 3.1 is the first version can be covered a major Windows Desktop App. Even this .NET Core series had embedded knowledge for WinRT. Due to sharing some code bases with UWP(.NET Native)

No.3 is due to simply a failure of Microsoft. However, like Win32 API scheme (A and W), it will not go away as runtime.
Those three (No.2, No3, No4.) are distinguishably different. But WPF and Winform are migrated with .NET 6. So if you don't have 3rd party relies on .NET Framework 3.5 or 4.8. It can be migrated, but you need to copy all runtime dlls. Since No.4 runtime is NOT a part of Windows OS.
