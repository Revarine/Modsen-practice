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
using System.Collections;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Data;
using calculator.ParserF;
using static MaterialDesignThemes.Wpf.Theme;

namespace calculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>


    public class Variable
    {
        public string Name { get; set; }
        public double Value { get; set; }
    }

    public class Function
    {
        public string Name { get; set; }
        public string Expression { get; set; }
    }

    public static class DataVault
    {
        private static ObservableCollection<Variable> variables;
        private static ObservableCollection<Function> functions;

        static DataVault()
        {
            variables = new ObservableCollection<Variable>();
            functions = new ObservableCollection<Function>();
        }

        public static void addVariable(string variableName, double variableValue)
        {
            var existingVariable = variables.FirstOrDefault(v => v.Name == variableName);
            if (existingVariable != null)
            {
                existingVariable.Value = variableValue;
            }
            else
            {
                variables.Add(new Variable { Name = variableName, Value = variableValue });
            }
        }

        public static void AddFunction(string functionName, string functionExpression)
        {
            var existingFunction = functions.FirstOrDefault(f => f.Name == functionName);
            if (existingFunction != null)
            {
                existingFunction.Expression = functionExpression;
            }
            else
            {
                functions.Add(new Function { Name = functionName, Expression = functionExpression });
            }
        }

        public static ObservableCollection<Variable> getVariables()
        {
            return variables;
        }

        public static ObservableCollection<Function> GetFunctions()
        {
            return functions;
        }
    }
    
    public partial class MainWindow : Window
    {
        private readonly Parser parser = new Parser();
        private readonly Computations computations = new Computations();

        public MainWindow()
        {
            InitializeComponent();
            variablesView.ItemsSource = DataVault.getVariables();
        }

        private void Calculate_Click( object sender, RoutedEventArgs e )
        {
            try
            {
                var input = inputField.Text;
                var expression = parser.Parse(input);
                //Console.WriteLine($"Parsed Expression Tree: {expression}");
                var result = computations.Calculate(expression);

                inputField.Text = $"{result}";

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }
        }

        private void showAddFunctionMenu(object sender, RoutedEventArgs e)
        {
            new AddFunction().ShowDialog();
        }
        private void showAddVariableMenu(object sender, RoutedEventArgs e)
        {
            new AddVariable().ShowDialog();
        }

        private void numberButton_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Controls.Button clickedButton = sender as System.Windows.Controls.Button;
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
            System.Windows.Controls.Button clickedButton = sender as System.Windows.Controls.Button;
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