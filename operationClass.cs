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
        private static bool _firstInput = true;
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
                _lastClicked.BorderBrush = Brushes.Black;
                _lastClicked.BorderThickness = new Thickness(0.5);
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

                if (_firstInput)
                {
                    _targetDisplay.Text = buttonText;
                    _firstInput = false;
                } else
                {
                    _targetDisplay.Text += buttonText;
                }
            }

        }
        public static void putDecimal(object sender)
        {
            checkNullTarget();
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
            _argA = null;
            _argB = null;
            _firstInput = true;
            checkButtonStyle(sender, false);
        }
        public static void pressOperator(object sender)
        {
            checkNullTarget();
            checkButtonStyle(sender, true);
            _firstInput = true;
            _argA = double.Parse(_targetDisplay.Text);

            if (sender is Button button)
            {
                switch (button.Tag)
                {
                    case Operators.Addition:
                        _currentOp = Operators.Addition;
                        break;
                    case Operators.Subtraction:
                        _currentOp = Operators.Subtraction;
                        break;
                    case Operators.Multiplication:
                        _currentOp = Operators.Multiplication;
                        break;
                    case Operators.Division:
                        _currentOp = Operators.Division;
                        break;
                    default:
                        _currentOp = Operators.None;
                        break;
                }
            }

        }
        public static void calcOperation()
        {
            checkNullTarget();
            _firstInput = true;
            _argB = double.Parse(_targetDisplay.Text);
            double result;

            if (_lastClicked != null)
            {
                _lastClicked.BorderBrush = Brushes.Black;
                _lastClicked.BorderThickness = new Thickness(0.5);
                _lastClicked = null;
            }

            switch (_currentOp)
            {
                case Operators.Addition:
                    result = (double)(_argA + _argB);
                    _targetDisplay.Text = result.ToString();
                    _argA = result;
                    _argB = null;
                    break;

                case Operators.Subtraction:
                    result = (double)(_argA - _argB);
                    _targetDisplay.Text = result.ToString();
                    _argA = result;
                    _argB = null;
                    break;

                case Operators.Multiplication:
                    result = (double)(_argA * _argB);
                    _targetDisplay.Text = result.ToString();
                    _argA = result;
                    _argB = null;
                    break;

                case Operators.Division:
                    result = (double)(_argA / _argB);
                    _targetDisplay.Text = result.ToString();
                    _argA = result;
                    _argB = null;
                    break;
                default:
                    MessageBox.Show("error calculating response","ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                    break;
            }


        }

    }
}
