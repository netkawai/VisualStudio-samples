﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public string ThemeName { get; set; }

        public MainWindow()
        {
            if (SettingsHelper.IsDarkSystemTheme())
                ThemeName = "Dark";
            else
                ThemeName = "Light";

            InitializeComponent();
            DataContext = this;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
#if WINDOWS_UWP
            new CommunityToolkit.WinUI.Notifications.ToastContentBuilder()
                .AddText("This is a test notification")
                .Show(); 
            // Not seeing the Show() method? Make sure you have version 7.0, and if you're
#endif
        }
    }
}
