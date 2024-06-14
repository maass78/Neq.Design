using System.Windows;

namespace Neq.Design.WPF.Windows
{
    public partial class NeqMessageBoxWindow : Window
    {
        public NeqMessageBoxWindow(string caption, string message)
        {
            InitializeComponent();

            buttonClose.Click += (s, e) => Close();
            buttonMinimize.Click += (s, e) => WindowState = WindowState.Minimized;

            Title = caption;
            text.Text = message;

            WindowStartupLocation = WindowStartupLocation.CenterScreen;

            buttonOk.Click += (s, e) => Close();
        }
    }
}
