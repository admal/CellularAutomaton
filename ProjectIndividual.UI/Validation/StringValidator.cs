using System.Globalization;
using System.Windows;
using System.Windows.Controls;

namespace ProjectIndividual.UI.Validation
{
    public class StringValidator : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string input = value as string;
            if (string.IsNullOrWhiteSpace(input))
            {
             //   MessageBox.Show("Test");
                return new ValidationResult(false, "String can not be empty!");
            }
            return new ValidationResult(true, null);
        }
    }
}