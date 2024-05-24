using System.Windows.Media;
using System.Windows;
using System.Windows.Controls;

namespace Neq.Design.WPF.Controls
{
    public class NeqTextBox : TextBox
    {
        public static readonly DependencyProperty HintColorProperty =
            DependencyProperty.Register("HintColor", typeof(Brush), typeof(NeqTextBox), new PropertyMetadata() { DefaultValue = new SolidColorBrush() });

        public Brush HintColor
        {
            get { return (Brush)GetValue(HintColorProperty); }
            set { SetValue(HintColorProperty, value); }
        }

        public static readonly DependencyProperty HintTextProperty =
            DependencyProperty.Register("HintText", typeof(string), typeof(NeqTextBox), new PropertyMetadata() { DefaultValue = string.Empty });

        public string HintText
        {
            get { return (string)GetValue(HintTextProperty); }
            set { SetValue(HintTextProperty, value); }
        }

        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(NeqTextBox), new PropertyMetadata() { DefaultValue = new CornerRadius(0) });

        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }
    }
}
