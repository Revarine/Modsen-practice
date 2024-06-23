using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using System.Windows;
using calculator.DataVault;

namespace calculator.Views
{
    public partial class AddFunction : Window
    {
        private ObservableCollection<Parameter> parameters;

        public AddFunction()
        {
            InitializeComponent();
            parameters = new ObservableCollection<Parameter>();
            ParametersListBox.ItemsSource = parameters;
        }

        private void AddParameterClick(object sender, RoutedEventArgs e)
        {
            string parameterName = ParameterTextBox.Text.Trim();

            if (string.IsNullOrWhiteSpace(parameterName))
            {
                MessageBox.Show("Function parameter can't be empty.");
                return;
            }

            if (!Regex.IsMatch(parameterName, @"^[a-zA-Z]+$"))
            {
                MessageBox.Show("Parameter name can only contain english letters.");
                return;
            }

            parameters.Add(new Parameter { Name = parameterName });
            ParameterTextBox.Clear();
        }

        private void CreateNewFunctionClick(object sender, RoutedEventArgs e)
        {
            var functionName = FunctionNameTextBox.Text;
            var functionExpression = FunctionExpressionTextBox.Text;
            var isValid = Regex.IsMatch(functionName, @"^[a-zA-Z][a-zA-Z0-9]*$");

            if (!isValid)
            {
                MessageBox.Show("Function name can only start with english letter and contain letters or numbers in it.");
                return;
            }

            if (string.IsNullOrWhiteSpace(functionName) || string.IsNullOrWhiteSpace(functionExpression))
            {
                MessageBox.Show("Function name or function expression can't be empty.");
                return;
            }

            DataVault.DataVault.AddFunction(functionName, functionExpression, parameters);
            this.Close();
        }
    }
}