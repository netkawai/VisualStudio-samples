using System;
using Microsoft.Win32;
using Windows.Foundation.Metadata;
using Windows.UI.ViewManagement;
using System.Windows.Media;
namespace WpfApp1
{
    public static class SettingsHelper
    {
        private static Color ToColor(Windows.UI.Color color)
        {
            return Color.FromArgb(color.A, color.R, color.G, color.B);
        }
        public static bool IsDarkSystemTheme()
        {
            UISettings _uiSettings = new UISettings();
            return IsDarkBackground(ToColor(_uiSettings.GetColorValue(UIColorType.Background)));
        }
        private static bool IsDarkBackground(Color color)
        {
            return color.R + color.G + color.B < (255 * 3 - color.R - color.G - color.B);
        }
    }
}
