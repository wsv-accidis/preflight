using System.Collections.Specialized;
using System.Diagnostics;
using System.Linq;
using System.Windows.Controls;
using Accidis.Apps.Preflight.ViewModels;

namespace Accidis.Apps.Preflight.Views.Settings
{
	public partial class FeedsControl : UserControl
	{
		readonly FeedsViewModel _viewModel;
		bool _suppressUpdateViewModel;

		public FeedsControl( FeedsViewModel viewModel )
		{
			InitializeComponent();
			_viewModel = viewModel;
			_viewModel.Items.CollectionChanged += OnItemsChanged;

			DataContext = viewModel;
			RefreshSelected();
		}

		void RefreshSelected()
		{
			_suppressUpdateViewModel = true;

			List.SelectedItems.Clear();
			foreach( FeedsItemViewModel item in _viewModel.Items.Where( i => i.Selected ) )
				List.SelectedItems.Add( item );

			_suppressUpdateViewModel = false;
		}

		void OnItemsChanged( object sender, NotifyCollectionChangedEventArgs e )
		{
			RefreshSelected();
		}

		void OnSelectionChanged( object sender, SelectionChangedEventArgs e )
		{
			if( _suppressUpdateViewModel )
				return;

			foreach( FeedsItemViewModel item in _viewModel.Items )
				item.Selected = List.SelectedItems.Contains( item );

			SaveSelection();
		}

		void SaveSelection()
		{
			Debug.WriteLine( "FeedsControl: Persisted settings." );

			App.Settings.SelectedFeeds = _viewModel.Items
				.Where( s => s.Selected )
				.Select( s => s.Title )
				.ToArray();

			App.Settings.TriggerRefresh();
		}
	}
}