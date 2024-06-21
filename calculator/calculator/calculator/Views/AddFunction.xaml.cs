using System.Collections.ObjectModel;
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
            if (!string.IsNullOrWhiteSpace(ParameterTextBox.Text))
            {
                parameters.Add(new Parameter { Name = ParameterTextBox.Text });
                ParameterTextBox.Clear();
            }
            else
            {
                MessageBox.Show("Parameter name cannot be empty.");
            }
        }

        private void CreateNewFunctionClick(object sender, RoutedEventArgs e)
        {
            string functionName = FunctionNameTextBox.Text;
            string functionExpression = FunctionExpressionTextBox.Text;

            if (string.IsNullOrWhiteSpace(functionName) || string.IsNullOrWhiteSpace(functionExpression))
            {
                MessageBox.Show("Function name and expression cannot be empty.");
                return;
            }

            DataVault.DataVault.AddFunction(functionName, functionExpression, parameters);
            this.Close();
        }
    }
}