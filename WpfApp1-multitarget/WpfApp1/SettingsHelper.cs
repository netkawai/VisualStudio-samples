using System;
using Microsoft.Win32;
using System.Windows.Media;
#if WINDOWS_UWP
using Windows.Foundation.Metadata;
using Windows.UI.ViewManagement;
#endif 

namespace WpfApp1
{
    public static class SettingsHelper
    {
#if WINDOWS_UWP
        private static Color ToColor(Windows.UI.Color color)
        {
            return Color.FromArgb(color.A, color.R, color.G, color.B);
        }

        private static bool IsDarkBackground(Color color)
        {
            return color.R + color.G + color.B < (255 * 3 - color.R - color.G - color.B);
        }

#endif
        public static bool IsDarkSystemTheme()
        {
#if WINDOWS_UWP
            UISettings _uiSettings = new UISettings();
            return IsDarkBackground(ToColor(_uiSettings.GetColorValue(UIColorType.Background)));
#else
            return false; // always;
#endif
        }
    }
}
