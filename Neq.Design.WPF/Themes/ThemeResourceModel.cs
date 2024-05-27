using System.ComponentModel;

namespace Neq.Design.WPF.Themes
{
    public class ThemeResourceModel : INotifyPropertyChanged
    {
        private Theme _theme;

        public ThemeResourceModel(Theme theme, string resourceName, int order = 0)
        {
            ResourceName = resourceName;
            Order = order;

            _theme = theme;
            _theme.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == ResourceName)
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Resource)));
            };

        }

        public int Order { get; }

        public string ResourceName { get; }
        
        public object Resource { get => _theme.GetType().GetProperty(ResourceName).GetValue(_theme); set => _theme.GetType().GetProperty(ResourceName).SetValue(_theme, value); }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
