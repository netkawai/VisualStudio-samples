using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace CustomCommandsDemo
{
    public class SimpleViewPresentationModel : DependencyObject
    {
        public SimpleViewPresentationModel()
        {
            CookDinnerCommand = new StringDelegateCommand();
        }
        public static readonly DependencyProperty CookDinnerCommandProperty = DependencyProperty.Register("CookDinnerCommand", typeof(StringDelegateCommand), typeof(SimpleViewPresentationModel));

        public StringDelegateCommand CookDinnerCommand
        {
            // IMPORTANT: To maintain parity between setting a property in XAML and procedural code, do not touch the getter and setter inside this dependency property!
            get
            {
                return (StringDelegateCommand)GetValue(CookDinnerCommandProperty);
            }
            set
            {
                SetValue(CookDinnerCommandProperty, value);
            }
        }
        
    }
}
