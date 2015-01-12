using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Accidis.Apps.Preflight.Model;
using Accidis.Apps.Preflight.Utils.Diagnostics;

namespace Accidis.Apps.Preflight.ViewModels
{
	public sealed class AirportsViewModel : ViewModelBase
	{
		public AirportsViewModel()
		{
			Items = new ObservableCollection<AirportsItemViewModel>();
			foreach( string selected in App.Settings.PinnedAirports )
				Items.Add( new AirportsItemViewModel { IataCode = selected, Selected = true } );
		}

		public ObservableCollection<AirportsItemViewModel> Items { get; private set; }

		public void AddOrSelect( string iataCode )
		{
			AirportsItemViewModel item = Items.FirstOrDefault( s => String.Equals( s.IataCode, iataCode, StringComparison.OrdinalIgnoreCase ) );
			if( null != item )
				item.Selected = true;
			else
				Items.Insert( 0, new AirportsItemViewModel { IataCode = iataCode.ToUpper(), Selected = true } );
		}

		public void RefreshFromFeed( Feed feed )
		{
			using( new LoggingTimer( "AirportsViewModel.RefreshFromFeed" ) )
			{
				IEnumerable<string> selected = Items.Where( a => a.Selected ).Select( a => a.IataCode );
				IEnumerable<string> notSelected = feed.Airports.Where( a => !selected.Contains( a ) );

				AirportsItemViewModel[] items = selected.OrderBy( s => s )
					.Select( s => new AirportsItemViewModel { IataCode = s, Selected = true } )
					.Concat( notSelected.Select( s => new AirportsItemViewModel { IataCode = s } ) )
					.ToArray();

				Items.Clear();
				foreach( AirportsItemViewModel item in items )
					Items.Add( item );
			}
		}
	}
}