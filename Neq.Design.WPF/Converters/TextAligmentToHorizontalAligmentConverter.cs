using System;
using System.Windows.Data;
using System.Windows;

namespace Neq.Design.WPF.Converters
{
    [ValueConversion(typeof(TextAlignment), typeof(HorizontalAlignment))]
    public class TextAligmentToHorizontalAligmentConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            switch ((TextAlignment)value)
            {
                case TextAlignment.Left:
                    return HorizontalAlignment.Left;
                case TextAlignment.Center:
                    return HorizontalAlignment.Center;
                case TextAlignment.Right:
                    return HorizontalAlignment.Right;
                default:
                    return HorizontalAlignment.Stretch;
            }

        } 

        public object ConvertBack(object value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            switch ((HorizontalAlignment)value)
            {
                case HorizontalAlignment.Left:
                    return TextAlignment.Left;
                case HorizontalAlignment.Center:
                    return TextAlignment.Center;
                case HorizontalAlignment.Right:
                    return TextAlignment.Right;
                default:
                    return TextAlignment.Justify;
            }
        }
    }
}
