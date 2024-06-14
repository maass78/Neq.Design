namespace Neq.Design.WPF.Colors.Types
{
    public struct HSLColor
    {
        public double H;

        public double S;

        public double L;

        public double A;

        public static readonly HSLColor Black = new HSLColor()
        {
            A = 1,
            H = 0,
            S = 0,
            L = 0,
        };

        public static readonly HSLColor White = new HSLColor()
        {
            A = 1,
            H = 0,
            S = 0,
            L = 1,
        };

        public static readonly HSLColor Red = new HSLColor()
        {
            A = 1,
            H = 0,
            S = 1,
            L = 0.5,
        };
    }
}
