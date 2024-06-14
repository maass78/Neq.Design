using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;

namespace Neq.Design.WPF.Controls
{
    public class NeqNumericTextBox : NeqTextBox
    {
        private string _previousText;

        public int Minimum
        {
            get { return (int)GetValue(MinimumProperty); }
            set { SetValue(MinimumProperty, value); }
        }

        public static readonly DependencyProperty MinimumProperty =
          DependencyProperty.Register("Minimum", typeof(int), typeof(NeqNumericTextBox), new PropertyMetadata());

        public int Maximum
        {
            get { return (int)GetValue(MaximumProperty); }
            set { SetValue(MaximumProperty, value); }
        }

        public static readonly DependencyProperty MaximumProperty =
          DependencyProperty.Register("Maximum", typeof(int), typeof(NeqNumericTextBox), new PropertyMetadata());


        public int Value
        {
            get { return (int)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        public static readonly DependencyProperty ValueProperty =
         DependencyProperty.Register("Value", typeof(int), typeof(NeqNumericTextBox), new PropertyMetadata(OnValueChanged));

        private static void OnValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var value = ((int)e.NewValue).ToString();

            var textBox = (d as NeqNumericTextBox);

            if (textBox.Text != value)
                textBox.Text = value;
        }

        //public int Value 
        //{ 
        //    get => 
        //    set => Text = value.ToString(CultureInfo.InvariantCulture); 
        //}

        public NeqNumericTextBox()
        {
            _previousText = Text;

            PreviewTextInput += OnPreviewTextInput;

            LostFocus += OnLostFocus;

            //DataObject.AddPastingHandler(this, OnPaste);

            TextChanged += (s, e) =>
            {
                //Value

                //if (!int.TryParse(Text, out int result) && Text != string.Empty)
                //{
                //    Text = _previousText;
                //    SelectionStart = Text.Length;
                //}
                //else
                //{
                //    if (result <= Maximum && result >= Minimum)
                //    {
                //        _previousText = Text;
                //    }
                //    else if (Text != string.Empty)
                //    {
                //        Text = _previousText;
                //        SelectionStart = Text.Length;
                //    }
                //}



                var success = int.TryParse(Text, out int value) && value >= Minimum && value <= Maximum;

                IsDestructive = !success;

                if (success)
                    SetValue(ValueProperty, value);

                //Value = 
            };
        }

        private void OnLostFocus(object sender, RoutedEventArgs e)
        {
            Text = Value.ToString();
        }

        //private void OnPaste(object sender, DataObjectPastingEventArgs e)
        //{
        //    throw new NotImplementedException();
        //}

        private void OnPreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            //if (int.)
            //if (e.Te)

            string fullText = Text.Insert(SelectionStart, e.Text);

            if (fullText == string.Empty)
                return;

            if (!int.TryParse(fullText, out int value) /*|| value < Minimum || value > Maximum*/)
                e.Handled = true;
        }
    }
}
