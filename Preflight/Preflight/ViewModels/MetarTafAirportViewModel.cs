using System;
using System.Windows;

namespace Accidis.Apps.Preflight.ViewModels
{
	public sealed class MetarTafAirportViewModel : ViewModelBase
	{
		const string _noMetar = "No METAR";
		const string _noTaf = "No TAF";
		string _iataCode;
		string _metar = _noMetar;
		string _taf = _noTaf;
		Visibility _visibility = Visibility.Collapsed;

		public string IataCode
		{
			get { return _iataCode; }
			set { SetProperty( ref _iataCode, value, "IataCode" ); }
		}

		public string Metar
		{
			get { return _metar; }
			set
			{
				if( String.IsNullOrEmpty( value ) )
					value = _noMetar;

				SetProperty( ref _metar, value, "Metar" );
			}
		}

		public string Taf
		{
			get { return _taf; }
			set
			{
				if( String.IsNullOrEmpty( value ) )
					value = _noTaf;

				SetProperty( ref _taf, value, "Taf" );
			}
		}

		public Visibility Visibility
		{
			get { return _visibility; }
			set { SetProperty( ref _visibility, value, "Visibility" ); }
		}
	}
}