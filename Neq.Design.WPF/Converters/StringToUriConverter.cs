using System;
using System.Windows.Data;

namespace Neq.Design.WPF.Converters
{
    [ValueConversion(typeof(string), typeof(Uri))]
    public class StringToUriConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter,
          System.Globalization.CultureInfo culture) => new Uri((string)value); 

        public object ConvertBack(object value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture) => ((Uri)value).ToString();
    }
}
