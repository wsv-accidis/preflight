using System;
using System.Globalization;
using System.Windows.Controls;
using System.Windows.Input;
using Microsoft.Phone.Controls;

namespace Accidis.Apps.Preflight.Views
{
    public partial class WindCalculatorPage : PhoneApplicationPage
    {
        public WindCalculatorPage()
        {
            InitializeComponent();
            RunwayNumberField.TextChanged += OnInputFieldChanged;
            RunwayNumberField.KeyUp += OnInputFieldKeyUp;
            WindDirectionField.TextChanged += OnInputFieldChanged;
            WindDirectionField.KeyUp += OnInputFieldKeyUp;
            WindSpeedField.TextChanged += OnInputFieldChanged;
            WindSpeedField.KeyUp += OnInputFieldKeyUp;
        }

        void OnInputFieldChanged(object sender, TextChangedEventArgs e)
        {
            Recalculate();
        }

        void Recalculate()
        {
            int rwyNum, windDirection, windSpeed;
            try
            {
                rwyNum = Int32.Parse(RunwayNumberField.Text, NumberStyles.None, CultureInfo.InvariantCulture);
                windDirection = Int32.Parse(WindDirectionField.Text, NumberStyles.None, CultureInfo.InvariantCulture);
                windSpeed = Int32.Parse(WindSpeedField.Text, NumberStyles.None, CultureInfo.InvariantCulture);
            }
            catch
            {
                ClearValues();
                return;
            }

            // Sanity checks
            if(rwyNum < 0 || rwyNum > 36 || windDirection < 0 || windDirection > 360 || windSpeed < 0)
            {
                ClearValues();
                return;
            }

            if(rwyNum == 36)
                rwyNum = 0;
            if(windDirection == 360)
                windDirection = 0;
            int rwyHeading = rwyNum * 10;

            // Calculate wind angle (with wraparound)
            int windAngle = windDirection - rwyHeading;
            if(windAngle > 180)
                windAngle -= 360;
            else if(windAngle < -180)
                windAngle += 360;

            // Calculate wind components
            double windAngleInRadians = (windAngle * Math.PI) / 180.0;
            double crosswind = Math.Abs(windSpeed * Math.Sin(windAngleInRadians));
            double headWind = windSpeed * Math.Cos(windAngleInRadians);

            WindAngleLabel.Text = String.Concat((windAngle > 0 ? "+" : ""), windAngle);
            CrosswindLabel.Text = crosswind.ToString("N1");
            HeadwindLabel.Text = headWind.ToString("N1");
        }

        void ClearValues()
        {
            WindAngleLabel.Text = CrosswindLabel.Text = HeadwindLabel.Text = String.Empty;
        }

        static void OnInputFieldKeyUp(object sender, KeyEventArgs e)
        {
            // Suppress '.' key on keypad
            if(Key.Decimal == e.Key)
                e.Handled = true;
        }
    }
}