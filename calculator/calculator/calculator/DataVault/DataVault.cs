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
        public string name { get; set; }
        public Func<double[], double> function { get; set; }

        public Function(string name, Func<double[], double> function)
        {
            this.name = name;
            this.function = function;
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

        public static void addFunction(Function function)
        {
            functions.Add(function);
            MessageBox.Show("Успех!!!");
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