using System.Windows;

namespace Accidis.Apps.Preflight.ViewModels
{
    public sealed class SigMetAirportViewModel : ViewModelBase
    {
        string _iataCode;
        string _sigMet;
        Visibility _visibility = Visibility.Collapsed;

        public string IataCode
        {
            get { return _iataCode; }
            set { SetProperty( ref _iataCode, value, "IataCode" ); }
        }

        public string SigMet
        {
            get { return _sigMet; }
            set { SetProperty( ref _sigMet, value, "SigMet" ); }
        }

        public Visibility Visibility
        {
            get { return _visibility; }
            set { SetProperty( ref _visibility, value, "Visibility" ); }
        }
    }
}