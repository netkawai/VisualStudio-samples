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
using System.Diagnostics;

namespace EventsAndCommandsDemo
{
    /// <summary>
    /// Interaction logic for CommandWindow.xaml
    /// </summary>
    public partial class CommandWindow : Window
    {
        public CommandWindow()
        {
            InitializeComponent();
            AddHandler(CommandManager.PreviewCanExecuteEvent, (RoutedEventHandler)WindowPreviewCanExecuteEventHandler,true);
            AddHandler(CommandManager.CanExecuteEvent, (RoutedEventHandler)WindowCanExecuteEventHandler,true);
            uc1.AddHandler(CommandManager.PreviewCanExecuteEvent, (RoutedEventHandler)UserControlPreviewCanExecuteEventHandler,true);
            uc1.AddHandler(CommandManager.CanExecuteEvent, (RoutedEventHandler)UserControlCanExecuteEventHandler,true);
        }

        private void WindowPreviewCanExecuteEventHandler(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("Window PreviewCanExecute event handler called");
        }

        private void WindowCanExecuteEventHandler(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("Window CanExecute event handler called");
        }

        private void UserControlPreviewCanExecuteEventHandler(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("User control PreviewCanExecute event handler called");
        }

        private void UserControlCanExecuteEventHandler(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("User control CanExecute event handler called");
        }

        private void WindowCanExecuteHandler(object sender, CanExecuteRoutedEventArgs e)
        {
            Debug.WriteLine("Window CanExecute command handler executing");
            e.CanExecute = true;
        }

        private void WindowExecutedHandler(object sender, ExecutedRoutedEventArgs e)
        {
            Debug.WriteLine("Window Executed command handler executing");
        }

        private void CommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {

        }
    }
}
