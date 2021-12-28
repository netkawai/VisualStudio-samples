using Microsoft.Graphics.Canvas.UI.Xaml;
using Microsoft.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Web.Http.Filters;
using Windows.Security.Cryptography.Certificates;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace WinUIApp1
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.InitializeComponent();
        }

        private async void myButton_Click(object sender, RoutedEventArgs e)
        {
            myButton.Content = "Clicked";

            // Try to use UWP API (Windows.Web)
            var baseFilter = new HttpBaseProtocolFilter();
#if DEBUG
            baseFilter.IgnorableServerCertificateErrors.Add(ChainValidationResult.Expired);
            baseFilter.IgnorableServerCertificateErrors.Add(ChainValidationResult.Untrusted);
            baseFilter.IgnorableServerCertificateErrors.Add(ChainValidationResult.InvalidName);
#endif
            var uri = new System.Uri("https://www.bing.com");
            using (var httpClient = new Windows.Web.Http.HttpClient(baseFilter))
            {
                // Always catch network exceptions for async methods
                try
                {
                    string result = await httpClient.GetStringAsync(uri);
                }
                catch (Exception ex)
                {
                    // Details in ex.Message and ex.HResult.
                }
            }
        }

        private void nextButton_Click(object sender, RoutedEventArgs e)
        {
            // This is a pure .NET (Syste.NET)
            var uri = new System.Uri("https://www.bing.com");
            var handler = new System.Net.Http.HttpClientHandler()
            {
                ServerCertificateCustomValidationCallback = System.Net.Http.HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
            };

            using (var http = new System.Net.Http.HttpClient(handler))
            {
                // Always catch network exceptions for async methods
                try
                {
                    var res = http.GetAsync(uri);
                }
                catch (Exception ex)
                {
                    // Details in ex.Message and ex.HResult.
                }

            }
        }


        void CanvasControl_Draw(CanvasControl sender, CanvasDrawEventArgs args)
        {
            args.DrawingSession.DrawEllipse(155, 115, 80, 30, Colors.Black, 3);
            args.DrawingSession.DrawText("Hello, world!", 100, 100, Colors.Yellow);
        }
    }
}
