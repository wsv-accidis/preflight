using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using Accidis.Apps.Preflight.ViewModels;

namespace Accidis.Apps.Preflight.Views.Main
{
	public partial class MetarTafControl : UserControl
	{
		readonly MetarTafViewModel _viewModel;
		readonly CollectionViewSource _viewSource;
		string _filter;
		bool _hasFilter;

		public MetarTafControl( MetarTafViewModel viewModel )
		{
			InitializeComponent();
			_viewModel = viewModel;

			_viewSource = (CollectionViewSource) Resources["ViewSource"];
			_viewSource.Source = _viewModel.Items;
			_viewSource.Filter += ViewSourceFilter;
		}

		void ViewSourceFilter( object sender, FilterEventArgs e )
		{
			e.Accepted = !_hasFilter || ( (MetarTafAirportViewModel) e.Item ).IataCode.StartsWith( _filter );
		}

		void OnSelectionChanged( object sender, SelectionChangedEventArgs e )
		{
			object selectedItem = List.SelectedItem;
			foreach( MetarTafAirportViewModel item in _viewModel.Items )
				item.Visibility = ( ReferenceEquals( selectedItem, item ) ? Visibility.Visible : Visibility.Collapsed );
		}

		void OnSearchFieldGotFocus( object sender, EventArgs e )
		{
			SearchField.Text = String.Empty;
		}

		void OnSearchFieldLostFocus( object sender, EventArgs e )
		{
			ApplyFilter( SearchField.Text );
		}

		void OnSearchFieldTextChanged( object sender, TextChangedEventArgs e )
		{
			int pos = SearchField.SelectionStart;
			SearchField.Text = SearchField.Text.ToUpper();
			SearchField.SelectionStart = pos;
		}

		void OnShowAllClicked( object sender, EventArgs e )
		{
			ApplyFilter( String.Empty );
		}

		void OnSearchClicked( object sender, EventArgs e )
		{
			ApplyFilter( SearchField.Text );
			List.Focus();
		}

		void ApplyFilter( string filter )
		{
			filter = filter.Trim();

			if( _filter != filter )
			{
				_filter = filter;
				_hasFilter = !String.IsNullOrWhiteSpace( _filter );
				ShowAllButton.IsEnabled = _hasFilter;
				SearchField.Text = _filter;
				_viewSource.View.Refresh();

				if( _hasFilter )
					_viewSource.View.MoveCurrentToFirst();
				else
					_viewSource.View.MoveCurrentTo( null );
			}
		}
	}
}