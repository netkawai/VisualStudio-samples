"C:\Program Files (x86)\Windows Kits\10\bin\10.0.19041.0\x64\makeappx" pack /d "C:\Hobby\personal\VisualStudio-samples\SimpleWin32Sparse\SimpleWin32Sparse" /p "C:\Hobby\personal\VisualStudio-samples\SimpleWin32Sparse\package.msix" /nv /o
REM "C:\Program Files (x86)\Windows Kits\10\bin\10.0.19041.0\x64\signtool" sign /fd SHA256 /a /f "$(SolutionDir)\signingkey.pfx" /p "password" "$(ProjectDir)\package.msix"