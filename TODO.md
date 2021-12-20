Recreate WinUIApp1

To follow the unpackaged app, first re-create sln WinUI3 packaged (Desktop)
then add <WindowsPackageType>None</WindowsPackageType>

https://docs.microsoft.com/en-us/windows/apps/winui/winui3/create-your-first-winui3-app?pivots=winui3-unpackaged-csharp

then follow the same way, add Microsoft.Graphics.Win2D and OpenTK, then
Use Grid to split 4 panels, One is 3D view (codeproject)
https://www.codeproject.com/Articles/839389/Fast-Point-Cloud-Viewer-with-Csharp-and-OpenGL

then other three are sliced view like blender will use canvas Win2D to draw sliced images.
This is a way as far as I know, to utilize the hardware acceleration for 3D and 2D view both.

