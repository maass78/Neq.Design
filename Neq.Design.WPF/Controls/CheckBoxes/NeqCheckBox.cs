using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Neq.Design.WPF.Controls
{
    public class NeqCheckBox : CheckBox
    {
        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(NeqCheckBox), new PropertyMetadata() { DefaultValue = new CornerRadius(0) });

        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }

        public static readonly DependencyProperty CheckBrushProperty =
            DependencyProperty.Register("CheckBrush", typeof(Brush), typeof(NeqCheckBox), new PropertyMetadata() { DefaultValue = new SolidColorBrush() });

        public Brush CheckBrush
        {
            get { return (Brush)GetValue(CheckBrushProperty); }
            set { SetValue(CheckBrushProperty, value); }
        }
    }
}
