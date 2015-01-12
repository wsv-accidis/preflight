using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using Accidis.Apps.Preflight.Model;
using Accidis.Apps.Preflight.Utils.Diagnostics;
using Accidis.Apps.Preflight.Utils.Windows;
using Accidis.Apps.Preflight.ViewModels;
using Accidis.Apps.Preflight.Views.Main;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Microsoft.Phone.Tasks;

namespace Accidis.Apps.Preflight.Views
{
    public partial class MainPage
    {
        readonly MetarTafViewModel _metarTaf = new MetarTafViewModel();
        readonly Dictionary<string, PinnedAirportViewModel> _pinnedAirports = new Dictionary<string, PinnedAirportViewModel>();
        readonly SigMetViewModel _sigMet = new SigMetViewModel();
        bool _feedDownloaded;
        bool _needsInit;
        Popup _splashScreen = new Popup { Child = new SplashScreen(), IsOpen = true };

        public MainPage()
        {
            InitializeComponent();
            InitializeBackgroundImage();
            InitializePanorama();

            App.Repository.DownloadStarted += OnDownloadStarted;
            App.Repository.DownloadFailed += OnDownloadFailed;
            App.Repository.DownloadCompleted += OnDownloadCompleted;
            App.Repository.FeedRefreshed += OnFeedRefreshed;
            App.Settings.Refresh += OnSettingsRefreshed;
        }

        protected override void OnNavigatedTo( NavigationEventArgs e )
        {
            Debug.WriteLine( "MainPage: Navigated to." );
            base.OnNavigatedTo( e );

            if( _needsInit )
            {
                _needsInit = false;
                InitializePanorama();
            }

            if( !_feedDownloaded )
            {
                _feedDownloaded = true;
                App.Repository.BeginDownloadFeed();
            }

            if( null != _splashScreen )
            {
                _splashScreen.IsOpen = false;
                _splashScreen = null;
                ApplicationBar.IsVisible = true;
            }
        }

        void InitializeBackgroundImage()
        {
            string path = ThemeResources.UsesLightTheme
                              ? "../Resources/PanoramaBackgroundLight.jpg"
                              : "../Resources/PanoramaBackgroundDark.jpg";

            Panorama.Background = new ImageBrush
                                      {
                                          ImageSource = new BitmapImage( new Uri( path, UriKind.Relative ) ),
                                          Stretch = Stretch.None
                                      };
        }

        void InitializePanorama()
        {
            using( new LoggingTimer( "InitializePanorama" ) )
            {
                Panorama.Items.Clear();

                _pinnedAirports.Clear();
                foreach( string airport in App.Settings.PinnedAirports.OrderBy( s => s ) )
                {
                    var viewModel = new PinnedAirportViewModel { Airport = airport };
                    _pinnedAirports.Add( airport, viewModel );
                    Panorama.Items.Add( new PinnedAirportPanoramaItem( viewModel ) );
                }

                Panorama.Items.Add( new PanoramaItem { Header = "metar/taf", Content = new MetarTafControl( _metarTaf ) } );
                Panorama.Items.Add( new PanoramaItem { Header = "sigmet", Content = new SigMetControl( _sigMet ) } );

                if( App.Settings.UseSwedishFeeds )
                    Panorama.Items.Add( new PanoramaItem { Header = "low level fc", Content = new LlfControl() } );

                Panorama.Items.Add( new PanoramaItem { Header = "notam", Content = new NotamControl() } );
                Panorama.Items.Add( new PanoramaItem { Header = "charts", Content = new ChartsControl() } );
            }

            RefreshFromFeed();
        }

        void RefreshFromFeed()
        {
            Feed feed = App.Repository.Feed;

            _metarTaf.RefreshFromFeed( feed );
            _sigMet.RefreshFromFeed( feed );

            foreach( string airport in App.Settings.PinnedAirports )
            {
                if( !_pinnedAirports.ContainsKey( airport ) )
                    continue; // should never actually happen, but we don't want to crash just in case...

                PinnedAirportViewModel item = _pinnedAirports[airport];
                item.RefreshFromFeed( feed );
            }
        }

        void StartProgress()
        {
            ProgressBar.Visibility = Visibility.Visible;
            ProgressBar.IsIndeterminate = true;
        }

        void StopProgress()
        {
            ProgressBar.IsIndeterminate = false;
            ProgressBar.Visibility = Visibility.Collapsed;
        }

        void OnDownloadStarted( object sender, EventArgs e )
        {
            if( !Dispatcher.CheckAccess() )
            {
                Dispatcher.BeginInvoke( (EventHandler)OnDownloadStarted, sender, e );
                return;
            }

            StartProgress();
        }

        void OnDownloadCompleted( object sender, EventArgs e )
        {
            if( !Dispatcher.CheckAccess() )
            {
                Dispatcher.BeginInvoke( (EventHandler)OnDownloadCompleted, sender, e );
                return;
            }

            StopProgress();
        }

        void OnFeedRefreshed( object sender, EventArgs e )
        {
            if( !Dispatcher.CheckAccess() )
            {
                Dispatcher.BeginInvoke( (EventHandler)OnFeedRefreshed, sender, e );
                return;
            }

            RefreshFromFeed();
        }

        void OnDownloadFailed( object sender, UnhandledExceptionEventArgs e )
        {
            if( !Dispatcher.CheckAccess() )
            {
                Dispatcher.BeginInvoke( (EventHandler<UnhandledExceptionEventArgs>)OnDownloadFailed, sender, e );
                return;
            }

            StopProgress();

            var ex = e.ExceptionObject as Exception;
            string message = String.Concat( "Oops. The feed could not be downloaded due to the following error:", Environment.NewLine,
                                           ( null != ex ? ex.Message : "Unknown error." ), Environment.NewLine, Environment.NewLine,
                                           "Sorry about that. Please note, information displayed may be out of date." );

            MessageBox.Show( message, "Error", MessageBoxButton.OK );
        }

        void OnFuelCalculatorClicked( object sender, EventArgs e )
        {
            Application.Current.Navigate( "/Views/FuelCalculatorPage.xaml" );
        }

        void OnRefreshClicked( object sender, EventArgs e )
        {
            App.Repository.BeginDownloadFeed();
        }

        void OnOpenFpcClicked( object sender, EventArgs e )
        {
            var task = new WebBrowserTask { Uri = new Uri( "http://www.lfv.se/sv/FPC/" ) };
            task.Show();
        }

        void OnSettingsClicked( object sender, EventArgs e )
        {
            Application.Current.Navigate( "/Views/SettingsPage.xaml" );
        }

        void OnWindCalculatorClicked( object sender, EventArgs e )
        {
            Application.Current.Navigate( "/Views/WindCalculatorPage.xaml" );
        }

        void OnApplicationBarStateChanged( object sender, ApplicationBarStateChangedEventArgs e )
        {
            ApplicationBar.Opacity = ( e.IsMenuVisible ? 0.9 : 0.6 );
        }

        void OnSettingsRefreshed( object sender, EventArgs e )
        {
            Debug.WriteLine( "MainPage: Will refresh settings on next init." );
            _needsInit = true;
        }
    }
}