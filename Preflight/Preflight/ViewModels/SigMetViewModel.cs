using System.Collections.Generic;
using System.Collections.ObjectModel;
using Accidis.Apps.Preflight.Model;
using System;
using Accidis.Apps.Preflight.Utils.Diagnostics;

namespace Accidis.Apps.Preflight.ViewModels
{
    public sealed class SigMetViewModel : ViewModelBase
    {
        public SigMetViewModel()
        {
            Items = new ObservableCollection<SigMetAirportViewModel>();
        }

        public ObservableCollection<SigMetAirportViewModel> Items { get; private set; }

        public void RefreshFromFeed( Feed feed )
        {
            using( new LoggingTimer( "SigMetViewModel.RefreshFromFeed" ) )
            {
                Items.Clear();

                if( null == feed.SigMetList )
                    return;

                foreach( var pair in feed.SigMetList )
                {
                    string sigMets = String.Join( Environment.NewLine, pair.Value );
                    Items.Add( new SigMetAirportViewModel { IataCode = pair.Key, SigMet = sigMets } );
                }
            }
        }
    }
}
