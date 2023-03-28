========================================================
Advanced Reusable Styles and Themes in WPF - Source Code
========================================================

To run the Globomantics.UI Dev app: 

* Open Globomantics.UI.sln in Visual Studio 
* Set the Dev project to be the start-up project
* Build and run

The TeRex app needs a Globomantics.UI.WPF NuGet package located in a local NuGet repo called Dev at C:\NuGet

* Make sure nuget.exe is available in your system path
* Make sure you have a C:\NuGet folder created on your system
* Open Globomantics.UI.sln in Visual Studio and set it to Release mode and build
    * This will create the Globomantics.UI.WPF NuGet package
* Open TeRex.sln in Visual Studio
* In Visual Studio check Tools > Options > NuGet Package Manager > Package Sources and
  make sure there is a source called Dev pointing to C:\NuGet
* Build and run