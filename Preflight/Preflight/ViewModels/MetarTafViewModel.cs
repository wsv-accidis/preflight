using System.Collections.ObjectModel;
using Accidis.Apps.Preflight.Model;
using Accidis.Apps.Preflight.Utils.Diagnostics;

namespace Accidis.Apps.Preflight.ViewModels
{
	public sealed class MetarTafViewModel : ViewModelBase
	{
		public MetarTafViewModel()
		{
			Items = new ObservableCollection<MetarTafAirportViewModel>();
		}

		public ObservableCollection<MetarTafAirportViewModel> Items { get; private set; }

		public void RefreshFromFeed( Feed feed )
		{
			using( new LoggingTimer( "MetarTafViewModel.RefreshFromFeed" ) )
			{
				Items.Clear();

				if( null == feed.Airports )
					return;

				foreach( string iataCode in feed.Airports )
				{
					string taf, metar;
					var item = new MetarTafAirportViewModel { IataCode = iataCode };
					if( null != feed.MetarList && feed.MetarList.TryGetValue( iataCode, out metar ) )
						item.Metar = metar;
					if( null != feed.TafList && feed.TafList.TryGetValue( iataCode, out taf ) )
						item.Taf = taf;

					Items.Add( item );
				}
			}
		}
	}
}