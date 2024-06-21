using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace calculator.DataVault
{
    public class Variable : INotifyPropertyChanged
    {
        private string name;
        private double value;
        
        public string Name
        {
            get => name;
            set
            {
                if (name != value)
                {
                    name = value;
                    OnPropertyChanged();
                }
            }
        }
        public double Value
        {
            get => value;
            set
            {
                if (value != this.value)
                {
                    this.value = value;
                    OnPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class Parameter : INotifyPropertyChanged
    {
        private string name;

        public string Name
        {
            get => name;
            set
            {
                if (name != value)
                {
                    name = value;
                    OnPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class Function : INotifyPropertyChanged
    {
        private string name;
        private string expression;
        private ObservableCollection<Parameter> parameters;

        public string Name
        {
            get => name;
            set
            {
                if (name != value)
                {
                    name = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Expression
        {
            get => expression;
            set
            {
                if (expression != value)
                {
                    expression = value;
                    OnPropertyChanged();
                }
            }
        }

        public ObservableCollection<Parameter> Parameters
        {
            get => parameters;
            set
            {
                if (parameters != value)
                {
                    parameters = value;
                    OnPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

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
