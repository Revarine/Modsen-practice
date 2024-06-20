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
        public string Value { get; set; }
    }

    public static class DataVault
    {
        private static Dictionary<string, double> variables;
        private static Dictionary<string, string> functions;
        private static ObservableCollection<Variable> variablesList;

        static DataVault()
        {
            variables = new Dictionary<string, double>();
            functions = new Dictionary<string, string>();
            variablesList = new ObservableCollection<Variable>();
        }

        public static void addVariable(string variableName, double variableValue)
        {
            variables.Add(variableName, variableValue);
            variablesList.Add(new Variable { Name = variableName, Value = variableValue.ToString() });
        }

        public static void AddFunction(string functionName, string functionExpression)
        {
            functions.Add(functionName, functionExpression);
        }

        public static ObservableCollection<Variable> getVariablesList()
        {
            return variablesList;
        }

    }
    
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataVault.addVariable("xd", 1.23);
            variablesView.ItemsSource = DataVault.getVariablesList();
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