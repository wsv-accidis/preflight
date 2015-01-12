using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;

namespace Accidis.Apps.Preflight.Views.Settings
{
	public partial class LanguageControl : UserControl
	{
		public LanguageControl()
		{
			InitializeComponent();
			EnglishRadio.IsChecked = !App.Settings.UseSwedishFeeds;
			SwedishRadio.IsChecked = App.Settings.UseSwedishFeeds;
		}

		void OnCheckedChanged( object sender, RoutedEventArgs e )
		{
			Debug.WriteLine( "LanguageControl: Persisted settings." );
			App.Settings.UseSwedishFeeds = SwedishRadio.IsChecked.GetValueOrDefault( false );
            App.Settings.TriggerRefresh();
		}
	}
}
