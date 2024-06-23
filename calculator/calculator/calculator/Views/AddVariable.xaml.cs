using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using calculator.DataVault;

namespace calculator.Views
{
    public partial class AddVariable : Window
    {
        public AddVariable()
        {
            InitializeComponent();
        }

        private void createNewVariableClick(object sender, RoutedEventArgs e)
        {
            var isValid = Regex.IsMatch(variableName.Text, @"^[a-zA-Z][a-zA-Z0-9]*$");
            if (!isValid)
            {
                new ResultWindow("Variable name can only start with english letter and contain letters or numbers in it.").ShowDialog();
                return;
            }

            isValid = Regex.IsMatch(variableValue.Text, @"^(\-)?\d+(\.\d+)?$");
            if (!isValid)
            {
                new ResultWindow("The variable must have an integer or decimal value (decimal separator: \".\")").ShowDialog();
                return;
            }
            
            DataVault.DataVault.AddVariable(variableName.Text, Convert.ToDouble(variableValue.Text));
        }
    }
}
