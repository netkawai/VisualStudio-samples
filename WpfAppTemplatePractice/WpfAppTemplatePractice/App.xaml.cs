using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace WpfAppTemplatePractice
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override async void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var viewForWin = new DialogTest();
            var mainWindow = new DialogWindow();
            mainWindow.ViewModel = viewForWin;
            var customPage = new CustomPage();
            viewForWin.Content = customPage;

            mainWindow.Show();
        }

    }
}
