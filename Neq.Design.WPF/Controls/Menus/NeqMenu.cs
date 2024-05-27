using System.Windows;
using System.Windows.Controls;

namespace Neq.Design.WPF.Controls
{
    public class NeqMenu : Menu
    {
        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(NeqMenu), new PropertyMetadata() { DefaultValue = new CornerRadius(0) });

        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }

        public static readonly DependencyProperty OrientaionProperty =
            DependencyProperty.Register("Orientaion", typeof(Orientation), typeof(NeqMenu), new PropertyMetadata() { DefaultValue = Orientation.Horizontal });

        public Orientation Orientaion
        {
            get { return (Orientation)GetValue(OrientaionProperty); }
            set { SetValue(OrientaionProperty, value); }
        }
    }
}
