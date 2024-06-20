using calculator.Views;
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

namespace calculator
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

        private void ShowAddFunction(object sender, RoutedEventArgs e)
        {
            new AddFunction().ShowDialog();
        }
        private void ShowAddVariable(object sender, RoutedEventArgs e)
        {
            new AddVariable().ShowDialog();
        }

        private void numberButton_Click(object sender, RoutedEventArgs e)
        {
            Button clickedButton = sender as Button;
            if (clickedButton != null)
            {
                inputField.Text += clickedButton.Content.ToString();
                inputField.Focus();
                inputField.SelectionStart = inputField.Text.Length;
                inputField.SelectionLength = 0;
            }
        }

        private void clearFieldButton_Click(object sender, RoutedEventArgs e)
        {
            inputField.Text = "";
        }

        private void operationButton_Click(object sender, RoutedEventArgs e)
        {
            Button clickedButton = sender as Button;
            if (clickedButton != null)
            {
                string operation = clickedButton.Content.ToString();
                string inputText = inputField.Text;

                if (inputText.Length > 0 && "+-*/.".Contains(inputText[inputText.Length - 1]))
                {
                    inputField.Text = inputText.Substring(0, inputText.Length - 1) + operation;
                }
                else if (operation == "-" && (inputText.Length == 0))
                {
                    inputField.Text += operation;
                }
                else if (operation == "." && (inputText.Length == 0))
                {
                    inputField.Text = inputField.Text+ "0" + operation;
                }
                else if (inputText.Length == 0 || inputText[inputText.Length - 1] == ' ')
                {
                    return;
                }
                else
                {
                    inputField.Text += operation;
                }

                inputField.Focus();
                inputField.SelectionStart = inputField.Text.Length;
                inputField.SelectionLength = 0;
            }
        }
    }
}