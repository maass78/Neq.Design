using System.Windows.Media;
using System.Windows;
using System.Windows.Controls;

namespace Neq.Design.WPF.Controls
{
    public class NeqSlider : Slider
    {
        public static readonly DependencyProperty ThumbBorderBrushProperty =
            DependencyProperty.Register("ThumbBorderBrush", typeof(Brush), typeof(NeqSlider), new PropertyMetadata());

        public Brush ThumbBorderBrush
        {
            get { return (Brush)GetValue(ThumbBorderBrushProperty); }
            set { SetValue(ThumbBorderBrushProperty, value); }
        }

        public static readonly DependencyProperty ThumbBorderThicknessProperty =
            DependencyProperty.Register("ThumbBorderThickness", typeof(Thickness), typeof(NeqSlider), new PropertyMetadata());

        public Thickness ThumbBorderThickness
        {
            get { return (Thickness)GetValue(ThumbBorderThicknessProperty); }
            set { SetValue(ThumbBorderThicknessProperty, value); }
        }

        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(NeqSlider), new PropertyMetadata() { DefaultValue = new CornerRadius(0) });

        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }
    }
}
