using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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

    public class TestViewListItem : INotifyPropertyChanged
    {
        public Color TextColor { get; set; }
        public bool IsChecked { get; set; } = true;
        public string Label { get; set; }
        public string Note { get; set; }
        public object Tag { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;
    }

    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        ObservableCollection<TestViewListItem> Items = new();

        public Window1()
        {
            Items.Add(new TestViewListItem() { Label = "test", Note = "test note" });
            Items.Add(new TestViewListItem() { Label = "test2", Note = "test2 note" });

            InitializeComponent();
            DataContext = this;
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
