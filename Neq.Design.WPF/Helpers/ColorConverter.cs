using Neq.Design.WPF.Helpers.Types;
using System;
using System.Windows.Media;

namespace Neq.Design.WPF.Helpers
{
    public static class ColorConverter
    {
        public static Color HSLToRGB(HSLColor hsl) => HSLToRGB(hsl.H, hsl.S, hsl.L, hsl.A);
        public static Color HSLToRGB(double h, double s, double l, double a)
        {
            if (h < 0 || h > 360)
                throw new ArgumentException(nameof(h));

            if (s < 0 || s > 1)
                throw new ArgumentException(nameof(s));

            if (l < 0 || l > 1)
                throw new ArgumentException(nameof(l));

            if (a < 0 || a > 1)
                throw new ArgumentException(nameof(a));

            double c = (1 - Math.Abs(2 * l - 1)) * s;
            double x = c * (1 - Math.Abs((h / 60) % 2 - 1));

            double m = l - c / 2;

            double r1;
            double g1;
            double b1;


            switch ((int)(h / 60))
            {
                case 0:
                    r1 = c;
                    g1 = x;
                    b1 = 0;
                    break;

                case 1:
                    r1 = x;
                    g1 = c;
                    b1 = 0;
                    break;

                case 2:
                    r1 = 0;
                    g1 = c;
                    b1 = x;
                    break;

                case 3:
                    r1 = 0;
                    g1 = x;
                    b1 = c;
                    break;

                case 4:
                    r1 = x;
                    g1 = 0;
                    b1 = c;
                    break;

                case 5:
                case 6:
                    r1 = c;
                    g1 = 0;
                    b1 = x;
                    break;
                default:
                    throw new ArgumentException(nameof(h));
            }

            return new Color()
            {
                R = (byte)Math.Round(255 * (r1 + m)),
                G = (byte)Math.Round(255 * (g1 + m)),
                B = (byte)Math.Round(255 * (b1 + m)),
                A = (byte)Math.Round(a * 255)
            };
        }

        public static HSLColor RGBToHSL(Color rgb) => RGBToHSL(rgb.A, rgb.R, rgb.G, rgb.B);
        public static HSLColor RGBToHSL(byte a, byte r, byte g, byte b)
        {
            HSLColor hsl = new HSLColor() { A = a / 255d };

            double r1 = r / 255d;
            double g1 = g / 255d;
            double b1 = b / 255d;

            double cMax = Math.Max(r1, Math.Max(g1, b1));
            double cMin = Math.Min(r1, Math.Min(g1, b1));

            double delta = cMax - cMin;

            hsl.L = (cMax + cMin) / 2;

            if (delta == 0)
            {
                hsl.H = 0;
                hsl.S = 0;
                return hsl;
            }
            
            hsl.S = delta / (1 - Math.Abs(2 * hsl.L - 1));

            if (cMax == r1)
                hsl.H = 60 * (((g1 - b1) / delta) % 6);
            else if (cMax == g1)
                hsl.H = 60 * (((b1 - r1) / delta) + 2);
            else if (cMax == b1)
                hsl.H = 60 * (((r1 - g1) / delta) + 4);

            if (hsl.H < 0)
                hsl.H = 360 + hsl.H;

            return hsl;
        }
    }
}

