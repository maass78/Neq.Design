using System;

namespace Neq.Design.WPF.Exceptions
{
    /// <summary>
    /// Исключение при взаимодействии с темами
    /// </summary>
    public class ThemeException : Exception
    {
        public ThemeException(string message) : base(message) { }
    }
}
