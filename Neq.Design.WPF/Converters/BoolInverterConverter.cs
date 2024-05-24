using System;
using System.Windows.Data;

namespace Neq.Design.WPF.Converters
{
    [ValueConversion(typeof(bool), typeof(bool))]
    public class BoolInverterConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter,
          System.Globalization.CultureInfo culture) => !(bool)value;

        public object ConvertBack(object value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture) => !(bool)value;
    }
}
