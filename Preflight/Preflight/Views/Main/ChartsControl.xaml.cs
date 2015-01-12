using System;
using System.Windows;
using System.Windows.Controls;
using Accidis.Apps.Preflight.Services;
using Accidis.Apps.Preflight.Utils.Windows;

namespace Accidis.Apps.Preflight.Views.Main
{
	public partial class ChartsControl : UserControl
	{
		public ChartsControl()
		{
			InitializeComponent();
		}

		void OnNordicSwcClicked( object sender, RoutedEventArgs e )
		{
			OpenChart( Charts.NordicSwc );
		}

		void OnVfrChartClicked( object sender, RoutedEventArgs e )
		{
			OpenChart( Charts.VfrChart );
		}

		void OnVfrFc06Clicked( object sender, RoutedEventArgs e )
		{
			OpenChart( Charts.VfrForecast06 );
		}

		void OnVfrFc12Clicked( object sender, RoutedEventArgs e )
		{
			OpenChart( Charts.VfrForecast12 );
		}

		void OnLlfChartClicked( object sender, RoutedEventArgs e )
		{
			OpenChart( Charts.LlfChart );
		}

		static void OpenChart( string chart )
		{
			Application.Current.Navigate( "/Views/ChartPage.xaml?chart={0}", Uri.EscapeUriString( chart ) );
		}
	}
}