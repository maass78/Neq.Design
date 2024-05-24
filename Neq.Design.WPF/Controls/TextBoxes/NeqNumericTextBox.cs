using System.Globalization;
using System.Windows;

namespace Neq.Design.WPF.Controls
{
    public class NeqNumericTextBox : NeqTextBox
    {
        private string previousText;

        public int Minimum
        {
            get { return (int)GetValue(MinimumProperty); }
            set { SetValue(MinimumProperty, value); }
        }

        public static readonly DependencyProperty MinimumProperty =
          DependencyProperty.Register("Minimum", typeof(int), typeof(NeqNumericTextBox));

        public int Maximum
        {
            get { return (int)GetValue(MaximumProperty); }
            set { SetValue(MaximumProperty, value); }
        }

        public static readonly DependencyProperty MaximumProperty =
          DependencyProperty.Register("Maximum", typeof(int), typeof(NeqNumericTextBox));

        public int Value 
        { 
            get => int.TryParse(Text, out int value) ? value : Minimum;
            set => Text = value.ToString(CultureInfo.InvariantCulture); 
        }

        public NeqNumericTextBox() : base()
        {
            previousText = Text;

            TextChanged += (s, e) =>
            {
                if (!int.TryParse(Text, out int result) && Text != string.Empty)
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
