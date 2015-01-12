using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Accidis.Apps.Preflight.Model;
using Accidis.Apps.Preflight.Services;

namespace Accidis.Apps.Preflight.ViewModels
{
	public sealed class FeedsViewModel : ViewModelBase
	{
		public FeedsViewModel()
		{
			Items = new ObservableCollection<FeedsItemViewModel>();
		}

		public ObservableCollection<FeedsItemViewModel> Items { get; private set; }

		public void Initialize()
		{
			Items.Clear();

			IEnumerable<string> selectedFeeds = App.Settings.SelectedFeeds;
			foreach( FeedMetaData feed in Feeds.All )
			{
				bool isSelected = selectedFeeds.Contains( feed.Name );
				Items.Add( new FeedsItemViewModel { Title = feed.Name, Selected = isSelected } );
			}
		}
	}
}