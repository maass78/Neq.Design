using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Neq.Design.WPF.Controls
{
    public class NeqRadioButton : RadioButton
    {
        public static readonly DependencyProperty IconProperty =
        DependencyProperty.Register("Icon", typeof(ImageSource), typeof(NeqRadioButton), new PropertyMetadata());

        public ImageSource Icon
        {
            get { return (ImageSource)GetValue(IconProperty); }
            set { SetValue(IconProperty, value); }
        }

        public static readonly DependencyProperty IconMarginProperty =
        DependencyProperty.Register("IconMargin", typeof(Thickness), typeof(NeqRadioButton), new PropertyMetadata() { DefaultValue = new Thickness(0) });

        public Thickness IconMargin
        {
            get { return (Thickness)GetValue(IconMarginProperty); }
            set { SetValue(IconMarginProperty, value); }
        }

        public static readonly DependencyProperty CornerRadiusProperty =
        DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(NeqRadioButton), new PropertyMetadata() { DefaultValue = new CornerRadius(0) });

        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }
    }
}
