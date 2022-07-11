using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace EventsAndCommandsDemo
{
    /// <summary>
    /// Interaction logic for StartupWindow.xaml
    /// </summary>
    public partial class StartupWindow : Window
    {
        public StartupWindow()
        {
            InitializeComponent();
        }

        private void ShowEventsWindow(object sender, RoutedEventArgs e)
        {
            new EventsWindow().Show();
        }

        private void ShowCommandWindow(object sender, RoutedEventArgs e)
        {
            new CommandWindow().Show();
        }

        private void ShowSimpleCommandWindow(object sender, RoutedEventArgs e)
        {
            new SimpleCommandWindow().Show();
        }
    }
}
