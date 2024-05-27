using Neq.Design.WPF.Helpers;
using Neq.Design.WPF.Themes;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Data;

namespace Neq.Design.WPF.Windows.ViewModels
{
    public class ThemeEditorViewModel : INotifyPropertyChanged
    {
        private string _themeSearch;
        public string ThemesSearch
        {
            get => _themeSearch;
            set
            {
                _themeSearch = value;
                Notify();
                ThemesCollectionView?.Refresh();
            }
        }

        private Theme _selectedTheme;
        public Theme SelectedTheme
        {
            get => _selectedTheme;
            set
            {
                _selectedTheme = value;
                Notify();

                IsSelectedThemeActive = _selectedTheme == _themes.CurrentTheme;
                AddSelectedTheme = _selectedTheme ?? _themes.CurrentTheme;

                _themeResourcesCollectionViewSource.Source = value?.GetThemeResources();
                ThemeResourcesCollectionView?.Refresh();
                Notify(nameof(ThemeResourcesCollectionView));
            }
        }

        private Theme _addSelectedTheme;
        public Theme AddSelectedTheme
        {
            get => _addSelectedTheme;
            set
            {
                _addSelectedTheme = value;
                Notify();
            }
        }

        private bool _isSelectedThemeActive;
        public bool IsSelectedThemeActive
        {
            get => _isSelectedThemeActive;
            set
            {
                _isSelectedThemeActive = value;
                Notify();
            }
        }

        private bool _isAddPopupVisible;
        public bool IsAddPopupVisible
        {
            get => _isAddPopupVisible;
            set
            {
                _isAddPopupVisible = value;
                Notify();
            }
        }


        private string _themeResourcesSearch;
        public string ThemeResourcesSearch
        {
            get => _themeResourcesSearch;
            set
            {
                _themeResourcesSearch = value;
                Notify();
                ThemeResourcesCollectionView?.Refresh();
            }
        }


        private CollectionViewSource _themesCollectionViewSource;

        private CollectionViewSource _themeResourcesCollectionViewSource;

        public ICollectionView ThemesCollectionView => _themesCollectionViewSource.View;

        public ICollectionView ThemeResourcesCollectionView => _themeResourcesCollectionViewSource.View;

        public ObservableCollection<Theme> AllThemes => _themes.ThemesCollection;

        private Themes.Themes _themes;


        private RelayCommand _addThemeCommand;
        public RelayCommand AddThemeCommand
        {
            get
            {
                if (_addThemeCommand == null)
                    _addThemeCommand = new RelayCommand(AddTheme, CanAddTheme);

                return _addThemeCommand;
            }
        }
        
        private RelayCommand _setThemeCommand;
        public RelayCommand SetThemeCommand
        {
            get
            {
                if (_setThemeCommand == null)
                    _setThemeCommand = new RelayCommand(SetTheme, CanSetTheme);

                return _setThemeCommand;
            }
        }


        private RelayCommand _removeThemeCommand;
        public RelayCommand RemoveThemeCommand 
        {
            get 
            {
                if (_removeThemeCommand == null)
                    _removeThemeCommand = new RelayCommand(RemoveTheme, CanRemoveTheme);

                return _removeThemeCommand; 
            } 
        }

        public ThemeEditorViewModel(Themes.Themes themes)
        {
            _themes = themes;
            AddSelectedTheme = SelectedTheme ?? _themes.CurrentTheme;

            _themesCollectionViewSource = new CollectionViewSource()
            {
                Source = themes.ThemesCollection
            };

            _themeResourcesCollectionViewSource = new CollectionViewSource();

            _themesCollectionViewSource.Filter += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(ThemesSearch))
                {
                    e.Accepted = true;
                    return;
                }

                var item = e.Item as Theme;

                e.Accepted = item.ThemeName.ToLower().Contains(ThemesSearch.ToLower());
            };

            _themeResourcesCollectionViewSource.Filter += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(ThemeResourcesSearch))
                {
                    e.Accepted = true;
                    return;
                }

                var item = e.Item as ThemeResourceModel;

                e.Accepted = item.ResourceName.ToLower().Contains(ThemeResourcesSearch.ToLower());
            };
        }

        private void RemoveTheme(object param)
        {
            if (SelectedTheme != null)
                _themes.ThemesCollection.Remove(SelectedTheme);
        }

        private bool CanRemoveTheme(object param) => _themes?.ThemesCollection != null && _themes.ThemesCollection.Count > 1 && SelectedTheme != _themes.CurrentTheme;

        private void SetTheme(object param)
        {
            if (SelectedTheme != null)
                _themes.CurrentTheme = SelectedTheme;
        }

        private bool CanSetTheme(object param) => _themes?.ThemesCollection != null && _themes.ThemesCollection.Count > 1 && SelectedTheme != _themes.CurrentTheme;


        private void AddTheme(object param)
        {
            if (AddSelectedTheme == null)
                return;

            var theme = new Theme(AddSelectedTheme) { ThemeName = "New Theme" };
            _themes.ThemesCollection.Add(theme);
            SelectedTheme = theme;

            IsAddPopupVisible = false;
        }

        private bool CanAddTheme(object param) => AddSelectedTheme != null;

        public event PropertyChangedEventHandler PropertyChanged;

        private void Notify([CallerMemberName] string propertyName = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
