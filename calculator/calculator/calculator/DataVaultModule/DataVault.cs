using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using calculator.Views;

namespace calculator.DataVault;

public static class DataVault
{
    private static ObservableCollection<Variable> variables;
    private static ObservableCollection<Function> functions;

    static DataVault()
    {
        variables = new ObservableCollection<Variable>();
        functions = new ObservableCollection<Function>();
    }

    public static void AddVariable(string variableName, double variableValue)
    {
        var existingVariable = variables.FirstOrDefault(v => v.Name == variableName);
        if (existingVariable != null)
        {
            new ResultWindow("Variable value is changed successfully!").ShowDialog();
            existingVariable.Value = variableValue;
        }
        else
        {
            variables.Add(new Variable { Name = variableName, Value = variableValue });
        }
    }

    public static void AddFunction(string functionName, string functionExpression, ObservableCollection<Parameter> parameters)
    {
        var existingFunction = functions.FirstOrDefault(f => f.Name == functionName);
        if (existingFunction != null)
        {
            new ResultWindow("Function values is changed successfully!").ShowDialog();
            existingFunction.Expression = functionExpression;
            existingFunction.Parameters = parameters;
        }
        else
        {
            functions.Add(new Function { Name = functionName, Expression = functionExpression, Parameters = parameters });
        }
    }

    public static ObservableCollection<Variable> GetVariables()
    {
        return variables;
    }

    public static ObservableCollection<Function> GetFunctions()
    {
        return functions;
    }
}