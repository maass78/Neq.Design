using System;
using System.Globalization;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace Neq.Design.WPF.Converters
{
    [ValueConversion(typeof(ImageSource), typeof(Image))]
    internal class SourceToImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) 
            => value == null ? null : (value is ImageSource image ? new Image() { Source = image.Clone() } : value);

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => value is Image image ? image.Source : value;
    }
}