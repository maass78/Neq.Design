using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace Neq.Design.WPF.Helpers
{
    public class UIRelayCommand : RelayCommand, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private static Dictionary<ModifierKeys, string> modifierText = new Dictionary<ModifierKeys, string>()
        {
            [ModifierKeys.None] = "",
            [ModifierKeys.Control] = "Ctrl+",
            [ModifierKeys.Control | ModifierKeys.Shift] = "Ctrl+Shift+",
            [ModifierKeys.Control | ModifierKeys.Alt] = "Ctrl+Alt+",
            [ModifierKeys.Control | ModifierKeys.Shift | ModifierKeys.Alt] = "Ctrl+Shift+Alt+",
            [ModifierKeys.Windows] = "Win+"
        };

        private Key _key;
        public Key Key
        {
            get => _key; 
            set { _key = value; Notify(); Notify(nameof(GestureText)); }
        }

        private ModifierKeys _modifiers;
        public ModifierKeys Modifiers
        {
            get => _modifiers; 
            set { _modifiers = value; Notify(); Notify(nameof(GestureText)); }
        }

        public string GestureText => modifierText[_modifiers] + _key.ToString();

        public UIRelayCommand(Action<object> execute, Predicate<object> canExecute, Key key, ModifierKeys modifiers)
            : base(execute, canExecute)
        {
            _key = key;
            _modifiers = modifiers;
        }

        private void Notify([CallerMemberName] string propertyName = "")
        {
            if (string.IsNullOrWhiteSpace(propertyName))
                return;

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
