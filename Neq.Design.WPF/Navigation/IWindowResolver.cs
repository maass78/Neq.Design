using System.Windows;

namespace Neq.Design.WPF.Navigation
{
    public interface IWindowResolver
    {
        Window Resolve(string alias);
    }
}
