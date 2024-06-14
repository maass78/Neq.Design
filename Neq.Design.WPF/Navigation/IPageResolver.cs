using System.Windows.Controls;

namespace Neq.Design.WPF.Navigation
{
    public interface IPageResolver
    {
        Page Resolve(string alias);
    }
}
