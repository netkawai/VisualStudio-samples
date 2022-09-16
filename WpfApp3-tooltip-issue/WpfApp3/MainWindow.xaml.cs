using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reflection;
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

namespace WpfApp3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Model _model;
        ToolTipEventHandler _ttEventHandler;

        public MainWindow()
        {
            _ttEventHandler = new ToolTipEventHandler(OnToolTipEvent);
            ChangeToolTipDefaults();
            _model = new Model(20);
            DataContext = _model;

            InitializeComponent();
        }

        void ChangeToolTipDefaults()
        {
            // To make the tooltip behavior easy to perceive in this sample app,
            // change the defaults for InitialShowDelay and ShowDuration to be
            // 5 seconds and 15 seconds, respectively.  A real app would do this
            // by setting those properties directly in markup or in an appropriate
            // Style, but for convenience do it by private reflection.  (Private
            // reflection is not supported in a real app - do not try this at home.)

            PropertyMetadata initialShowDelay = ToolTipService.InitialShowDelayProperty.DefaultMetadata;
            PropertyMetadata showDuration = ToolTipService.ShowDurationProperty.DefaultMetadata;

            FieldInfo fiDefaultValue = typeof(PropertyMetadata).GetField("_defaultValue", BindingFlags.Instance | BindingFlags.NonPublic);
            if (fiDefaultValue != null)
            {
                fiDefaultValue.SetValue(initialShowDelay, 5000);
                fiDefaultValue.SetValue(showDuration, 60000);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            AddToolTipEventHandlers(this);
        }

        // Find all elements that own a ToolTip, and register a handler for
        // the opening and closing events.
        private void AddToolTipEventHandlers(DependencyObject d)
        {
            UIElement uie = d as UIElement;
            ContentElement ce = d as ContentElement;

            // add handlers for d
            if (d.GetValue(ToolTipProperty) != null)
            {
                if (uie != null)
                {
                    uie.AddHandler(ToolTipService.ToolTipOpeningEvent, _ttEventHandler, handledEventsToo: true);
                    uie.AddHandler(ToolTipService.ToolTipClosingEvent, _ttEventHandler, handledEventsToo: true);
                }
                else if (ce != null)
                {
                    ce.AddHandler(ToolTipService.ToolTipOpeningEvent, _ttEventHandler, handledEventsToo: true);
                    ce.AddHandler(ToolTipService.ToolTipClosingEvent, _ttEventHandler, handledEventsToo: true);
                }
            }

            // recursively add handlers for descendants of d

            // visual subtree (exclude the ListBox - adding items to the list
            // while showing/hiding a tooltip in the list is too weird)
            if (uie == _listbox)
                uie = null;

            if (uie != null)
            {
                int n = VisualTreeHelper.GetChildrenCount(d);
                for (int i=0; i<n; ++i)
                {
                    AddToolTipEventHandlers(VisualTreeHelper.GetChild(d, i));
                }
            }

            // logical subtree (of ContentElement and certain other special-cases) 
            FlowDocumentScrollViewer fdsv;
            d = null;

            if (ce != null) d = ce;
            else if (uie != null)
            {
                if (uie is TextBlock) d = uie;
                else if ((fdsv = uie as FlowDocumentScrollViewer) != null) d = fdsv.Document;
            }

            if (d != null)
            {
                foreach (object child in LogicalTreeHelper.GetChildren(d))
                {
                    DependencyObject doChild = child as DependencyObject;
                    if (doChild != null)
                    {
                        AddToolTipEventHandlers(doChild);
                    }
                }
            }
        }

        private void OnToolTipEvent(object sender, ToolTipEventArgs e)
        {
            LogEvent(e);
        }

        private void LogEvent(RoutedEventArgs e)
        {
            DependencyObject d = e.Source as DependencyObject;
            string name = (string) d?.GetValue(NameProperty);
            if (String.IsNullOrEmpty(name))
            {
                name = e.Source.ToString();
                if (name.StartsWith("System."))
                {
                    int index = name.IndexOf(' ');
                    if (index < 0) index = name.Length;
                    index = name.Substring(0, index).LastIndexOf('.');
                    name = name.Substring(index + 1);
                }
            }

            DataItem item = new DataItem { Title = String.Format("{0:T} {1}{2} on {3}", 
                DateTime.Now, e.RoutedEvent.Name, (e.Handled) ? " HANDLED" : "", name) };
            _model.Data.Add(item);
            _listbox.ScrollIntoView(item);
        }
    }

    public class CheckBoxToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Visibility)
            {
                Visibility v = (Visibility)value;
                value = (bool?)(v == Visibility.Visible);
            }

            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool?)
            {
                bool? b = (bool?)value;
                value = (b == true) ? Visibility.Visible : Visibility.Collapsed;
            }
            return value;
        }
    }
}
