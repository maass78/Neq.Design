using Neq.Design.WPF.Windows;

namespace Neq.Design.WPF
{
    public static class NeqMessageBox
    {
        public static void Show(string caption, string message) => new NeqMessageBoxWindow(caption, message).ShowDialog();
    }
}
