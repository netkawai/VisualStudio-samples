using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;

namespace Globomantics.UI.WPF
{
    public sealed class Theme
    {

        [ThreadStatic]
        private static Dictionary<string, ResourceDictionary> resourceDictionaries;


        static Theme()
        {

            resourceDictionaries = new Dictionary<string, ResourceDictionary>();
            var dark = new ResourceDictionary();
            dark.Source = new Uri(@"Globomantics.UI.WPF;;;component/AppThemes/Dark.xaml", UriKind.Relative);
            resourceDictionaries.Add("Dark", dark);

            var light = new ResourceDictionary();
            light.Source = new Uri(@"Globomantics.UI.WPF;;;component/AppThemes/Light.xaml", UriKind.Relative);
            resourceDictionaries.Add("Light", light);
        }

        public static ThemeType ThemeType { get; set; } = ThemeType.Light;

        public static void LoadThemeType(ThemeType type)
        {
            ThemeType = type;
            foreach (var resource in resourceDictionaries.Values)
            {
                if (Application.Current.Resources.MergedDictionaries.Contains(resource))
                    Application.Current.Resources.MergedDictionaries.Remove(resource);
            }

            switch (type)
            {
                case ThemeType.Light:
                    {
                        Application.Current.Resources.MergedDictionaries.Add(resourceDictionaries["Light"]);
                        break;
                    }
                case ThemeType.Dark:
                    {
                        Application.Current.Resources.MergedDictionaries.Add(resourceDictionaries["Dark"]);
                        break;
                    }
            }

        }


        private static LinearGradientBrush GetWindowHeaderGradient()
        {
            return new LinearGradientBrush
            {
                StartPoint = new Point(0, 0.5),
                EndPoint = new Point(1, 0.5),
                GradientStops = new GradientStopCollection
                    {
                        new GradientStop { Offset = 0, Color = ColorFromHex("#FF833AB4") },
                        new GradientStop { Offset = 1, Color = ColorFromHex("#FFFD1D1D") }
                    }
            };
        }


        internal static Color ColorFromHex(string colorHex)
        {
            return (Color?)ColorConverter.ConvertFromString(colorHex) ?? Colors.Transparent;
        }
    }
}
