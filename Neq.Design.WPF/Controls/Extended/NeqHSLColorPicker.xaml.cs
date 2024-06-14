using System.Windows.Controls;
using System.Windows.Media;
using ColorConverter = Neq.Design.WPF.Colors.ColorConverter;
using System.Windows;
using System.Linq;
using System.Globalization;
using System;
using System.Collections.Generic;
using Neq.Design.WPF.Colors.Types;

namespace Neq.Design.WPF.Controls.Extended
{
    public partial class NeqHSLColorPicker : UserControl
    {
        private enum HandleFrom
        {
            None,
            Sliders,
            HSLATextBoxes,
            HEXARGB,
            ARGB,
            HSLA
        }

        private HSLColor _hsl;
        private bool _frozen = false;
        private int _round;

        public static readonly DependencyProperty SelectedColorProperty =
        DependencyProperty.Register("SelectedColor", typeof(Color), typeof(NeqHSLColorPicker), new FrameworkPropertyMetadata(System.Windows.Media.Colors.Red) { PropertyChangedCallback = OnSelectedColorPropertyChanged });

        public Color SelectedColor
        {
            get
            {
                return (Color)GetValue(SelectedColorProperty);
            }
            set
            {
                SetValue(SelectedColorProperty, value);
            }
        }

        private static void OnSelectedColorPropertyChanged(DependencyObject source,
            DependencyPropertyChangedEventArgs e)
        {
            NeqHSLColorPicker control = source as NeqHSLColorPicker;
            Color color = (Color)e.NewValue;

            control.SetColor(color);
        }

        public NeqHSLColorPicker()
        {
            InitializeComponent();

            _hsl = ColorConverter.RGBToHSL(SelectedColor);

            sliderH.ValueChanged += OnSlidersChanged;
            sliderS.ValueChanged += OnSlidersChanged;
            sliderL.ValueChanged += OnSlidersChanged;
            sliderA.ValueChanged += OnSlidersChanged;

            textH.TextChanged += OnTextChanged;
            textS.TextChanged += OnTextChanged;
            textL.TextChanged += OnTextChanged;
            textA.TextChanged += OnTextChanged;

            textHEXARGB.TextChanged += (s, e) => HandleColorChange(HandleFrom.HEXARGB);
            textARGB.TextChanged += (s, e) => HandleColorChange(HandleFrom.ARGB);
            textHSLA.TextChanged += (s, e) => HandleColorChange(HandleFrom.HSLA);

            for (int i = 2; i < 15; i++)
                comboRound.Items.Add(i);

            comboRound.SelectedIndex = 1;

            HandleRound();

            comboRound.SelectionChanged += (s, e) => HandleRound();

            HandleColorChange(HandleFrom.None);
        }

        private void HandleRound()
        {
            _round = (int)(comboRound.SelectedItem ?? 3);
            HandleColorChange(HandleFrom.None);
        }

        private void OnTextChanged(object sender, TextChangedEventArgs e)
        {
            HandleColorChange(HandleFrom.HSLATextBoxes);
        }

        private void OnSlidersChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            HandleColorChange(HandleFrom.Sliders);
        }

        private void HandleColorChange(HandleFrom from)
        {
            if (_frozen)
                return;

            _frozen = true;

            if (from == HandleFrom.Sliders)
            {
                _hsl.H = sliderH.Value;
                _hsl.S = sliderS.Value / 100;
                _hsl.L = sliderL.Value / 100;
                _hsl.A = sliderA.Value / 100;

            }
            else if (from == HandleFrom.HSLATextBoxes)
            {
                _hsl.H = textH.Value;
                _hsl.S = textS.Value;
                _hsl.L = textL.Value;
                _hsl.A = textA.Value;
            }
            else if (from == HandleFrom.ARGB)
            {
                if (string.IsNullOrWhiteSpace(textARGB.Text))
                {
                    _frozen = false;
                    return;
                }

                var data = textARGB.Text.Split(new char[] { ',' }, System.StringSplitOptions.RemoveEmptyEntries).Select(x => x.Trim()).ToList();

                if (data.Count == 3)
                {
                    if (!byte.TryParse(data[0], out byte r) 
                        || !byte.TryParse(data[1], out byte g)
                        || !byte.TryParse(data[2], out byte b))
                    {
                        _frozen = false;
                        return;
                    }

                    _hsl = ColorConverter.RGBToHSL(255, r, g, b);
                }
                else if (data.Count == 4)
                {
                    if (!byte.TryParse(data[0], out byte a)
                       || !byte.TryParse(data[1], out byte r)
                       || !byte.TryParse(data[2], out byte g)
                       || !byte.TryParse(data[3], out byte b))
                    {
                        _frozen = false;
                        return;
                    }

                    _hsl = ColorConverter.RGBToHSL(a, r, g, b);
                }
            }
            else if (from == HandleFrom.HSLA)
            {
                if (string.IsNullOrWhiteSpace(textHSLA.Text))
                {
                    _frozen = false;
                    return;
                }

                var data = textHSLA.Text.Split(new char[] { ',' }, System.StringSplitOptions.RemoveEmptyEntries).Select(x => x.Trim()).ToList();

                if (data.Count > 3)
                {
                    double a = 255;

                    if (!double.TryParse(data[0], NumberStyles.Any, CultureInfo.InvariantCulture, out double h)
                        || !double.TryParse(data[1], NumberStyles.Any, CultureInfo.InvariantCulture, out double s)
                        || !double.TryParse(data[2], NumberStyles.Any, CultureInfo.InvariantCulture, out double l)
                        || h < 0 || h > 360 || s < 0 || s > 1 || l < 0 || l > 1 || (data.Count == 4 && !double.TryParse(data[3], NumberStyles.Any, CultureInfo.InvariantCulture, out a)))
                    {
                        _frozen = false;
                        return;
                    }

                    _hsl = new HSLColor() { A = a, H = h, S = s, L = l };
                }
            }
            else if (from == HandleFrom.HEXARGB)
            {
                string text = textHEXARGB.Text.Replace("#", string.Empty);

                if (string.IsNullOrWhiteSpace(text) || (text.Length != 3 && text.Length != 4 && text.Length != 6 && text.Length != 8))
                {
                    _frozen = false;
                    return;
                }

                if (text.Length == 3)
                {
                    if (!byte.TryParse($"{text[0]}{text[0]}", NumberStyles.AllowHexSpecifier, CultureInfo.InvariantCulture, out byte r)
                            || !byte.TryParse($"{text[1]}{text[1]}", NumberStyles.AllowHexSpecifier, CultureInfo.InvariantCulture, out byte g)
                            || !byte.TryParse($"{text[2]}{text[2]}", NumberStyles.AllowHexSpecifier, CultureInfo.InvariantCulture, out byte b))
                    {
                        _frozen = false;
                        return;
                    }

                    _hsl = ColorConverter.RGBToHSL(255, r, g, b);
                }
                else if (text.Length == 4)
                {
                    if (!byte.TryParse($"{text[0]}{text[0]}", NumberStyles.AllowHexSpecifier, CultureInfo.InvariantCulture, out byte a)
                          || !byte.TryParse($"{text[1]}{text[1]}", NumberStyles.AllowHexSpecifier, CultureInfo.InvariantCulture, out byte r)
                          || !byte.TryParse($"{text[2]}{text[2]}", NumberStyles.AllowHexSpecifier, CultureInfo.InvariantCulture, out byte g)
                          || !byte.TryParse($"{text[3]}{text[3]}", NumberStyles.AllowHexSpecifier, CultureInfo.InvariantCulture, out byte b))
                    {
                        _frozen = false;
                        return;
                    }

                    _hsl = ColorConverter.RGBToHSL(a, r, g, b);
                }
                else
                {
                    List<string> values = new List<string>();

                    for (int i = 0; i < text.Length - 1; i += 2)
                        values.Add(text.Substring(i, 2));

                    if (values.Count == 3)
                    {
                        if (!byte.TryParse(values[0], NumberStyles.AllowHexSpecifier, CultureInfo.InvariantCulture, out byte r)
                            || !byte.TryParse(values[1], NumberStyles.AllowHexSpecifier, CultureInfo.InvariantCulture, out byte g)
                            || !byte.TryParse(values[2], NumberStyles.AllowHexSpecifier, CultureInfo.InvariantCulture, out byte b))
                        {
                            _frozen = false;
                            return;
                        }

                        _hsl = ColorConverter.RGBToHSL(255, r, g, b);
                    }
                    else if (values.Count == 4)
                    {
                        if (!byte.TryParse(values[0], NumberStyles.AllowHexSpecifier, CultureInfo.InvariantCulture, out byte a)
                           || !byte.TryParse(values[1], NumberStyles.AllowHexSpecifier, CultureInfo.InvariantCulture, out byte r)
                           || !byte.TryParse(values[2], NumberStyles.AllowHexSpecifier, CultureInfo.InvariantCulture, out byte g)
                           || !byte.TryParse(values[3], NumberStyles.AllowHexSpecifier, CultureInfo.InvariantCulture, out byte b))
                        {
                            _frozen = false;
                            return;
                        }

                        _hsl = ColorConverter.RGBToHSL(a, r, g, b);
                    }
                }

                
            }

            if (_round != 14)
            {
                _hsl.A = Math.Round(_hsl.A, _round);
                _hsl.H = Math.Round(_hsl.H, _round);
                _hsl.S = Math.Round(_hsl.S, _round);
                _hsl.L = Math.Round(_hsl.L, _round);
            }

            var color = ColorConverter.HSLToRGB(_hsl);

            if (color != SelectedColor)
                SetCurrentValue(SelectedColorProperty, color);

            if (from != HandleFrom.Sliders)
            {
                sliderH.Value = _hsl.H;
                sliderS.Value = _hsl.S * 100;
                sliderL.Value = _hsl.L * 100;
                sliderA.Value = _hsl.A * 100;
            }

            if (from != HandleFrom.HSLATextBoxes)
            {
                textH.Value = _hsl.H;
                textS.Value = _hsl.S;
                textL.Value = _hsl.L;
                textA.Value = _hsl.A;
            }

            if (from != HandleFrom.ARGB)
                textARGB.Text = $"{color.A}, {color.R}, {color.G}, {color.B}";

            if (from != HandleFrom.HSLA)
                textHSLA.Text = $"{_hsl.H.ToString(CultureInfo.InvariantCulture)}, {_hsl.S.ToString(CultureInfo.InvariantCulture)}, {_hsl.L.ToString(CultureInfo.InvariantCulture)}, {_hsl.A.ToString(CultureInfo.InvariantCulture)}";

            if (from != HandleFrom.HEXARGB)
                textHEXARGB.Text = $"#{(color.A == 255 ? string.Empty : color.A.ToString("X2"))}{color.R:X2}{color.G:X2}{color.B:X2}";

            var gradient = new LinearGradientBrush();
            var saturation = new LinearGradientBrush();
            var lightness = new LinearGradientBrush();
            var transparent = new LinearGradientBrush();

            for (int i = 0; i < 37; i++)
            {
                var rgb = ColorConverter.HSLToRGB(i * 10, _hsl.S, _hsl.L, _hsl.A);

                var gradientStop = new GradientStop(rgb, i == 36 ? 1 : i * 0.027);

                gradient.GradientStops.Add(gradientStop);
            }


            for (int i = 0; i < 11; i++)
            {
                var rgb = ColorConverter.HSLToRGB(_hsl.H, (double)i / 10, _hsl.L, _hsl.A);

                var gradientStop = new GradientStop(rgb, i * 0.1);

                saturation.GradientStops.Add(gradientStop);
            }

            for (int i = 0; i < 11; i++)
            {
                var rgb = ColorConverter.HSLToRGB(_hsl.H, _hsl.S, (double)i / 10, _hsl.A);

                var gradientStop = new GradientStop(rgb, i * 0.1);

                lightness.GradientStops.Add(gradientStop);
            }

            for (int i = 0; i < 11; i++)
            {
                var rgb = ColorConverter.HSLToRGB(_hsl.H, _hsl.S, _hsl.L, (double)i / 10);

                var gradientStop = new GradientStop(rgb, i * 0.1);

                transparent.GradientStops.Add(gradientStop);
            }

            sliderH.Background = gradient;
            sliderS.Background = saturation;
            sliderL.Background = lightness;
            sliderA.Background = transparent;

            borderSelectedColor.Background = new SolidColorBrush(color);
            _frozen = false;
        }

        public void SetColor(Color color)
        {
            if (_frozen)
                return;

            _hsl = ColorConverter.RGBToHSL(color);
            HandleColorChange(HandleFrom.None);
        }
    }
}
