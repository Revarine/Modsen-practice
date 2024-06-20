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
        public MainWindow()
        {
            InitializeComponent();
            DataVault.addVariable("xd", 1.23);
            variablesView.ItemsSource = DataVault.getVariables();
        }

        private void showAddFunctionMenu(object sender, RoutedEventArgs e)
        {
            new AddFunction().ShowDialog();
        }
        private void showAddVariableMenu(object sender, RoutedEventArgs e)
        {
            new AddVariable().ShowDialog();
        }
        
    }
}