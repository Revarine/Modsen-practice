
﻿using calculator.Views;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Data;
using System.Globalization;
using System.Text.RegularExpressions;
using calculator.ParserF;
using static MaterialDesignThemes.Wpf.Theme;
using calculator.DataVault;

namespace calculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    

    public partial class MainWindow : Window
    {
        private readonly Parser parser = new Parser();
        private readonly Computations computations = new Computations();
        public ObservableCollection<string> historyCollection = new ObservableCollection<string>();
        
        public MainWindow()
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");
            InitializeComponent();
            variablesView.ItemsSource = DataVault.DataVault.GetVariables();
            functionsView.ItemsSource = DataVault.DataVault.GetFunctions();
            historyView.ItemsSource = historyCollection;
        }

        private void Calculate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var input = inputField.Text;
                if (!historyCollection.Contains(input)) historyCollection.Add(input);
                var expression = parser.Parse(input);
                //Console.WriteLine($"Parsed Expression Tree: {expression}");
                var result = computations.Calculate(expression);

                inputField.Text = $"{result}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        private void showAddFunctionMenu(object sender, RoutedEventArgs e)
        {
            new AddFunction().ShowDialog();
        }

        private void showAddVariableMenu(object sender, RoutedEventArgs e)
        {
            new AddVariable().ShowDialog();
        }

        private void numberButton_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Controls.Button clickedButton = sender as System.Windows.Controls.Button;
            if (clickedButton != null)
            {
                inputField.Text += clickedButton.Content.ToString();
                inputField.Focus();
                inputField.SelectionStart = inputField.Text.Length;
                inputField.SelectionLength = 0;
            }
        }

        private void clearFieldButton_Click(object sender, RoutedEventArgs e)
        {
            inputField.Text = "";
        }

        private void operationButton_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Controls.Button clickedButton = sender as System.Windows.Controls.Button;
            if (clickedButton != null)
            {
                string operation = clickedButton.Content.ToString();
                string inputText = inputField.Text;

                if (inputText.Length > 0 && "+-*/.".Contains(inputText[inputText.Length - 1]))
                {
                    inputField.Text = inputText.Substring(0, inputText.Length - 1) + operation;
                }
                else if (operation == "-" && (inputText.Length == 0))
                {
                    inputField.Text += operation;
                }
                else if (operation == "." && (inputText.Length == 0))
                {
                    inputField.Text = inputField.Text + "0" + operation;
                }
                else if (inputText.Length == 0 || inputText[inputText.Length - 1] == ' ')
                {
                    return;
                }
                else
                {
                    inputField.Text += operation;
                }

                inputField.Focus();
                inputField.SelectionStart = inputField.Text.Length;
                inputField.SelectionLength = 0;
            }
        }
    }
}