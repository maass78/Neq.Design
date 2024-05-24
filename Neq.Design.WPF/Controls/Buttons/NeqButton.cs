using System.Windows;
using System.Windows.Controls;

namespace Neq.Design.WPF.Controls
{
    public class NeqButton : Button
    {
        public static readonly DependencyProperty IconProperty =
        DependencyProperty.Register("Icon", typeof(object), typeof(NeqButton), new PropertyMetadata());

        public object Icon
        {
            get { return (object)GetValue(IconProperty); }
            set { SetValue(IconProperty, value); }
        }

        public static readonly DependencyProperty IconMarginProperty =
        DependencyProperty.Register("IconMargin", typeof(Thickness), typeof(NeqButton), new PropertyMetadata() { DefaultValue = new Thickness(0) });

        public Thickness IconMargin
        {
            get { return (Thickness)GetValue(IconMarginProperty); }
            set { SetValue(IconMarginProperty, value); }
        }

        public static readonly DependencyProperty CornerRadiusProperty =
        DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(NeqButton), new PropertyMetadata() { DefaultValue = new CornerRadius(0) });

        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }

        public NeqButton() { }
    }
}
