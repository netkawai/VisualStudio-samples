using System;
using System.Collections.Generic;
using System.Text;
#if WINDOWS_UWP
using Windows.Web.Http;
#endif

namespace WpfApp1
{
    class Program
    {
        [STAThread]
        public static void Main()
        {

#if WINDOWS_UWP
            var client = new HttpClient();

            client.GetAsync(new Uri("https://google.com")).GetAwaiter().GetResult();
#else
            // Do nothing or use System.Net.Http
#endif

            System.Windows.Application app = new System.Windows.Application();
            app.StartupUri = new System.Uri("MainWindow.xaml", System.UriKind.Relative);
            app.Run();
        }
    }
}
