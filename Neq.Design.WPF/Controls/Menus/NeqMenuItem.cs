using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Neq.Design.WPF.Controls
{
    public class NeqMenuItem : MenuItem
    {
        public static readonly DependencyProperty IconSourceProperty =
            DependencyProperty.Register("IconSource", typeof(object), typeof(NeqMenuItem), new PropertyMetadata());

        public object IconSource
        {
            get { return (ImageSource)GetValue(IconSourceProperty); }
            set { SetValue(IconSourceProperty, value); }
        }

        public static readonly DependencyProperty IconMarginProperty =
            DependencyProperty.Register("IconMargin", typeof(Thickness), typeof(NeqMenuItem), new PropertyMetadata() { DefaultValue = new Thickness(0) });

        public Thickness IconMargin
        {
            get { return (Thickness)GetValue(IconMarginProperty); }
            set { SetValue(IconMarginProperty, value); }
        }

        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(NeqMenuItem), new PropertyMetadata() { DefaultValue = new CornerRadius(0) });

        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }

        public static readonly DependencyProperty InputGestureTextBrushProperty =
         DependencyProperty.Register("InputGestureTextBrush", typeof(Brush), typeof(NeqMenuItem), new PropertyMetadata());

        public Brush InputGestureTextBrush
        {
            get { return (Brush)GetValue(InputGestureTextBrushProperty); }
            set { SetValue(InputGestureTextBrushProperty, value); }
        }

    }
}
