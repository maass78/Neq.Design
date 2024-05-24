using System.Windows;
using Neq.Design.WPF.Windows;
using Neq.Design.WPF.Themes.ThemeSaveHandlers;
using System.Windows.Media;

namespace Neq.Design.WPF.Test
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Themes.Themes themes = new Themes.Themes(Themes.Themes.DarkTheme, Application.Current.Resources, new FileThemeSaveHandler("themes.txt"), Themes.Themes.DefaultThemes);

            sliderCornerRadius.Value = themes.CurrentTheme.CornerRadius.TopLeft;

            sliderCornerRadius.ValueChanged += (s, e) => themes.CurrentTheme.CornerRadius = new CornerRadius(sliderCornerRadius.Value);
            //rect1.Fill = new LinearGradientBrush(Colors.White, Colors.Red, new Point(0, 0), new Point(1, 0))
            //{
            //    //GradientStops = new GradientStopCollection()
            //    //{
            //    //    new GradientStop(, 0),
            //    //    new GradientStop(, 1),
            //    //}
            //};

            //rect1.OpacityMask = new LinearGradientBrush(Colors.Black, Color.FromArgb(0, 0, 0, 0), new Point(0, 0), new Point(0, 1));


            check.Checked += (s, e) => { themes.CurrentTheme = themes.ThemesCollection[0]; };
            check.Unchecked += (s, e) => themes.CurrentTheme = themes.ThemesCollection[1];

            new NeqThemeEditorWindow(themes).Show();
        }

       
    }
}
