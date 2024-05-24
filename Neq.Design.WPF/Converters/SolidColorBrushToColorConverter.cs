using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace Neq.Design.WPF.Converters
{
    [ValueConversion(typeof(SolidColorBrush), typeof(Color))]
    public class SolidColorBrushToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((SolidColorBrush)value).Color;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return new SolidColorBrush((Color)value);
        }
    }
}
