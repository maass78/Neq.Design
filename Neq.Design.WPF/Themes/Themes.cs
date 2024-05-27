using Neq.Design.WPF.Exceptions;
using Neq.Design.WPF.Helpers;
using Neq.Design.WPF.Themes.ThemeSaveHandlers;
using System;
using System.ComponentModel;
using System.Windows;

namespace Neq.Design.WPF.Themes
{
    /// <summary>
    /// Управление темами Neq.Design
    /// </summary>
    public class Themes : INotifyPropertyChanged
    {
        /// <summary>
        /// Ключ для определения, тема ли это
        /// </summary>
        public const string IsThemeFlag = "IsTheme";

        public static readonly Theme LightTheme = new Theme(new Uri("/Neq.Design.WPF;component/Themes/LightTheme.xaml", UriKind.RelativeOrAbsolute));
        public static readonly Theme DarkTheme = new Theme(new Uri("/Neq.Design.WPF;component/Themes/DarkTheme.xaml", UriKind.RelativeOrAbsolute));
        
        public static readonly FullyObservableCollection<Theme> DefaultThemes = new FullyObservableCollection<Theme>() { LightTheme, DarkTheme };


        private ResourceDictionary _appResources;

        private Theme _currentTheme;

        public event PropertyChangedEventHandler PropertyChanged;

        public Themes(Theme startTheme, ResourceDictionary appResources, IThemeSaveHandler themeSaveHandler, FullyObservableCollection<Theme> defaultThemes)
        {
            _appResources = appResources;
            ThemeSaveHandler = themeSaveHandler;

            if (ThemeSaveHandler != null)
            {
                try
                {
                    var load = ThemeSaveHandler.Load();

                    if (load == null)
                    {
                        ThemesCollection = defaultThemes;
                        CurrentTheme = startTheme;
                    }
                    else
                    {
                        ThemesCollection = load.Themes;
                        CurrentTheme = load.CurrentTheme;
                    }

                }
                catch
                {
                    ThemesCollection = defaultThemes;
                    CurrentTheme = startTheme;
                }
            }
            else
            {
                ThemesCollection = defaultThemes;
                CurrentTheme = startTheme;
            }

            ThemesCollection.CollectionChanged += (s, e) => Save();
            ThemesCollection.ItemPropertyChanged += (s, e) => Save();
        }

        public Themes(Theme startTheme, ResourceDictionary appResources) : this(startTheme, appResources, null, DefaultThemes) { }

        public Themes(Theme startTheme) : this(startTheme, Application.Current.Resources) { }

        public Themes(Theme startTheme, IThemeSaveHandler themeSaveHandler) : this(startTheme, Application.Current.Resources, themeSaveHandler, DefaultThemes) { }

        public FullyObservableCollection<Theme> ThemesCollection { get; private set; }
        public IThemeSaveHandler ThemeSaveHandler { get; }

        public Theme CurrentTheme
        {
            get => _currentTheme;
            set
            {
                if (_appResources == null)
                    throw new ThemeException($"Не найден словарь ресурсов. Проверьте, что вы передали в конструктор {nameof(Themes)} ресурсы приложения");

                _currentTheme = value;

                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CurrentTheme)));


                ResourceDictionary oldDict = null;

                foreach (var dict in _appResources.MergedDictionaries)
                {
                    foreach (object keyObj in dict.Keys)
                    {
                        if (!(keyObj is string key))
                            continue;

                        if (key == IsThemeFlag && dict[key] is bool isTheme && isTheme)
                            oldDict = dict;
                    }
                }
               

                if (oldDict != null)
                {
                    int ind = _appResources.MergedDictionaries.IndexOf(oldDict);
                    _appResources.MergedDictionaries.Remove(oldDict);
                    _appResources.MergedDictionaries.Insert(ind, _currentTheme.CompiledResources);
                }
                else
                {
                    _appResources.MergedDictionaries.Add(_currentTheme.CompiledResources);
                }

                Save();
            }
        }
        
        private void Save() => ThemeSaveHandler?.Save(new ThemesSave(ThemesCollection, CurrentTheme));
    }
}
