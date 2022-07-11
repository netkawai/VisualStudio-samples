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
    /// Interaction logic for CommandChildUserControl.xaml
    /// </summary>
    public partial class CommandChildUserControl : UserControl
    {
        public CommandChildUserControl()
        {
            InitializeComponent();
        }

        private void OnCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            Debug.WriteLine(string.Format("User control {0} CanExecute Called",Name));
            e.CanExecute = true;
        }

        private void OnExecute(object sender, ExecutedRoutedEventArgs e)
        {
            Debug.WriteLine(string.Format("User control {0} Executed Called", Name));
        }
    }
}
