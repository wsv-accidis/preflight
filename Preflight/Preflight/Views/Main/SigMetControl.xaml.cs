using System.Windows;
using System.Windows.Controls;
using Accidis.Apps.Preflight.ViewModels;

namespace Accidis.Apps.Preflight.Views.Main
{
    public partial class SigMetControl : UserControl
    {
        readonly SigMetViewModel _viewModel;

        public SigMetControl( SigMetViewModel viewModel )
        {
            InitializeComponent();
            _viewModel = viewModel;
            DataContext = _viewModel;
        }

        void OnSelectionChanged( object sender, SelectionChangedEventArgs e )
        {
            object selectedItem = List.SelectedItem;
            foreach( SigMetAirportViewModel item in _viewModel.Items )
                item.Visibility = ( ReferenceEquals( selectedItem, item ) ? Visibility.Visible : Visibility.Collapsed );
        }
    }
}