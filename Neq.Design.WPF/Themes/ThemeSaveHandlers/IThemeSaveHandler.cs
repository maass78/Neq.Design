using Neq.Design.WPF.Helpers;
using System.Collections.ObjectModel;

namespace Neq.Design.WPF.Themes.ThemeSaveHandlers
{
    public interface IThemeSaveHandler
    {
        void Save(ThemesSave themes);

        ThemesSave Load();
    }
}
