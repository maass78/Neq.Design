using System;
using System.Windows;
using System.Windows.Data;

namespace Neq.Design.WPF.Converters
{
    [ValueConversion(typeof(bool), typeof(Visibility))]
    public class BoolToVisibiltyConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture) => (bool)value ? Visibility.Visible : Visibility.Hidden;

        public object ConvertBack(object value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture) => (Visibility)value == Visibility.Visible;
    }
}