using System;
using System.Windows;
using System.Windows.Controls;

namespace WpfAppBlend2
{
    public class PropertyGridHelper
    {
#if false
        public static readonly DependencyProperty FontWeightProperty =
                DependencyProperty.RegisterAttached("FontWeight", typeof(FontWeight), typeof(PropertyGridHelper), new UIPropertyMetadata(System.Windows.FontWeights.Normal));

        [AttachedPropertyBrowsableForType(typeof(MultiLineEditorTextBox))]
        public static FontWeight GetFontWeight(DependencyObject ctrl)
        {
            return (FontWeight)ctrl.GetValue(FontWeightProperty);
        }

        [AttachedPropertyBrowsableForType(typeof(MultiLineEditorTextBox))]
        public static void SetFontWeight(DependencyObject ctrl, FontWeight value)
        {
           ctrl.SetValue(FontWeightProperty, value);
        }
#endif

    }
}
