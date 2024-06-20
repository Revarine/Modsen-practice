using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using System.Windows;

namespace calculator.DataVault
{
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
                MessageBox.Show("Переменная будет изменена на новую");
                existingVariable.Value = variableValue;
            }
            else
            {
                variables.Add(new Variable { Name = variableName, Value = variableValue });
            }
        }

        public static void addFunction(string functionName, string functionExpression)
        {
            var existingFunction = functions.FirstOrDefault(f => f.Name == functionName);
            if (existingFunction != null)
            {
                MessageBox.Show("Функция будет изменена на новую");
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
}