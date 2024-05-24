using Neq.Design.WPF.Themes;
using Neq.Design.WPF.Windows.ViewModels;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace Neq.Design.WPF.Windows
{

    public class ThemeResourceTemplateSelector : DataTemplateSelector
    {
        public DataTemplate ForSolidColorBrush { get; set; }
        public DataTemplate ForCornerRadius { get; set; }
        public DataTemplate ForString { get; set; }
        public DataTemplate Default { get; set; }

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

            return Default;
        }
    }

    //public class ThemeResourceModel : INotifyPropertyChanged
    //{
    //    public Theme Theme { get; set; }
    //    public string ResourceName { get; set; }

    //    public ThemeResourceModel(Theme theme, string resourceName)
    //    {
    //        Theme = theme;
    //        ResourceName = resourceName;
    //        Theme.PropertyChanged += (s, e) =>
    //        {
    //            if (e.PropertyName == ResourceName)
    //                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Resource)));
    //        };

    //    }

    //    public object Resource { get => Theme.GetType().GetProperty(ResourceName).GetValue(Theme); set => Theme.GetType().GetProperty(ResourceName).SetValue(Theme, value); }

    //    public event PropertyChangedEventHandler PropertyChanged;
    //}

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
