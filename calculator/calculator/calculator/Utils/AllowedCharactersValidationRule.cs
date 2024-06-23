using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace calculator.Utils
{
    public class AllowedCharactersValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string text = value as string;
            if (Regex.IsMatch(text, @"^[a-zA-Z0-9\(\)\+\-\*\/\.\s]*$"))
                return ValidationResult.ValidResult;
            else
                return new ValidationResult(false, "Allowed english letters, numbers, (), operations: .+-/ *");
        }
    }
}
