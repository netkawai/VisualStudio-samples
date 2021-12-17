using System;
using System.Collections.Generic;
using System.Text;

namespace WpfApp1
{
    class Program
    {
        [STAThread]
        public static void Main()
        {

            global::WinRT.ComWrappersSupport.InitializeComWrappers();

            System.Windows.Application app = new System.Windows.Application();
            app.StartupUri = new System.Uri("MainWindow.xaml", System.UriKind.Relative);
            app.Run();
        }
    }
}
