using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;
using FILETIME = System.Runtime.InteropServices.ComTypes.FILETIME;

namespace FileApp1
{
    class NativeMethod
    {
        internal const int MAX_PATH = 260;
        // While Windows allows larger paths up to a maxium of 32767 characters, because this is only an approximation and
        // can vary across systems and OS versions, we choose a limit well under so that we can give a consistent behavior.
        internal const int MAX_LONG_PATH = 32000;
        internal const int MAX_ALTERNATE = 14;



        [StructLayout(LayoutKind.Sequential)]
        internal struct WIN32_FILE_ATTRIBUTE_DATA
        {
            internal int fileAttributes;
            internal uint ftCreationTimeLow;
            internal uint ftCreationTimeHigh;
            internal uint ftLastAccessTimeLow;
            internal uint ftLastAccessTimeHigh;
            internal uint ftLastWriteTimeLow;
            internal uint ftLastWriteTimeHigh;
            internal uint fileSizeHigh;
            internal uint fileSizeLow;
        }


        [DllImport("api-ms-win-core-file-l1-2-1.dll", SetLastError = true, CharSet = CharSet.Unicode, BestFitMapping = false)]
        internal static extern bool GetFileAttributesEx(string name, int fileInfoLevel, out WIN32_FILE_ATTRIBUTE_DATA data);
    }
}
