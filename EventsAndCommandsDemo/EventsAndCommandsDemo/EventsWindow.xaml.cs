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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Diagnostics;

namespace EventsAndCommandsDemo
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class EventsWindow : Window
    {
        public EventsWindow()
        {
            InitializeComponent();
            AddHandler(UIElement.MouseLeftButtonDownEvent, (RoutedEventHandler)WindowLevelMouseDownCallMeAlwaysHandler,true);
            m_Image.AddHandler(UIElement.MouseLeftButtonDownEvent, (RoutedEventHandler)ImageMouseLeftButtonDownHandler);
        }

        private void myButton_Click(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("Button Click event, XAML hookup");
        }

        private void ImageMouseLeftButtonDownHandler(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("Image mouse left button down event");
            //e.Handled = true;
        }

        private void WindowLevelMouseDownCallMeAlwaysHandler(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("Window level handler that gets called always");
        }

        private void GridPreviewMouseDownHandler(object sender, MouseButtonEventArgs e)
        {
            Debug.WriteLine("Grid preview mouse left button down event, XAML hookup");
        }

        private void ImageElementMouseDown(object sender, MouseButtonEventArgs e)
        {
            Debug.WriteLine("Image Element mouse left button down event, XAML hookup");
        }
    }
}
