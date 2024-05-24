using System;
using System.Windows;
using System.Windows.Data;

namespace Neq.Design.WPF.Converters
{
    [ValueConversion(typeof(bool?), typeof(Visibility))]
    public class BoolNullToVisibiltyConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture) => (bool?)value ?? false ? Visibility.Visible : Visibility.Collapsed;
      
        public object ConvertBack(object value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture) => (Visibility)value == Visibility.Visible;
    }
}