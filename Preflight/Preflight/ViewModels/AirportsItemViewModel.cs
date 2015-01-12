namespace Accidis.Apps.Preflight.ViewModels
{
	public sealed class AirportsItemViewModel : ViewModelBase
	{
		string _iataCode;
		bool _selected;

		public string IataCode
		{
			get { return _iataCode; }
			set { SetProperty( ref _iataCode, value, "IataCode" ); }
		}

		public bool Selected
		{
			get { return _selected; }
			set { SetProperty( ref _selected, value, "Selected" ); }
		}
	}
}