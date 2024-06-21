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
            if (!string.IsNullOrWhiteSpace(ParameterTextBox.Text))
            {
                parameters.Add(new Parameter { Name = ParameterTextBox.Text });
                ParameterTextBox.Clear();
            }
            else
            {
                MessageBox.Show("Параметр не может быть пустым.");
            }
        }

        private void CreateNewFunctionClick(object sender, RoutedEventArgs e)
        {
            string functionName = FunctionNameTextBox.Text;
            string functionExpression = FunctionExpressionTextBox.Text;
            bool isValid = Regex.IsMatch(functionName, @"^[a-zA-Z][a-zA-Z0-9]*$");
            if (!isValid)
            {
                MessageBox.Show("В названии функции могуть быть только буквы или цифры, начинаться оно должно с буквы.");
                return;
            }
            if (string.IsNullOrWhiteSpace(functionName) || string.IsNullOrWhiteSpace(functionExpression))
            {
                MessageBox.Show("Название функции или выражение функции не может быть пустым.");
                return;
            }

            DataVault.DataVault.AddFunction(functionName, functionExpression, parameters);
            this.Close();
        }
    }
}