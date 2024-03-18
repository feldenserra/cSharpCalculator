using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Calculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }


        private void numDisplay_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
        private void num1_Click(object sender, RoutedEventArgs e)
        {
            operationClass.updateText(sender);

        }
        private void num2_Click(object sender, RoutedEventArgs e)
        {
            operationClass.updateText(sender);
        }
        private void num3_Click(object sender, RoutedEventArgs e)
        {
            operationClass.updateText(sender);

        }
        private void num4_Click(object sender, RoutedEventArgs e)
        {
            operationClass.updateText(sender);

        }
        private void num5_Click(object sender, RoutedEventArgs e)
        {
            operationClass.updateText(sender);

        }
        private void num6_Click(object sender, RoutedEventArgs e)
        {
            operationClass.updateText(sender);

        }
        private void num7_Click(object sender, RoutedEventArgs e)
        {
            operationClass.updateText(sender);

        }
        private void num8_Click(object sender, RoutedEventArgs e)
        {
            operationClass.updateText(sender);

        }
        private void num9_Click(object sender, RoutedEventArgs e)
        {
            operationClass.updateText(sender);

        }
        private void num0_Click(object sender, RoutedEventArgs e)
        {
            operationClass.updateText(sender);

        }
        private void numPeriod_Click(object sender, RoutedEventArgs e)
        {
            operationClass.putDecimal(sender);
        }
        private void numPlus_Click(object sender, RoutedEventArgs e)
        {
            operationClass.pressOperator(sender);
        }
        private void numMinus_Click(object sender, RoutedEventArgs e)
        {
            operationClass.pressOperator(sender);

        }
        private void numMultiply_Click(object sender, RoutedEventArgs e)
        {
            operationClass.pressOperator(sender);

        }
        private void numDivide_Click(object sender, RoutedEventArgs e)
        {
            operationClass.pressOperator(sender);

        }
        private void numEqual_Click(object sender, RoutedEventArgs e)
        {

        }
        private void numClear_Click(object sender, RoutedEventArgs e)
        {
            operationClass.clearText(sender);
        }

    }
}