using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Neq.Design.WPF.Controls
{
    public class NeqComboBox : ComboBox
    {
        public static readonly DependencyProperty CornerRadiusProperty =
           DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(NeqComboBox), new PropertyMetadata() { DefaultValue = new CornerRadius(0) });

        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }

        public static readonly DependencyProperty SelectionBrushProperty =
          DependencyProperty.Register("SelectionBrush", typeof(Brush), typeof(NeqComboBox), new PropertyMetadata() { DefaultValue = new SolidColorBrush() });

        public Brush SelectionBrush
        {
            get { return (Brush)GetValue(SelectionBrushProperty); }
            set { SetValue(SelectionBrushProperty, value); }
        }
    }
}
