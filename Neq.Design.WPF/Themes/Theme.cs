using Neq.Design.WPF.Exceptions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Media;

namespace Neq.Design.WPF.Themes
{
    public class Theme : INotifyPropertyChanged
    {
        private string _themeName { get; set; }

        [ThemeResource]
        public string ThemeName
        {
            get => _themeName;
            set { _themeName = value; Notify(); }
        }

        private double _fontSize { get; set; }

        [ThemeResource]
        public double FontSize
        {
            get => _fontSize;
            set { _fontSize = value; Notify(); }
        }


        private Brush _foregroundPrimary;

        [ThemeResource]
        public Brush ForegroundPrimary
        {
            get => _foregroundPrimary;
            set { _foregroundPrimary = value; Notify(); }
        }

        private Brush _foregroundSecondary;

        [ThemeResource]
        public Brush ForegroundSecondary
        {
            get => _foregroundSecondary;
            set { _foregroundSecondary = value; Notify(); }
        }

        private Brush _foregroundDisabled;

        [ThemeResource]
        public Brush ForegroundDisabled
        {
            get => _foregroundDisabled;
            set { _foregroundDisabled = value; Notify(); }
        }

        private Brush _backgroundPrimary;

        [ThemeResource]
        public Brush BackgroundPrimary
        {
            get => _backgroundPrimary;
            set { _backgroundPrimary = value; Notify(); }
        }

        private Brush _backgroundSecondary;

        [ThemeResource]
        public Brush BackgroundSecondary
        {
            get => _backgroundSecondary;
            set { _backgroundSecondary = value; Notify(); }
        }

        private Brush _backgroundStroke;

        [ThemeResource]
        public Brush BackgroundStroke
        {
            get => _backgroundStroke;
            set { _backgroundStroke = value; Notify(); }
        }

        private Brush _accent;

        [ThemeResource]
        public Brush Accent
        {
            get => _accent;
            set { _accent = value; Notify(); }
        }

        private Brush _accentHover;

        [ThemeResource]
        public Brush AccentHover
        {
            get => _accentHover;
            set { _accentHover = value; Notify(); }
        }

        private Brush _accentDisabled;

        [ThemeResource]
        public Brush AccentDisabled
        {
            get => _accentDisabled;
            set { _accentDisabled = value; Notify(); }
        }

        private Brush _controlBackground;

        [ThemeResource]
        public Brush ControlBackground
        {
            get => _controlBackground;
            set { _controlBackground = value; Notify(); }
        }

        private Brush _controlBackgroundHover;

        [ThemeResource]
        public Brush ControlBackgroundHover
        {
            get => _controlBackgroundHover;
            set { _controlBackgroundHover = value; Notify(); }
        }

        private Brush _controlBackgroundDisabled;

        [ThemeResource]
        public Brush ControlBackgroundDisabled
        {
            get => _controlBackgroundDisabled;
            set { _controlBackgroundDisabled = value; Notify(); }
        }

        private Brush _destructiveBackground;

        [ThemeResource]
        public Brush DestructiveBackground
        {
            get => _destructiveBackground;
            set { _destructiveBackground = value; Notify(); }
        }

        private Brush _destructiveForeground;

        [ThemeResource]
        public Brush DestructiveForeground
        {
            get => _destructiveForeground;
            set { _destructiveForeground = value; Notify(); }
        }

        private Brush _destructiveHover;

        [ThemeResource]
        public Brush DestructiveHover
        {
            get => _destructiveHover;
            set { _destructiveHover = value; Notify(); }
        }

        private Brush _destructiveBackgroundDisabled;

        [ThemeResource]
        public Brush DestructiveBackgroundDisabled
        {
            get => _destructiveBackgroundDisabled;
            set { _destructiveBackgroundDisabled = value; Notify(); }
        }

        private Brush _destructiveForegroundDisabled;

        [ThemeResource]
        public Brush DestructiveForegroundDisabled
        {
            get => _destructiveForegroundDisabled;
            set { _destructiveForegroundDisabled = value; Notify(); }
        }

        private Brush _scrollBarForeground;

        [ThemeResource]
        public Brush ScrollBarForeground
        {
            get => _scrollBarForeground;
            set { _scrollBarForeground = value; Notify(); }
        }

        private Brush _scrollBarBackground;

        [ThemeResource]
        public Brush ScrollBarBackground
        {
            get => _scrollBarBackground;
            set { _scrollBarBackground = value; Notify(); }
        }

        private Brush _textBoxSelection;

        [ThemeResource]
        public Brush TextBoxSelection
        {
            get => _textBoxSelection;
            set { _textBoxSelection = value; Notify(); }
        }

        private Brush _transparentBackgroundDark;

        [ThemeResource]
        public Brush TransparentBackgroundDark
        {
            get => _transparentBackgroundDark;
            set { _transparentBackgroundDark = value; Notify(); }
        }

        private Brush _transparentBackgroundLight;

        [ThemeResource]
        public Brush TransparentBackgroundLight
        {
            get => _transparentBackgroundLight;
            set { _transparentBackgroundLight = value; Notify(); }
        }

        private Brush _focusVisualBrush;

        [ThemeResource]
        public Brush FocusVisualBrush
        {
            get => _focusVisualBrush;
            set { _focusVisualBrush = value; Notify(); }
        }

        private CornerRadius _cornerRadius;

        [ThemeResource]
        public CornerRadius CornerRadius
        {
            get => _cornerRadius;
            set { _cornerRadius = value; Notify(); }
        }

        private bool _isTheme;

        [ThemeResource]
        public bool IsTheme
        {
            get => _isTheme;
            set { _isTheme = value; Notify(); }
        }




        private ResourceDictionary _compiledResources;

        public ResourceDictionary CompiledResources
        {
            get
            {
                if (_compiledResources == null)
                    Refresh();

                return _compiledResources;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Загружает тему из словаря ресурсов xaml
        /// </summary>
        /// <param name="uri">Uri-путь к словарю ресурсов, из которого необходимо загрузить тему</param>
        public Theme(Uri uri) : this(new ResourceDictionary() { Source = uri }) { }

        /// <summary>
        /// Загружает тему из словаря ресурсов
        /// </summary>
        /// <param name="resources">Словарь ресурсов темы</param>
        public Theme(ResourceDictionary resources) : this()
        {
            bool isTheme = false;

            foreach (string key in resources.Keys)
            {
                if (key == Themes.IsThemeFlag && resources[key] is bool flag && flag)
                {
                    isTheme = true;
                    continue;
                }

                var prop = GetType().GetProperty(key);

                if (prop == null)
                    continue;

                var attrs = prop.GetCustomAttributes(true);

                foreach (var attr in attrs)
                {
                    if (attr is ThemeResourceAttribute)
                        prop.SetValue(this, resources[key], null);
                }
            }

            if (!isTheme)
                throw new ThemeException($"В {nameof(ResourceDictionary)} не найден флаг {isTheme} равный true. Вставьте в словарь: <system:Boolean x:Key=\"IsTheme\">True</system:Boolean>");
        }

        /// <summary>
        /// Создает тему на основе заданной <paramref name="basedOn"/>
        /// </summary>
        /// <param name="name">Имя темы</param>
        /// <param name="basedOn">Родительская тема</param>
        public Theme(Theme basedOn) : this() 
        {
            foreach (var prop in GetType().GetProperties())
            {
                var attrs = prop.GetCustomAttributes(true);

                foreach (var attr in attrs)
                {
                    if (attr is ThemeResourceAttribute)
                        prop.SetValue(this, prop.GetValue(basedOn), null);
                }
            }

        }

        /// <summary>
        /// Создает тему, в которой не определены цвета и прочие свойства. Не забудьте определить, если вам это нужно!
        /// </summary>
        public Theme()
        {
            IsTheme = true;
        }

        /// <summary>
        /// Полностью обновляет <see cref="CompiledResources"/>. Обычно не требуется
        /// </summary>
        public void Refresh()
        {
            _compiledResources = new ResourceDictionary();

            var props = GetType().GetProperties();

            foreach (var prop in props)
                WriteToCompiled(prop);
        }

        private void Notify([CallerMemberName] string propertyName = "")
        {
            if (!string.IsNullOrEmpty(propertyName) && _compiledResources != null)
                WriteToCompiled(GetType().GetProperty(propertyName));

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void WriteToCompiled(PropertyInfo property)
        {
            var attrs = property.GetCustomAttributes(true);

            foreach (var attr in attrs)
            {
                if (attr is ThemeResourceAttribute)
                    _compiledResources[property.Name] = property.GetValue(this);
            }
        }

        public ReadOnlyCollection<ThemeResourceModel> GetThemeResources()
        {
            var list = new List<ThemeResourceModel>();

            var props = GetType().GetProperties();

            foreach (var prop in props)
            {
                var attrs = prop.GetCustomAttributes(true);

                foreach (var attr in attrs)
                {
                    if (attr is ThemeResourceAttribute themeResourceAttr)
                        list.Add(new ThemeResourceModel(this, prop.Name, themeResourceAttr.Order));
                }
            }

            return list.OrderBy(x => x.Order).ToList().AsReadOnly();
        }

        public override string ToString() => ThemeName;
    }
}
