using Neq.Design.WPF.Themes;
using Neq.Design.WPF.Windows.ViewModels;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Neq.Design.WPF.Windows
{
    public class ThemeResourceTemplateSelector : DataTemplateSelector
    {
        public DataTemplate ForSolidColorBrush { get; set; }
        public DataTemplate ForCornerRadius { get; set; }
        public DataTemplate ForString { get; set; }
        public DataTemplate Default { get; set; }
        public DataTemplate ForDouble { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            var themeResource = (ThemeResourceModel)item;

            if (themeResource.Resource is SolidColorBrush)
            {
                return ForSolidColorBrush;
            }
            else if (themeResource.Resource is CornerRadius)
            {
                return ForCornerRadius;
            }
            else if (themeResource.Resource is string)
            {
                return ForString;
            }
            else if (themeResource.Resource is double)
            {
                return ForDouble;
            }

            return Default;
        }
    }

    public partial class NeqThemeEditorWindow : Window
    {
        public NeqThemeEditorWindow(Themes.Themes themes)
        {
            InitializeComponent();

            buttonClose.Click += (s, e) => Close();
            buttonMinimize.Click += (s, e) => WindowState = WindowState.Minimized;
            buttonMaximize.Click += (s, e) => WindowState = WindowState == WindowState.Normal ? WindowState.Maximized : WindowState.Normal;

            DataContext = new ThemeEditorViewModel(themes);
            
            double margin = SystemParameters.WindowResizeBorderThickness.Left + SystemParameters.WindowNonClientFrameThickness.Left;

            StateChanged += (s, e) =>
            {
                if (WindowState == WindowState.Maximized)
                    gridMain.Margin = new Thickness(margin);
                else 
                    gridMain.Margin = new Thickness(0);
            };
        }
    }
}
