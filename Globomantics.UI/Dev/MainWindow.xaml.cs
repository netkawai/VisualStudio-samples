using System.Windows;
using Globomantics.UI.WPF;

namespace Dev
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void OnClick(object sender, RoutedEventArgs e)
        {
            var theme = Theme.ThemeType == ThemeType.Light ? ThemeType.Dark : ThemeType.Light;
            Theme.LoadThemeType(theme);
        }

        public string XamlImageKey { get; set; } = "Icon_TeRex";
    }
}