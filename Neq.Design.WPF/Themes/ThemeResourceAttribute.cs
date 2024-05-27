using System;
using System.Runtime.CompilerServices;

namespace Neq.Design.WPF.Themes
{
    public class ThemeResourceAttribute : Attribute
    {
        public ThemeResourceAttribute([CallerLineNumber] int order = 0)
        {
            Order = order;
        }

        public int Order { get; }
    }
}
