using System.ComponentModel;

namespace Neq.Design.WPF.Themes
{
    public class ThemeResourceModel : INotifyPropertyChanged
    {
        private Theme _theme;

        public ThemeResourceModel(Theme theme, string resourceName)
        {
            ResourceName = resourceName;
            
            _theme = theme;
            _theme.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == ResourceName)
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Resource)));
            };

        }

        public string ResourceName { get; }
        
        public object Resource { get => _theme.GetType().GetProperty(ResourceName).GetValue(_theme); set => _theme.GetType().GetProperty(ResourceName).SetValue(_theme, value); }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
