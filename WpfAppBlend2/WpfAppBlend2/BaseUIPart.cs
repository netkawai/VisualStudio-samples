using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace WpfAppBlend2
{
    internal class BaseUIPart : StackPanel // If I inherit from Panel, does not show anything why???
    {

        // if I need to define this DP as value inheritable, I need to RegisterAttach, otherwise, it does not work.
        public static readonly DependencyProperty IsHatchProperty =
                    DependencyProperty.Register(
                                "IsHatch",
                                typeof(bool),
                                typeof(UIElement),
                                new PropertyMetadata(false));
        public bool IsHatch
        {
            get { return (bool)GetValue(IsHatchProperty); }
            set { SetValue(IsHatchProperty, value); }
        }
    }

    internal class ChildPanel : BaseUIPart
    {
        static ChildPanel()
        {
            // Override original Register
            IsHatchProperty.OverrideMetadata(typeof(ChildPanel), new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.Inherits));


        }
    }

    internal class GrandChildPanel : ChildPanel
    {
        public GrandChildPanel()
        {
            this.Loaded += GrandChildPanel_Loaded;
        }

        private void GrandChildPanel_Loaded(object sender, RoutedEventArgs e)
        {
            if (this.Children.Count > 0)
            {
                var ui = this.Children[0];
                ((TextBlock)ui).Text = ((bool)GetValue(IsHatchProperty)).ToString();
            }
        }
    }
}
