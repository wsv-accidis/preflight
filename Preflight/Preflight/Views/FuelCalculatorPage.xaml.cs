using System;
using System.Globalization;
using System.Windows.Controls;
using Microsoft.Phone.Controls;

namespace Accidis.Apps.Preflight.Views
{
    public partial class FuelCalculatorPage : PhoneApplicationPage
    {
        const double _usGallonInLitres = 3.78541;

        public FuelCalculatorPage()
        {
            InitializeComponent();
            TargetTotalFuelField.TextChanged += OnTargetTotalFuelFieldChanged;
            CurrentFuelLeftField.TextChanged += OnInputFieldChanged;
            CurrentFuelRightField.TextChanged += OnInputFieldChanged;
            TargetFuelLeftField.TextChanged += OnInputFieldChanged;
            TargetFuelRightField.TextChanged += OnInputFieldChanged;
        }

        void OnTargetTotalFuelFieldChanged(object sender, TextChangedEventArgs e)
        {
            double totalFuel;
            if(!Double.TryParse(TargetTotalFuelField.Text, NumberStyles.AllowDecimalPoint, CultureInfo.CurrentCulture, out totalFuel))
                return;

            totalFuel /= 2.0;
            TargetFuelLeftField.Text = TargetFuelRightField.Text = totalFuel.ToString("N1", CultureInfo.CurrentCulture);
        }

        void OnInputFieldChanged(object sender, TextChangedEventArgs e)
        {
            Calculate(CurrentFuelLeftField, TargetFuelLeftField, FillFuelLeftLabel);
            Calculate(CurrentFuelRightField, TargetFuelRightField, FillFuelRightLabel);
        }

        static void Calculate(TextBox currentText, TextBox targetText, TextBlock fillLabel)
        {
            // Ugly hack for issue with decimal separators
            string currentString = currentText.Text, targetString = targetText.Text;
            string decimalSeparator = CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;
            if(decimalSeparator == ",")
            {
                currentString = currentString.Replace(".", decimalSeparator);
                targetString = targetString.Replace(".", decimalSeparator);
            }

            double current, target;
            try
            {
                current = Double.Parse(currentString, NumberStyles.AllowDecimalPoint, CultureInfo.CurrentCulture);
                target = Double.Parse(targetString, NumberStyles.AllowDecimalPoint, CultureInfo.CurrentCulture);
            }
            catch
            {
                fillLabel.Text = String.Empty;
                return;
            }

            // Do calculation and conversion
            double fill = target - current;
            fill *= _usGallonInLitres;

            fillLabel.Text = fill.ToString("N1", CultureInfo.CurrentCulture);
        }
    }
}