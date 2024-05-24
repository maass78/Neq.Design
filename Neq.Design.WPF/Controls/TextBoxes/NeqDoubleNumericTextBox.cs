using System.Globalization;
using System.Windows;

namespace Neq.Design.WPF.Controls
{
    public class NeqDoubleNumericTextBox : NeqTextBox
    {
        private string previousText;

        public double Minimum
        {
            get { return (double)GetValue(MinimumProperty); }
            set { SetValue(MinimumProperty, value); }
        }

        public static readonly DependencyProperty MinimumProperty =
          DependencyProperty.Register("Minimum", typeof(double), typeof(NeqDoubleNumericTextBox));

        public double Maximum
        {
            get { return (double)GetValue(MaximumProperty); }
            set { SetValue(MaximumProperty, value); }
        }

        public static readonly DependencyProperty MaximumProperty =
          DependencyProperty.Register("Maximum", typeof(double), typeof(NeqDoubleNumericTextBox));

        public double Value 
        { 
            get => double.TryParse(Text.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double value) ? value : Minimum;
            set => Text = value.ToString(CultureInfo.InvariantCulture); 
        }

        public NeqDoubleNumericTextBox() : base()
        {
            previousText = Text;

            TextChanged += (s, e) =>
            {
                if (!double.TryParse(Text.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double result) && Text != string.Empty)
                {
                    Text = previousText;
                    SelectionStart = Text.Length;
                }
                else
                {
                    if (result <= Maximum && result >= Minimum)
                    {
                        previousText = Text;
                    }
                    else if (Text != string.Empty)
                    {
                        Text = previousText;
                        SelectionStart = Text.Length;
                    }
                }
            };
        }
    }
}
