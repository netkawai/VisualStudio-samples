using System;
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
using System.Windows.Shapes;

namespace WpfAppTemplatePractice
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
        }
        private void Expander_Collapsed(object sender, RoutedEventArgs e)
        {
            leftColumn.Width = new GridLength(1, GridUnitType.Star);
            rightColumn.Width = new GridLength(1, GridUnitType.Auto);
        }

        private void Expander_Expanded(object sender, RoutedEventArgs e)
        {
            rightColumn.Width = new GridLength(1, GridUnitType.Star);
        }
    }
}
