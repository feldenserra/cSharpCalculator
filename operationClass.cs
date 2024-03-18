using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Calculator
{
    public static class operationClass
    {
        private static TextBox? _targetDisplay;
        private static double? _argA;
        private static double? _argB;
        private static bool _inCalc = false;

        public static void checkNullTarget()
        {
            if (_targetDisplay == null)
            {
                _targetDisplay = Application.Current.MainWindow.FindName("numDisplay") as TextBox;
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
        public static void clearText()
        {
            checkNullTarget();
            _targetDisplay.Text = "0";
        }
        public static void pressAdd(object sender)
        {
            checkNullTarget();
            double _argA = double.Parse(_targetDisplay.Text);
        }
        public static double Add(double a, double b)
        {
            return a + b;
        }

    }
}
