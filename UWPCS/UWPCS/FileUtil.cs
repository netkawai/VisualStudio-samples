using System;
using System.Diagnostics.Contracts;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;



namespace FileApp1
{
    static  class FileUtil
    {
        const int HRESULT_WIN32_FILE_NOT_FOUND = unchecked((int)0x80070002);
        const int HRESULT_WIN32_PATH_NOT_FOUND = unchecked((int)0x80070003);

        // 
        // Use to avoid the perf hit of a Demand when the Demand is not necessary for security.
        // 
        // If trueOnError is set, then return true if we cannot confirm that the file does NOT exist.
        //
        internal static bool FileExists(string filename, bool trueOnError)
        {
            NativeMethod.WIN32_FILE_ATTRIBUTE_DATA data;
            bool ok = NativeMethod.GetFileAttributesEx(filename, 0, out data);
            if (ok)
            {
                // The path exists. Return true if it is a file, false if a directory. 
                return (data.fileAttributes & (int)FileAttributes.Directory) != (int)FileAttributes.Directory;
            }
            else
            {
                if (!trueOnError)
                {
                    return false;
                }
                else
                {
                    // Return true if we cannot confirm that the file does NOT exist. 
                    int hr = Marshal.GetHRForLastWin32Error();
                    if (hr == HRESULT_WIN32_FILE_NOT_FOUND || hr == HRESULT_WIN32_PATH_NOT_FOUND)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
            }
        }
    }
}
