using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows;

namespace Neq.Design.WPF.Converters
{
    [ValueConversion(typeof(CornerRadius), typeof(double))]
    public class CornerRadiusToDoubleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((CornerRadius)value).TopLeft;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return new CornerRadius((double)value);
        }
    }
}
