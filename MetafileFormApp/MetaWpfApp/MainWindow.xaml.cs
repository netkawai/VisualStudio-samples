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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Drawing.Imaging;

namespace MetaWpfApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private Metafile meta;



        private void Button_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.IDataObject data = System.Windows.Clipboard.GetDataObject();
            if (data != null)
            {
                foreach (var format in data.GetFormats())
                {
                    Console.WriteLine(format.ToString());
                }
                if (data.GetDataPresent(System.Windows.DataFormats.EnhancedMetafile, false))
                {
                    // If the desired data format is present, use one of the GetData methods to retrieve the
                    // data from the data object.
                    meta = (Metafile)data.GetData(System.Windows.DataFormats.EnhancedMetafile);
                    MetafileHeader metaheader = meta.GetMetafileHeader();
                    Console.Write(metaheader.Type);


                }
            }

        }
    }
}
