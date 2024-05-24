using Neq.Design.WPF.Helpers;
using Neq.Design.WPF.Themes;
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

                _themeResourcesCollectionViewSource.Source = value?.GetThemeResources();
                ThemeResourcesCollectionView?.Refresh();
                Notify(nameof(ThemeResourcesCollectionView));
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

        private Themes.Themes _themes;


        private RelayCommand _addThemeCommand;
        public RelayCommand AddThemeCommand
        {
            get
            {
                if (_addThemeCommand == null)
                    _addThemeCommand = new RelayCommand(RemoveTheme, CanRemoveTheme);

                return _addThemeCommand;
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



        public event PropertyChangedEventHandler PropertyChanged;

        private void Notify([CallerMemberName] string propertyName = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
