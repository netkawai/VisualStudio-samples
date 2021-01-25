using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MetafileFormApp
{
    class ImageItem
    {
        public Image image { get; set; }

        private Graphics.EnumerateMetafileProc metafileDelegate;
        private Point destPoint;

        public void Open(string fileName)
        {
            image = new Metafile(fileName);


        }

        private MemoryStream memoryStream;

        public void writeToStream(MemoryStream strema)
        {
            if (image is Metafile) {
                Graphics offScreenBufferGraphics;
                destPoint = new Point(0, 0);
                metafileDelegate = new Graphics.EnumerateMetafileProc(MetafileCallback);
                using (MemoryStream stream = new MemoryStream())
                {
                    using (offScreenBufferGraphics = Graphics.FromHwndInternal(IntPtr.Zero))
                    {
                        offScreenBufferGraphics.EnumerateMetafile((Metafile)this.image, destPoint, metafileDelegate);
                    }
                }
            }
        }

        private bool MetafileCallback(
       EmfPlusRecordType recordType,
       int flags,
       int dataSize,
       IntPtr data,
       PlayRecordCallback callbackData)
        {
            byte[] dataArray = null;
            if (data != IntPtr.Zero)
            {
                // Copy the unmanaged record to a managed byte buffer 
                // that can be used by PlayRecord.
                dataArray = new byte[dataSize];
                Marshal.Copy(data, dataArray, 0, dataSize);
                // Copy to Memory Stream dataArray.
                memoryStream.Write(dataArray, 0, dataSize);
            }
            return true;
        }
    }
}
