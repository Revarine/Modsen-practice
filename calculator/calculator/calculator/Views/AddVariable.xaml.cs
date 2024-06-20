using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace calculator.Views
{
    /// <summary>
    /// Логика взаимодействия для AddVariable.xaml
    /// </summary>
    public partial class AddVariable : Window
    {
        public AddVariable()
        {
            InitializeComponent();
        }

        private void createNewVariable(object sender, RoutedEventArgs e)
        {
            DataVault.addVariable(variableName.Text, Convert.ToDouble(variableValue.Text));
            
            MessageBox.Show($"{variableName.Text} + {variableValue.Text}");
            
        }
    }
}
