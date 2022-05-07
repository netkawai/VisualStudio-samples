using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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
    /// Follow steps 1a or 1b and then 2 to use this custom control in a XAML file.
    ///
    /// Step 1a) Using this custom control in a XAML file that exists in the current project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:WpfAppBlend2"
    ///
    ///
    /// Step 1b) Using this custom control in a XAML file that exists in a different project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:WpfAppBlend2;assembly=WpfAppBlend2"
    ///
    /// You will also need to add a project reference from the project where the XAML file lives
    /// to this project and Rebuild to avoid compilation errors:
    ///
    ///     Right click on the target project in the Solution Explorer and
    ///     "Add Reference"->"Projects"->[Browse to and select this project]
    ///
    ///
    /// Step 2)
    /// Go ahead and use your control in the XAML file.
    ///
    ///     <MyNamespace:PropertyGrid/>
    ///
    /// </summary>
    [TemplatePart(Name = PART_DragThumb, Type = typeof(Thumb))]
    public class PropertyGrid : Control
    {
        private const string PART_DragThumb = "PART_DragThumb";
        private Thumb _dragThumb;

        static PropertyGrid()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PropertyGrid), new FrameworkPropertyMetadata(typeof(PropertyGrid)));
        }

        public static DependencyProperty PropertiesProperty = DependencyProperty.Register(nameof(Properties), typeof(List<PropertyItemView>), typeof(PropertyGrid));

        public List<PropertyItemView> Properties { get { return (List<PropertyItemView>)GetValue(PropertiesProperty); } set { SetValue(PropertiesProperty,value); } }




        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            if (_dragThumb != null)
                _dragThumb.DragDelta -= DragThumb_DragDelta;
            _dragThumb = GetTemplateChild(PART_DragThumb) as Thumb;
            if (_dragThumb != null)
                _dragThumb.DragDelta += DragThumb_DragDelta;


            //Update TranslateTransform in code-behind instead of XAML to remove the
            //output window error.
            //When we use FindAncesstor in custom control template for binding internal elements property 
            //into its ancestor element, Visual Studio displays data warning messages in output window when 
            //binding engine meets unmatched target type during visual tree traversal though it does the proper 
            //binding when it receives expected target type during visual tree traversal
            //ref : http://www.codeproject.com/Tips/124556/How-to-suppress-the-System-Windows-Data-Error-warn
            TranslateTransform _moveTransform = new TranslateTransform();
            _moveTransform.X = NameColumnWidth;
            if (_dragThumb != null)
            {
                _dragThumb.RenderTransform = _moveTransform;
            }

            this.UpdateThumb();
        }

        private void UpdateThumb()
        {
            if (_dragThumb != null)
            {
                 _dragThumb.Margin = new Thickness(-1, 0, 0, 0);
            }
        }

        public static readonly DependencyProperty NameColumnWidthProperty = DependencyProperty.Register("NameColumnWidth", typeof(double), typeof(PropertyGrid), new UIPropertyMetadata(150.0, OnNameColumnWidthChanged));
        public double NameColumnWidth
        {
            get
            {
                return (double)GetValue(NameColumnWidthProperty);
            }
            set
            {
                SetValue(NameColumnWidthProperty, value);
            }
        }

        private static void OnNameColumnWidthChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            PropertyGrid propertyGrid = o as PropertyGrid;
            if (propertyGrid != null)
                propertyGrid.OnNameColumnWidthChanged((double)e.OldValue, (double)e.NewValue);
        }

        protected virtual void OnNameColumnWidthChanged(double oldValue, double newValue)
        {
            if (_dragThumb != null)
                ((TranslateTransform)_dragThumb.RenderTransform).X = newValue;
        }
        private void DragThumb_DragDelta(object sender, DragDeltaEventArgs e)
        {
            NameColumnWidth = Math.Min(Math.Max(this.ActualWidth * 0.1, NameColumnWidth + e.HorizontalChange), this.ActualWidth * 0.9);
        }
    }
}
