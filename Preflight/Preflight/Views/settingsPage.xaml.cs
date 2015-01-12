using System.Windows;
using Accidis.Apps.Preflight.ViewModels;
using Accidis.Apps.Preflight.Views.Settings;
using Microsoft.Phone.Controls;

namespace Accidis.Apps.Preflight.Views
{
	public partial class SettingsPage : PhoneApplicationPage
	{
		bool _loaded;

		public SettingsPage()
		{
			InitializeComponent();
		}

		void OnLoaded( object sender, RoutedEventArgs e )
		{
			if( _loaded )
				return;

			InitializePivot();
			_loaded = true;
		}

		void InitializePivot()
		{
			var feedsViewModel = new FeedsViewModel();
			feedsViewModel.Initialize();

			var airportsViewModel = new AirportsViewModel();
			airportsViewModel.RefreshFromFeed( App.Repository.Feed );

			Pivot.Items.Add( new PivotItem { Header = "feeds", Content = new FeedsControl( feedsViewModel ) } );
			Pivot.Items.Add( new PivotItem { Header = "pinned", Content = new SelectPinnedAirportsControl( airportsViewModel ) } );
			Pivot.Items.Add( new PivotItem { Header = "language", Content = new LanguageControl() } );
			Pivot.Items.Add( new PivotItem { Header = "about", Content = new AboutControl() } );
		}
	}
}