using System.Globalization;
using System.Windows;

namespace Neq.Design.WPF.Controls
{
    public class NeqDecimalNumericTextBox : NeqTextBox
    {
        private string previousText;

        public decimal Minimum
        {
            get { return (decimal)GetValue(MinimumProperty); }
            set { SetValue(MinimumProperty, value); }
        }

        public static readonly DependencyProperty MinimumProperty =
          DependencyProperty.Register("Minimum", typeof(decimal), typeof(NeqDecimalNumericTextBox));

        public decimal Maximum
        {
            get { return (decimal)GetValue(MaximumProperty); }
            set { SetValue(MaximumProperty, value); }
        }

        public static readonly DependencyProperty MaximumProperty =
          DependencyProperty.Register("Maximum", typeof(decimal), typeof(NeqDecimalNumericTextBox));

        public decimal Value 
        { 
            get => decimal.TryParse(Text.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out decimal value) ? value : Minimum;
            set => Text = value.ToString(CultureInfo.InvariantCulture); 
        }

        public NeqDecimalNumericTextBox() : base()
        {
            previousText = Text;

            TextChanged += (s, e) =>
            {
                if (!decimal.TryParse(Text.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out decimal result) && Text != string.Empty)
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
