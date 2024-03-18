using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Media;

namespace Calculator
{
    public static class operationClass
    {
        private static TextBox? _targetDisplay;
        private static Button? _lastClicked;
        private static double? _argA;
        private static double? _argB;
        private static string _currentOp = Operators.None;

        public static void checkNullTarget()
        {
            if (_targetDisplay == null)
            {
                _targetDisplay = Application.Current.MainWindow.FindName("numDisplay") as TextBox;
            }
        }
        public static void checkButtonStyle(object sender, bool action)
        {
            if (_lastClicked != null)
            {
                _lastClicked.BorderBrush = Brushes.Transparent;
                _lastClicked.BorderThickness = new Thickness(0);
                _lastClicked = null;
            }
            if (action)
            {
                if (sender is Button button)
                {
                    button.BorderBrush = Brushes.Red;
                    button.BorderThickness = new Thickness(2);
                    _lastClicked = button;
                }
            }
        }
        public static void updateText(object sender)
        {
            checkNullTarget();

            if(sender is Button button)
            {
                string buttonText = button.Content.ToString();
                if (_targetDisplay.Text == "0")
                {
                    _targetDisplay.Text = buttonText;
                } else
                {
                _targetDisplay.Text += buttonText;
                }
            }

        }
        public static void putDecimal(object sender)
        {
            bool decimalPresent = _targetDisplay.Text.Any(s => s == '.');

            if (!decimalPresent)
            {
                _targetDisplay.Text += ".";
            }
        }
        public static void clearText(object sender)
        {
            checkNullTarget();
            _targetDisplay.Text = "0";
            _currentOp = Operators.None;
            checkButtonStyle(sender, false);
        }
        public static void pressOperator(object sender)
        {
            checkNullTarget();
            checkButtonStyle(sender, true);
            
            _argA = double.Parse(_targetDisplay.Text);

        }
        public static double Add(double a, double b)
        {
            return a + b;
        }

    }
}
