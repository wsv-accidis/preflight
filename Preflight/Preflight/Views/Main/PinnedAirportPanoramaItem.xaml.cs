using System.Windows.Controls;
using Accidis.Apps.Preflight.ViewModels;

namespace Accidis.Apps.Preflight.Views.Main
{
	public partial class PinnedAirportPanoramaItem : UserControl
	{
		public PinnedAirportPanoramaItem( PinnedAirportViewModel viewModel )
		{
			InitializeComponent();
			DataContext = viewModel;
		}
	}
}