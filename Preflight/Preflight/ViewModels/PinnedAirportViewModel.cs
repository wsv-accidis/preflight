using System.Collections.ObjectModel;
using Accidis.Apps.Preflight.Model;
using Accidis.Apps.Preflight.Utils.Diagnostics;

namespace Accidis.Apps.Preflight.ViewModels
{
	public sealed class PinnedAirportViewModel : ViewModelBase
	{
		string _airport;

		public PinnedAirportViewModel()
		{
			Items = new ObservableCollection<SimpleListItemViewModel>();
		}

		public string Airport
		{
			get { return _airport; }
			set { SetProperty( ref _airport, value, "Airport" ); }
		}

		public ObservableCollection<SimpleListItemViewModel> Items { get; private set; }

		public void RefreshFromFeed( Feed feed )
		{
			using( new LoggingTimer( "PinnedAirportViewModel.RefreshFromFeed" ) )
			{
				Items.Clear();

				string temp;
                if( null != feed.MetarList && feed.MetarList.TryGetValue( Airport, out temp ) )
                    Items.Add( new SimpleListItemViewModel { Title = "METAR", Content = temp } );
                if( null != feed.TafList && feed.TafList.TryGetValue( Airport, out temp ) )
                    Items.Add( new SimpleListItemViewModel { Title = "TAF", Content = temp } );
                if( Items.Count == 0 )
                    Items.Add( new SimpleListItemViewModel { Title = "No data", Content = "Sorry, no TAF/METAR is currently available for this airport." } );
			}
		}
	}
}