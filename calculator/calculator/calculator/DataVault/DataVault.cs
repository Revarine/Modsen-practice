using System.Collections.ObjectModel;
using System.Windows;

namespace calculator.DataVault
{
    public class Variable
    {
        public string Name { get; set; }
        public double Value { get; set; }
    }
    public class Parameter
    {
        public string Name { get; set; }
    }

    public class Function
    {
        public string Name { get; set; }
        public string Expression { get; set; }
        public ObservableCollection<Parameter> Parameters { get; set; }

        public Function()
        {
            Parameters = new ObservableCollection<Parameter>();
        }
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

        public static void AddVariable(string variableName, double variableValue)
        {
            var existingVariable = variables.FirstOrDefault(v => v.Name == variableName);
            if (existingVariable != null)
            {
                MessageBox.Show("Переменная будет изменена на новую");
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
                MessageBox.Show("Функция будет изменена на новую");
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
}
