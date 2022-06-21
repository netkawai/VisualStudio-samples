using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Shapes;
using Microsoft.Web.WebView2.Core;
using Path = System.IO.Path;

namespace WpfApp1Blend
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        bool firstload = true;
        BrowserViewModel Model = new BrowserViewModel();
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = Model;
            InitializeAsync();
        }

        // called from Window Constructor after InitializeComponent()
        // note: the `async void` signature is required for environment init
        async void InitializeAsync()
        {
            // must create a data folder if running out of a secured folder that can't write like Program Files
            var env = await CoreWebView2Environment.CreateAsync(userDataFolder: Path.Combine(Path.GetTempPath(), "WpfApp1Blend"));

            // NOTE: this waits until the first page is navigated - then continues
            //       executing the next line of code!
            await webView.EnsureCoreWebView2Async(env);

            if (Model.AutoOpenDevTools)
                webView.CoreWebView2.OpenDevToolsWindow();

            // Almost always need this event for something    
            webView.NavigationCompleted += WebView_NavigationCompleted;

            // set the initial URL
            webView.Source = new Uri("https://google.ca");
        }

        private void WebView_NavigationCompleted(object sender, Microsoft.Web.WebView2.Core.CoreWebView2NavigationCompletedEventArgs e)
        {
            if (e.IsSuccess)
                Model.Url = webView.Source.ToString();

            if (firstload)
            {
                firstload = false;
                webView.Visibility = Visibility.Visible;
            }
        }
    }
}
