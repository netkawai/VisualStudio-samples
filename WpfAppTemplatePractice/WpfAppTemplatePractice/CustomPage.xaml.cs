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
using System.Windows.Navigation;
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
    /// Interaction logic for CustomPage.xaml
    /// </summary>
    public partial class CustomPage : UserControl
    {
        ObservableCollection<TestViewListItem> Items = new();

        public CustomPage()
        {
            Items.Add(new TestViewListItem() { Label = "test", Note = "test note" });
            Items.Add(new TestViewListItem() { Label = "test2", Note = "test2 note" });

            InitializeComponent();
            DataContext = this;
        }
    }
}
