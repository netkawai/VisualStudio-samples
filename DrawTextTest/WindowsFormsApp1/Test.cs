using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Drawing.Text;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace WindowsFormsApp1
{
    public class Test
    {
        private const int BaseIterations = 3;
        private const int RenderIterations = 10000;

        private const string TestString = "Test-s.tri,ng m(g=j{}3)";
        private static readonly Font _font1 = new Font("Arial", 11);
        // private static readonly Font _font1 = new Font("Segoe UI", 9);
        // private static readonly Font _font1 = new Font("Tahoma", 10);
        // private static readonly Font _font1 = new Font("Microsoft Sans Serif", 9);
        // private static readonly Font _font1 = SystemFonts.DefaultFont;

        private const string TestStringS = "lllllllll HtmlLabel";
        private static readonly Font _fontS = new Font("Microsoft Sans Serif", 9, FontStyle.Bold);

        private static readonly Font _fontPerf1 = new Font("Arial", 11);
        private static readonly Font _fontPerf2 = new Font(SystemFonts.DefaultFont.FontFamily, 12);
        private static readonly Font _fontPerf3 = new Font("Arial", 12);
        private static readonly Font[] _fontsPerf = new[] { _fontPerf1, _fontPerf2, _fontPerf3 };

        public static void Run()
        {
            try
            {
                RunVisualTest(TestString, _font1, "TestVisual.png");
                RunVisualTest(TestStringS, _fontS, "TestVisual2.png");

                RunGdiVsGdiPlusVisualTest(1);
                RunGdiVsGdiPlusVisualTest(2);
                RunGdiVsGdiPlusVisualTest(3);
                RunGdiVsGdiPlusVisualTest(10);
                RunGdiVsGdiPlusVisualTest(20);
                RunGdiVsGdiPlusVisualTest(21);
                RunGdiVsGdiPlusVisualTest(30);

                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();

                RunPerformanceTest();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private static void RunVisualTest(String str, Font font, string file)
        {
            var image = new Bitmap(600, 400, PixelFormat.Format32bppArgb);

            int xOffset = image.Width / 2 + 5;
            int yOffset = 15;
            int yOffsetStep = font.Height + 15;
            var offset = (int)(font.GetHeight() / 6f);

            using (var g = Graphics.FromImage(image))
            {
                using (var b = new LinearGradientBrush(new Point(0, 0), new Point(xOffset - 15, 0), Color.LightGray, Color.White))
                    g.FillRectangle(b, 0, 0, xOffset - 15, image.Height);

                TextRenderer.DrawText(g, str + " (TextRenderer)", font, new Point(5, yOffset), Color.Red, Color.White);
                TextRenderer.DrawText(g, str + " (TextRenderer)", font, new Point(xOffset, yOffset), Color.Red);
                yOffset += yOffsetStep;

                DrawNativeString(str + " (Native)", image, 5, yOffset, font);
                DrawNativeString(str + " (Native)", image, xOffset, yOffset, font);
                yOffset += yOffsetStep;

                g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
                g.DrawString(str + " (DrawString CT)", font, Brushes.Red, new Point(5, yOffset));
                g.DrawString(str + " (DrawString CT)", font, Brushes.Red, new Point(xOffset, yOffset));
                g.TextRenderingHint = TextRenderingHint.SystemDefault;
                yOffset += yOffsetStep;

                g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
                g.DrawString(str + " (DrawString CT-T)", font, Brushes.Red, new Point(5 + offset, yOffset), StringFormat.GenericTypographic);
                g.DrawString(str + " (DrawString CT-T)", font, Brushes.Red, new Point(xOffset + offset, yOffset), StringFormat.GenericTypographic);
                g.TextRenderingHint = TextRenderingHint.SystemDefault;
                yOffset += yOffsetStep;

                g.TextRenderingHint = TextRenderingHint.AntiAlias;
                g.DrawString(str + " (DrawString AA)", font, Brushes.Red, new Point(5, yOffset));
                g.DrawString(str + " (DrawString AA)", font, Brushes.Red, new Point(xOffset, yOffset));
                g.TextRenderingHint = TextRenderingHint.SystemDefault;
                yOffset += yOffsetStep;

                g.TextRenderingHint = TextRenderingHint.AntiAlias;
                g.DrawString(str + " (DrawString AA-T)", font, Brushes.Red, new Point(5 + offset, yOffset), StringFormat.GenericTypographic);
                g.DrawString(str + " (DrawString AA-T)", font, Brushes.Red, new Point(xOffset + offset, yOffset), StringFormat.GenericTypographic);
                g.TextRenderingHint = TextRenderingHint.SystemDefault;
                yOffset += yOffsetStep;

                DrawGraphicsPathString(str + " (GraphicsPath D)", 5, yOffset, g);
                DrawGraphicsPathString(str + " (GraphicsPath D)", xOffset, yOffset, g);
                yOffset += yOffsetStep;

                g.SmoothingMode = SmoothingMode.HighQuality;
                g.CompositingQuality = CompositingQuality.HighQuality;
                DrawGraphicsPathString(str + " (GraphicsPath HQ)", 5, yOffset, g);
                DrawGraphicsPathString(str + " (GraphicsPath HQ)", xOffset, yOffset, g);
                g.SmoothingMode = SmoothingMode.Default;
            }

            image.Save(file, ImageFormat.Png);
        }

        private static void RunGdiVsGdiPlusVisualTest(int type)
        {
            var image = new Bitmap(3400, 3100, PixelFormat.Format32bppArgb);

            using (var g = Graphics.FromImage(image))
            {
                g.Clear(Color.White);

                List<Font> fonts = new List<Font>();
                foreach (var fontFamily in FontFamily.Families)
                {
                    try
                    {
                        FontStyle fontStyle = FontStyle.Regular;
                        if (type == 2)
                            fontStyle = FontStyle.Italic;
                        else if (type == 3)
                            fontStyle = FontStyle.Bold;
                        fonts.Add(new Font(fontFamily, 10, fontStyle));
                    }
                    catch
                    { }
                }

                int xOffset = 5;
                int yOffset = 5;

                foreach (var font in fonts)
                {
                    var fh = (int)font.GetHeight();
                    var str = type != 20 ? font.Name : font.Name + " " + "שלום";
                    if (type == 21)
                        str = "wwwwwwwwwwww llllllllllll wlewlewlewlewlewlewle";

                    if (type == 10)
                    {
                        var s = MeasureStringNative(str, font, g);
                        g.DrawRectangle(Pens.Black, xOffset + (int)(_font1.GetHeight() / 6f), yOffset, s.Width, s.Height);
                        s = MeasureString(str, font, g, StringFormat.GenericTypographic);
                        g.DrawRectangle(Pens.Black, xOffset + (int)(_font1.GetHeight() / 6f), yOffset + fh, s.Width, s.Height);
                        g.DrawRectangle(Pens.Black, xOffset + (int)(_font1.GetHeight() / 6f), yOffset + fh * 2, s.Width, s.Height);
                        s = MeasureString(str, font, g, StringFormat.GenericDefault);
                        g.DrawRectangle(Pens.Black, xOffset + (int)(_font1.GetHeight() / 6f), yOffset + fh * 3, s.Width, s.Height);
                    }

                    DrawNativeString(str, image, xOffset, yOffset, font);
                    g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
                    g.DrawString(str, font, Brushes.Red, new Point(xOffset + (int)(font.GetHeight() / 6f), yOffset + fh), StringFormat.GenericTypographic);
                    g.TextRenderingHint = TextRenderingHint.AntiAlias;
                    g.DrawString(str, font, Brushes.Red, new Point(xOffset + (int)(font.GetHeight() / 6f), yOffset + fh * 2), StringFormat.GenericTypographic);
                    g.DrawString(str, font, Brushes.Red, new Point(xOffset, yOffset + fh * 3));
                    yOffset += 4 * fh + 10;
                    if (yOffset > 3000)
                    {
                        yOffset = 5;
                        xOffset += 300;
                    }
                }
            }

            image.Save("GdiVsGdiPlus_" + type + ".png", ImageFormat.Png);
        }

        private static void RunPerformanceTest()
        {
            GC.Collect();
            long tr = RunPerformanceIterations(1);
            GC.Collect();
            long gp = RunPerformanceIterations(2);
            GC.Collect();
            long gp_hq = RunPerformanceIterations(3);
            GC.Collect();
            long n = RunPerformanceIterations(4);
            GC.Collect();
            long ds_ct = RunPerformanceIterations(5);
            GC.Collect();
            long ds_aa = RunPerformanceIterations(6);
            GC.Collect();
            long ds_aat = RunPerformanceIterations(7);

            var sb = new StringBuilder();
            sb.AppendFormat("Native GDI:      {0}", n).AppendLine();
            sb.AppendFormat("DrawString AA:   {0} ({1:N1})", ds_aa, ds_aa / (double)n).AppendLine();
            sb.AppendFormat("DrawString AAT:  {0} ({1:N1})", ds_aat, ds_aat / (double)n).AppendLine();
            sb.AppendFormat("DrawString CT:   {0} ({1:N1} - {2:N1})", ds_ct, ds_ct / (double)n, ds_ct / (double)ds_aa).AppendLine();
            sb.AppendFormat("TextRenderer:    {0} ({1:N1} - {2:N1})", tr, tr / (double)n, tr / (double)ds_aa).AppendLine();
            sb.AppendFormat("GraphicsPath:    {0} ({1:N1} - {2:N1})", gp, gp / (double)n, gp / (double)ds_aa).AppendLine();
            sb.AppendFormat("GraphicsPath HQ: {0} ({1:N1} - {2:N1})", gp_hq, gp_hq / (double)n, gp_hq / (double)ds_aa).AppendLine();
            MessageBox.Show(sb.ToString());
        }


        #region Performance tests

        public static long RunPerformanceIterations(int type)
        {
            var sw = Stopwatch.StartNew();

            for (int i = 0; i < BaseIterations; i++)
            {
                var font = _fontsPerf[i % _fontsPerf.Length];

                var image = new Bitmap(500, 500, PixelFormat.Format32bppArgb);
                using (var g = Graphics.FromImage(image))
                {
                    switch (type)
                    {
                        case 1:
                            RunTextRenderer(g, font);
                            break;
                        case 2:
                            RunGraphicsPathNormal(g, font);
                            break;
                        case 3:
                            RunGraphicsPathHighQuiality(g, font);
                            break;
                        case 4:
                            RunNative(image, font);
                            break;
                        case 5:
                            RunDrawStringCT(g, font);
                            break;
                        case 6:
                            RunDrawStringAA(g, font);
                            break;
                        case 7:
                            RunDrawStringAAT(g, font);
                            break;
                    }
                }
            }

            return sw.ElapsedMilliseconds;
        }

        private static void RunTextRenderer(Graphics g, Font font)
        {
            for (int i = 0; i < RenderIterations; i++)
            {
                TextRenderer.DrawText(g, TestString, font, new Point(5, 5), Color.Red);
            }
        }

        private static void RunDrawStringCT(Graphics g, Font font)
        {
            g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            for (int i = 0; i < RenderIterations; i++)
            {
                g.DrawString(TestString, font, Brushes.Red, new Point(5, 5));
            }
            g.TextRenderingHint = TextRenderingHint.SystemDefault;
        }

        private static void RunDrawStringAA(Graphics g, Font font)
        {
            g.TextRenderingHint = TextRenderingHint.AntiAlias;
            for (int i = 0; i < RenderIterations; i++)
            {
                g.DrawString(TestString, font, Brushes.Red, new Point(5, 5));
            }
            g.TextRenderingHint = TextRenderingHint.SystemDefault;
        }

        private static void RunDrawStringAAT(Graphics g, Font font)
        {
            g.TextRenderingHint = TextRenderingHint.AntiAlias;
            for (int i = 0; i < RenderIterations; i++)
            {
                g.DrawString(TestString, font, Brushes.Red, new Point(5, 5), StringFormat.GenericTypographic);
            }
            g.TextRenderingHint = TextRenderingHint.SystemDefault;
        }

        private static void RunGraphicsPathNormal(Graphics g, Font font)
        {
            var fontFamily = font.FontFamily;
            float emSize = g.DpiY * _font1.Size / 72f;
            for (int i = 0; i < RenderIterations; i++)
            {
                using (var path = new GraphicsPath())
                {
                    path.AddString(TestString, fontFamily, (int)font.Style, emSize, new Point(5, 5), StringFormat.GenericDefault);
                    g.FillPath(Brushes.Red, path);
                }
            }
        }

        private static void RunGraphicsPathHighQuiality(Graphics g, Font font)
        {
            var fontFamily = font.FontFamily;
            float emSize = g.DpiY * _font1.Size / 72f;
            g.SmoothingMode = SmoothingMode.HighQuality;
            for (int i = 0; i < RenderIterations; i++)
            {
                using (var path = new GraphicsPath())
                {
                    path.AddString(TestString, fontFamily, (int)font.Style, emSize, new Point(5, 5), StringFormat.GenericDefault);
                    g.FillPath(Brushes.Red, path);
                }
            }
            g.SmoothingMode = SmoothingMode.Default;
        }

        private static void RunNative(Image image, Font font)
        {
            // create memory buffer from desktop handle that supports alpha channel
            IntPtr dib;
            var memoryHdc = CreateMemoryHdc(IntPtr.Zero, image.Width, image.Height, out dib);
            try
            {
                // execute GDI text rendering
                var hFont = font.ToHfont();
                SelectObject(memoryHdc, hFont);
                SetTextColor(memoryHdc, (Color.Red.B & 0xFF) << 16 | (Color.Red.G & 0xFF) << 8 | Color.Red.R);
                for (int i = 0; i < RenderIterations; i++)
                {
                    TextOut(memoryHdc, 5, 5, TestString, TestString.Length);
                }

                // copy from memory buffer to image
                using (var imageGraphics = Graphics.FromImage(image))
                {
                    var imgHdc = imageGraphics.GetHdc();
                    BitBlt(imgHdc, 0, 0, image.Width, image.Height, memoryHdc, 0, 0, 0x00CC0020);
                    imageGraphics.ReleaseHdc(imgHdc);
                }

                DeleteObject(hFont);
            }
            finally
            {
                // release memory buffer
                DeleteObject(dib);
                DeleteDC(memoryHdc);
            }
        }

        #endregion



        #region Draw helper methods

        public static Size MeasureStringNative(string str, Font font, Graphics g)
        {
            var size = new Size();
            var hdc = g.GetHdc();
            var hFont = font.ToHfont();
            SelectObject(hdc, hFont);
            GetTextExtentPoint32(hdc, str, str.Length, ref size);
            DeleteObject(hFont);
            g.ReleaseHdc(hdc);
            return size;
        }

        public static Size MeasureString(string str, Font font, Graphics g, StringFormat format)
        {
            var characterRanges = new CharacterRange[1];
            characterRanges[0] = new CharacterRange(0, str.Length);
            var stringFormat = new StringFormat(format);
            stringFormat.FormatFlags = StringFormatFlags.NoClip | StringFormatFlags.MeasureTrailingSpaces;
            stringFormat.SetMeasurableCharacterRanges(characterRanges);
            var size = g.MeasureCharacterRanges(str, font, RectangleF.Empty, stringFormat)[0].GetBounds(g).Size;
            return new Size((int)Math.Round(size.Width), (int)Math.Round(size.Height));
        }

        private static void DrawNativeString(string text, Image image, int x, int y, Font font)
        {
            // adjust for native offset
            x += (int)(_font1.GetHeight() / 6f);

            IntPtr dib;
            var memoryHdc = CreateMemoryHdc(IntPtr.Zero, image.Width, image.Height, out dib);
            try
            {
                using (var imageGraphics = Graphics.FromImage(image))
                {
                    // copy image background to memory HDC so when copied back it will have the proper background
                    using (var memGraphics = Graphics.FromHdc(memoryHdc))
                        memGraphics.DrawImageUnscaled(image, 0, 0);

                    // execute GDI text rendering
                    var hFont = font.ToHfont();
                    SelectObject(memoryHdc, hFont);
                    SetTextColor(memoryHdc, (Color.Red.B & 0xFF) << 16 | (Color.Red.G & 0xFF) << 8 | Color.Red.R);
                    TextOut(memoryHdc, x, y, text, text.Length);
                    DeleteObject(hFont);

                    // copy from memory buffer to image
                    var imgHdc = imageGraphics.GetHdc();
                    BitBlt(imgHdc, x, y, image.Width, (int)(_font1.GetHeight() + 5), memoryHdc, x, y, 0x00CC0020);
                    imageGraphics.ReleaseHdc(imgHdc);
                }
            }
            finally
            {
                // release memory buffer
                DeleteObject(dib);
                DeleteDC(memoryHdc);
            }
        }

        private static void DrawGraphicsPathString(string text, int x, int y, Graphics g)
        {
            float emSize = g.DpiY * _font1.Size / 72f;
            using (var path = new GraphicsPath())
            {
                path.AddString(text, _font1.FontFamily, (int)_font1.Style, emSize, new Point(x, y), StringFormat.GenericDefault);
                g.FillPath(Brushes.Red, path);
            }
        }

        #endregion


        #region GDI Interop

        private static IntPtr CreateMemoryHdc(IntPtr hdc, int width, int height, out IntPtr dib)
        {
            // Create a memory DC so we can work off-screen
            IntPtr memoryHdc = CreateCompatibleDC(hdc);
            SetBkMode(memoryHdc, 1);

            // Create a device-independent bitmap and select it into our DC
            var info = new BitMapInfo();
            info.biSize = Marshal.SizeOf(info);
            info.biWidth = width;
            info.biHeight = -height;
            info.biPlanes = 1;
            info.biBitCount = 32;
            info.biCompression = 0; // BI_RGB
            IntPtr ppvBits;
            dib = CreateDIBSection(hdc, ref info, 0, out ppvBits, IntPtr.Zero, 0);
            SelectObject(memoryHdc, dib);

            return memoryHdc;
        }

        [DllImport("gdi32.dll")]
        public static extern int SetBkMode(IntPtr hdc, int mode);

        [DllImport("gdi32.dll", ExactSpelling = true, SetLastError = true)]
        private static extern IntPtr CreateCompatibleDC(IntPtr hdc);

        [DllImport("gdi32.dll")]
        private static extern int SetTextColor(IntPtr hdc, int color);

        [DllImport("gdi32.dll", EntryPoint = "GetTextExtentPoint32W")]
        public static extern int GetTextExtentPoint32(IntPtr hdc, [MarshalAs(UnmanagedType.LPWStr)] string str, int len, ref Size size);

        [DllImport("gdi32.dll", EntryPoint = "TextOutW")]
        private static extern bool TextOut(IntPtr hdc, int x, int y, [MarshalAs(UnmanagedType.LPWStr)] string str, int len);

        [DllImport("gdi32.dll")]
        private static extern IntPtr CreateDIBSection(IntPtr hdc, [In] ref BitMapInfo pbmi, uint iUsage, out IntPtr ppvBits, IntPtr hSection, uint dwOffset);

        [DllImport("gdi32.dll")]
        public static extern int SelectObject(IntPtr hdc, IntPtr hgdiObj);

        [DllImport("gdi32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool BitBlt(IntPtr hdc, int nXDest, int nYDest, int nWidth, int nHeight, IntPtr hdcSrc, int nXSrc, int nYSrc, long dwRop);

        [DllImport("gdi32.dll")]
        public static extern bool DeleteObject(IntPtr hObject);

        [DllImport("gdi32.dll", ExactSpelling = true, SetLastError = true)]
        public static extern bool DeleteDC(IntPtr hdc);

        [StructLayout(LayoutKind.Sequential)]
        internal struct BitMapInfo
        {
            public int biSize;
            public int biWidth;
            public int biHeight;
            public short biPlanes;
            public short biBitCount;
            public int biCompression;
            public int biSizeImage;
            public int biXPelsPerMeter;
            public int biYPelsPerMeter;
            public int biClrUsed;
            public int biClrImportant;
            public byte bmiColors_rgbBlue;
            public byte bmiColors_rgbGreen;
            public byte bmiColors_rgbRed;
            public byte bmiColors_rgbReserved;
        }

        #endregion
    }
}
