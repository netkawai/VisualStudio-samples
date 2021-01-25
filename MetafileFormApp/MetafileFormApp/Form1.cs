using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace MetafileFormApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            grfx = CreateGraphics();
            MemoryStream ms = new MemoryStream();
            ipHdc = grfx.GetHdc();

            copied_meta = new Metafile(ms, ipHdc);
        }
        IntPtr ipHdc;
        private Graphics grfx;
        private Metafile copied_meta = null;

        protected override void OnPaint(PaintEventArgs e)
        {

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
            }

            // copied_meta.PlayRecord(recordType, flags, dataSize, dataArray);

            return true;
        }
#if false
        [DllImport("gdi32.dll",CharSet = CharSet.Unicode, SetLastError = true)]
        static extern IntPtr CopyEnhMetaFile(  // Copy EMF to file
            IntPtr hemfSrc,   // Handle to EMF
            String lpszFile // File
        );
        [DllImport("gdi32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        static extern uint GetEnhMetaFileBits(IntPtr hemf, uint cbBuffer, Byte[] lpbBuffer);

        static int CF_ENHMETAFILE = 14;
        [DllImport("user32.dll")]
        static extern bool OpenClipboard(IntPtr hWndNewOwner);

        [DllImport("user32.dll")]
        static extern bool CloseClipboard();

        [DllImport("user32.dll")]
        static extern IntPtr GetClipboardData(uint format);
#endif 


        private void Button3_Click(object sender, EventArgs e)
        {

        System.Windows.IDataObject data = System.Windows.Clipboard.GetDataObject();
            if (data != null)
            {
                foreach (var format in data.GetFormats())
                {
                    Console.WriteLine(format.ToString());
                }
                if (data.GetDataPresent(System.Windows.DataFormats.EnhancedMetafile, false))
                {
                    // If the desired data format is present, use one of the GetData methods to retrieve the
                    // data from the data object.
                    //                    meta = (Metafile)data.GetData(typeof(System.Drawing.Imaging.Metafile));
                    Graphics.EnumerateMetafileProc metafileDelegate = new Graphics.EnumerateMetafileProc(MetafileCallback);

                    Metafile meta = (Metafile)data.GetData(System.Windows.DataFormats.EnhancedMetafile);
                    //                    IntPtr handle = meta.GetHenhmetafile();
                    // out of memory
                    meta.Clone();

                    using (var g = CreateGraphics())
                    {
                        // Throw exception
                        g.EnumerateMetafile(meta, new Point(0, 0), metafileDelegate);
                    }
#if false

                    uint size = GetEnhMetaFileBits(handle, 0, null);
                    IntPtr copied = CopyEnhMetaFile(enhanceHandle, @"C:\iBlast\file0.emf");

                    if (copied == IntPtr.Zero)
                    {
                        Console.WriteLine(Marshal.GetLastWin32Error());
                    }
#endif

                }
            }

#if false
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    var sr = new StreamReader(openFileDialog1.FileName);
                }
                catch (SecurityException ex)
                {
                    MessageBox.Show($"Security error.\n\nError message: {ex.Message}\n\n" +
                    $"Details:\n\n{ex.StackTrace}");
                }
            }
#endif

        }

        private void Button1_Click(object sender, EventArgs e)
        {

            copied_meta.Save(@"C:\iBlast\File0.emf", ImageFormat.Emf);

            grfx.ReleaseHdc(ipHdc);
            grfx.Dispose();
            //                    grfx.EnumerateMetafile(meta, new Point(10, 10), metafileDelegate);
            grfx.Dispose();

            //mf.Save(@"C:\iBlast\file.emf", ImageFormat.Emf);

        }

    }
}
