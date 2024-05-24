using Neq.Design.WPF.Helpers;
using System;

namespace Neq.Design.WPF.Themes.ThemeSaveHandlers
{
    public class ThemesSave
    {
        public FullyObservableCollection<Theme> Themes { get; set; }

        public DateTime DateTime { get; set; }

        public Theme CurrentTheme { get; set; }

        public bool HasNewSave { get; set; }

        public ThemesSave(FullyObservableCollection<Theme> themes, Theme currentTheme)
        {
            DateTime = DateTime.UtcNow;
            Themes = themes;
            CurrentTheme = currentTheme;
        }
    }
}
