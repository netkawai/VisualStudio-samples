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

namespace CustomCommandsDemo
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class SimpleView : Window
    {
        SimpleViewPresenter m_Presenter = new SimpleViewPresenter();
        public SimpleView()
        {
            InitializeComponent();
            DataContext = m_Presenter.Model;
        }

        private void OnAddHandler(object sender, RoutedEventArgs e)
        {
            m_Presenter.AddCommandHandler();
        }
    }
}
