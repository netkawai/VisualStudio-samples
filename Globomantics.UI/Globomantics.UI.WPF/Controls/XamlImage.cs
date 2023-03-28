﻿using System.Windows;
using System.Windows.Controls;

namespace Globomantics.UI.WPF.Controls
{
    public class XamlImage : Control
    {
        static XamlImage()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(XamlImage), 
                new FrameworkPropertyMetadata(typeof(XamlImage)));

            IsTabStopProperty.OverrideMetadata(typeof(XamlImage), 
                new FrameworkPropertyMetadata(false));
        }
    }
}
