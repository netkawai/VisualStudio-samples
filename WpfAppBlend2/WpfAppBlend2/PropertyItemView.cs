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
    ///     <MyNamespace:PropertyItemView/>
    ///
    /// </summary>
    public class PropertyItemView : Control
    {
        static PropertyItemView()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PropertyItemView), new FrameworkPropertyMetadata(typeof(PropertyItemView)));
        }

        #region DisplayName

        public static readonly DependencyProperty DisplayNameProperty =
            DependencyProperty.Register("DisplayName", typeof(string), typeof(PropertyItemView), new UIPropertyMetadata(null));

        public string DisplayName
        {
            get { return (string)GetValue(DisplayNameProperty); }
            set { SetValue(DisplayNameProperty, value); }
        }

        #endregion //DisplayName

        public static readonly DependencyProperty EditorProperty = DependencyProperty.Register("Editor", typeof(FrameworkElement), typeof(PropertyItemView), new UIPropertyMetadata(null, OnEditorChanged));
        public FrameworkElement Editor
        {
            get
            {
                return (FrameworkElement)GetValue(EditorProperty);
            }
            set
            {
                SetValue(EditorProperty, value);
            }
        }

        private static void OnEditorChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            PropertyItemView propertyItem = o as PropertyItemView;
            if (propertyItem != null)
                propertyItem.OnEditorChanged((FrameworkElement)e.OldValue, (FrameworkElement)e.NewValue);
        }

        private void OnEditorChanged(FrameworkElement oldValue, FrameworkElement newValue)
        {

        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
        }

        public static readonly DependencyProperty IsReadOnlyProperty =
    DependencyProperty.Register("IsReadOnly", typeof(bool), typeof(PropertyItemView), new UIPropertyMetadata(false, OnIsReadOnlyChanged));

        public bool IsReadOnly
        {
            get { return (bool)GetValue(IsReadOnlyProperty); }
            set { SetValue(IsReadOnlyProperty, value); }
        }

        private static void OnIsReadOnlyChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            var propertyItem = o as PropertyItemView;
            if (propertyItem != null)
                propertyItem.OnIsReadOnlyChanged((bool)e.OldValue, (bool)e.NewValue);
        }

        protected virtual void OnIsReadOnlyChanged(bool oldValue, bool newValue)
        {

        }
    }
}
