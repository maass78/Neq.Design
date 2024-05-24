using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using System.Windows;

namespace Neq.Design.WPF.Controls
{
    public class NeqToggleButton : ToggleButton
    {
        public static readonly DependencyProperty IconProperty =
        DependencyProperty.Register("Icon", typeof(ImageSource), typeof(NeqToggleButton), new PropertyMetadata());

        public ImageSource Icon
        {
            get { return (ImageSource)GetValue(IconProperty); }
            set { SetValue(IconProperty, value); }
        }

        public static readonly DependencyProperty IconMarginProperty =
        DependencyProperty.Register("IconMargin", typeof(Thickness), typeof(NeqToggleButton), new PropertyMetadata() { DefaultValue = new Thickness(0) });

        public Thickness IconMargin
        {
            get { return (Thickness)GetValue(IconMarginProperty); }
            set { SetValue(IconMarginProperty, value); }
        }

        public static readonly DependencyProperty CornerRadiusProperty =
        DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(NeqToggleButton), new PropertyMetadata() { DefaultValue = new CornerRadius(0) });

        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }

        public NeqToggleButton() { }
    }
}
