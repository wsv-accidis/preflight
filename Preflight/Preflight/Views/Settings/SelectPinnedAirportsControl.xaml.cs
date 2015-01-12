using System;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Accidis.Apps.Preflight.ViewModels;

namespace Accidis.Apps.Preflight.Views.Settings
{
	public partial class SelectPinnedAirportsControl : UserControl
	{
		const int _maxPinned = 10;
		readonly AirportsViewModel _viewModel;
		bool _suppressUpdateViewModel;

		public SelectPinnedAirportsControl( AirportsViewModel viewModel )
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
			foreach( AirportsItemViewModel item in _viewModel.Items.Where( i => i.Selected ) )
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

			if( List.SelectedItems.Count <= _maxPinned )
			{
				foreach( AirportsItemViewModel item in _viewModel.Items )
					item.Selected = List.SelectedItems.Contains( item );

				SaveSelection();
			}
			else
			{
				MessageBox.Show( String.Format( "You can't pin more than {0} airports. Sorry about that.", _maxPinned ), "Too many", MessageBoxButton.OK );
				foreach( object item in e.AddedItems )
					List.SelectedItems.Remove( item );
			}
		}

		void SaveSelection()
		{
			Debug.WriteLine( "SelectPinnedAirportsControl: Persisted settings." );

			App.Settings.PinnedAirports = _viewModel.Items
				.Where( s => s.Selected )
				.Select( s => s.IataCode )
				.ToArray();

			App.Settings.TriggerRefresh();
		}
	}
}