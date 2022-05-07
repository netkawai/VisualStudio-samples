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

namespace WpfAppBlend2
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class UserControl1 : UserControl
    {
        public UserControl1()
        {
            InitializeComponent();
            this.Loaded += UserControl1_Loaded;
        }

        private void UserControl1_Loaded(object sender, RoutedEventArgs e)
        {
            var properties = new List<PropertyItemView>();

            // Initialize the control
            var prop = new PropertyItemView() { DisplayName = "MultiLine", Editor = new TextBox() };
            properties.Add(prop);
            prop = new PropertyItemView() { DisplayName = "ComboBox", Editor = new ComboBox() };
            properties.Add(prop);

            prop = new PropertyItemView() { DisplayName = "TextBox", Editor = new TextBox() };
            properties.Add(prop);

            this.PropertyList.Properties = properties;
        }
    }
}
