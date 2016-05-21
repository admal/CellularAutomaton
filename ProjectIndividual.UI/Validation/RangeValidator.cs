using System;
using System.Globalization;
using System.Windows.Controls;

namespace ProjectIndividual.UI.Validation
{
    public class RangeValidator : ValidationRule
    {
        private int min;
        private int max;

        public RangeValidator()
        {
        }

        public int Min
        {
            get { return min; }
            set { min = value; }
        }

        public int Max
        {
            get { return max; }
            set { max = value; }
        }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            int input = 0;

            try
            {
                if (((string)value).Length > 0)
                    input = Int32.Parse((String)value);
            }
            catch (Exception e)
            {
                return new ValidationResult(false, "Illegal characters or " + e.Message);
            }

            if ((input < Min) || (input > Max))
            {
                return new ValidationResult(false,
                  "Please enter number of row/column in range: " + Min + " - " + Max + ".");
            }
            else
            {
                return new ValidationResult(true, null);
            }
        }
    }
}