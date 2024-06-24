using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using System.Windows;
using calculator.DataVault;
using calculator.ParserF;

namespace calculator.Views;

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
            new ResultWindow("Function parameter can't be empty.").ShowDialog();
            return;
        }

        if (!Regex.IsMatch(parameterName, @"^[a-zA-Z]+$"))
        {
            new ResultWindow("Parameter name can only contain english letters.").ShowDialog();
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
            new ResultWindow("Function name can only start with english letter and contain letters or numbers in it.").ShowDialog();
            return;
        }

        if (string.IsNullOrWhiteSpace(functionName) || string.IsNullOrWhiteSpace(functionExpression))
        {
            new ResultWindow("Function name or function expression can't be empty.").ShowDialog();
            return;
        }

        try
        {
            var expression = new Parser().Parse(functionExpression);
        }
        catch (Exception exception)
        {
            new ResultWindow("Bad expression").ShowDialog();
        }
            
        DataVault.DataVault.AddFunction(functionName, functionExpression, parameters);
        this.Close();
    }
}