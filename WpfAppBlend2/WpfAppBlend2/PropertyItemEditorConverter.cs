using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Controls.Primitives;
using System.Windows;

namespace WpfAppBlend2
{
    public class PropertyItemEditorConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if ((values == null) || (values.Length != 2))
                return null;

            var editor = values[0];
            var isPropertyItemReadOnly = values[1] as bool?;

            if ((editor == null) ||  !isPropertyItemReadOnly.HasValue)
                return editor;

            return editor;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        private bool IsPropertySetLocally(object editor, DependencyProperty dp)
        {
            if (dp == null)
                return false;

            var editorObject = editor as DependencyObject;
            if (editorObject == null)
                return false;

            var valueSource = DependencyPropertyHelper.GetValueSource(editorObject, dp);

            return (valueSource.BaseValueSource == BaseValueSource.Local);
        }
    }
}
