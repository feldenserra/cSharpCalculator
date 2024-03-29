﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.NetworkInformation;
using System.Security.AccessControl;
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
        private static double? _argSameOp;
        private static bool _firstInput = true;
        private static bool _secondInput = false;
        private static bool _sameOp = false;
        private static string _currentOp = Operators.None;
        private static string _lastOp = Operators.None;

        public static void checkNullTarget()
        {
            if (_targetDisplay == null)
            {
                _targetDisplay = Application.Current.MainWindow.FindName("numDisplay") as TextBox;
            }
        }
        public static void checkButtonStyle(object? sender, bool action)
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
            if (_argA != null && _currentOp != Operators.None)
            {
                _secondInput = true;
            }
        }
        public static void putDecimal(object sender)
        {
            checkNullTarget();
            bool decimalPresent = _targetDisplay.Text.Any(s => s == '.');

            if (!decimalPresent)
            {
                _targetDisplay.Text += ".";
                _firstInput = false;
            }
        }
        public static void clearText(object? sender)
        {
            checkNullTarget();
            if (sender != null)
            {
            _targetDisplay.Text = "0";
            }
            _currentOp = Operators.None;
            _argA = null;
            _argB = null;
            _firstInput = true;
            _secondInput = false;
            _sameOp = false;
            _argSameOp = null;
            checkButtonStyle(sender, false);
        }
        public static void pressOperator(object sender)
        {
            checkNullTarget();
            checkButtonStyle(sender, true);
            _firstInput = true;
            _sameOp = false;
            _argSameOp = null;
            double currentInput = double.Parse(_targetDisplay.Text);

            if (_argA != null && _currentOp != Operators.None && _secondInput)
            {
                _argA = inlineOperation((double)_argA, currentInput, _currentOp);
                _targetDisplay.Text = _argA.ToString();
            } else
            {
                _argA = currentInput;
            }

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
            double result;

            if (_sameOp)
            {
                if (_targetDisplay.Text != _argA.ToString())
                {
                    _argA = double.Parse(_targetDisplay.Text);
                }
                switch (_lastOp)
                {
                    case Operators.Addition:
                        result = (double)(_argA + _argSameOp);                     
                        break;

                    case Operators.Subtraction:
                        result = (double)(_argA - _argSameOp);
                        break;

                    case Operators.Multiplication:
                        result = (double)(_argA * _argSameOp);
                        break;

                    case Operators.Division:
                        if (_argB == 0)
                        {
                            clearText(null);
                            _targetDisplay.Text = "Can't Divide By Zero";
                            return;
                        }
                        result = (double)(_argA / _argSameOp);
                        break;
                    default:
                        MessageBox.Show("error calculating response", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                        result = double.NaN;
                        break;
                }
                _targetDisplay.Text = result.ToString();
                _argA = result;
            } 
            else
            {
                _argB = double.Parse(_targetDisplay.Text);
                switch (_currentOp)
                {
                    case Operators.Addition:
                        result = (double)(_argA + _argB);
                        break;

                    case Operators.Subtraction:
                        result = (double)(_argA - _argB);
                        break;

                    case Operators.Multiplication:
                        result = (double)(_argA * _argB);
                        break;

                    case Operators.Division:
                        if (_argB == 0)
                        {
                            clearText(null);
                            _targetDisplay.Text = "Can't Divide By Zero";
                            result = double.NaN;
                            break;
                        }
                        result = (double)(_argA / _argB);
                        break;
                    default:
                        MessageBox.Show("error calculating response", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                        result = double.NaN;
                        break;
                }
                _targetDisplay.Text = result.ToString();
                _argA = result;
                _argSameOp = _argB;
                _argB = null;
                _sameOp = true;
                _lastOp = _currentOp;
                _currentOp = Operators.None;
            }
            checkButtonStyle(null, false);
        }
        public static double inlineOperation(double a, double b, string op)
        {
            _secondInput = false;
            switch (_currentOp)
            {
                case Operators.Addition:
                    return (double)(a + b);

                case Operators.Subtraction:
                    return (double)(a - b);

                case Operators.Multiplication:
                    return (double)(a * b);

                case Operators.Division:
                    if (b == 0)
                    {
                        clearText(null);
                        _targetDisplay.Text = "Can't Divide By Zero";
                        return double.NaN;
                    }
                    return (double)(a / b);
                default:
                    MessageBox.Show("error calculating response", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                    return double.NaN;

            }

        }
    }
}
