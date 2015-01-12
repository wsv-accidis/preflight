using Accidis.Apps.Preflight.Model;
using Accidis.Apps.Preflight.Services;
using Accidis.Apps.Preflight.Services.Parsers;
using Microsoft.Phone.Controls;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Accidis.Apps.Preflight.Utils;
using Accidis.Apps.Preflight.Utils.Windows;

namespace Accidis.Apps.Preflight.Views
{
    public partial class TextPage : PhoneApplicationPage
    {
        const int _linesPerPage = 300;
        TextMetaData _content;
        IReadOnlyList<string> _downloadedContent;
        int _maxPage;
        int _page = 0;

        public TextPage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo( NavigationEventArgs e )
        {
            string textName = NavigationContext.QueryString["text"];
            _content = Texts.All.First( t => t.Name == textName );

            ShowProgress();
            BeginLoad();
            base.OnNavigatedTo( e );
        }

        protected override void OnNavigatingFrom( NavigatingCancelEventArgs e )
        {
            _content = null;
            ContentPanel.Children.Clear();
            base.OnNavigatingFrom( e );
        }

        void ShowProgress()
        {
            ContentGrid.Visibility = Visibility.Collapsed;
            WaitPanel.Visibility = Visibility.Visible;
            ProgressBar.IsIndeterminate = true;
        }

        void BeginLoad()
        {
            if( null == _content )
                return;

            var downloader = new TextContentDownloader();
            downloader.DownloadCompleted += OnDownloadCompleted;
            downloader.DownloadFailed += OnLoadingFailed;
            downloader.BeginDownloadText( _content.Url );
        }

        void OnDownloadCompleted( object sender, EventArgs e )
        {
            if( !Dispatcher.CheckAccess() )
            {
                Dispatcher.BeginInvoke( (EventHandler)OnDownloadCompleted, sender, e );
                return;
            }

            _downloadedContent = sender.Is<TextContentDownloader>().Lines;
            Debug.WriteLine( "TextPage: Loading {0} lines of content.", _downloadedContent.Count );
            InitializePaging();
            InitializeContent();

            ContentGrid.Visibility = Visibility.Visible;
            HideProgress();
        }

        void InitializeContent()
        {
            _page = ( _page > _maxPage ? _maxPage : _page );
            PageIndicator.Text = String.Concat( 1 + _page, '/', 1 + _maxPage );            
            IEnumerable<string> linesInPage = _downloadedContent.Skip( _page * _linesPerPage ).Take( _linesPerPage );
            SetContent( linesInPage );
        }

        void InitializePaging()
        {
            _maxPage = ( _downloadedContent.Count / _linesPerPage );

            if( _maxPage > 0 )
            {
                PageIndicator.Text = String.Concat( 1 + _page, '/', 1 + _maxPage );
                PageIndicatorBlock.Visibility = Visibility.Visible;
                PageInstructionBlock.Visibility = Visibility.Visible;
            }
        }

        void OnFlickCompleted( object sender, FlickGestureEventArgs e )
        {
            // Ignore vertical flicks - those are for the scroll view
            if( e.Direction != System.Windows.Controls.Orientation.Horizontal )
                return;

            if( e.HorizontalVelocity > 0 )
            {
                if( _page > 0 )
                {
                    Debug.WriteLine( "Flicked left, going from page {0} to {1}.", _page, _page - 1 );

                    _page--;
                    InitializeContent();
                }
                else
                    Debug.WriteLine( "Flicked left, but already on first page." );
            }
            else
            {
                if( _page < _maxPage )
                {
                    Debug.WriteLine( "Flicked right, going from page {0} to {1}.", _page, _page + 1 );

                    _page++;
                    InitializeContent();
                }
                else
                    Debug.WriteLine( "Flicked right, but already on last page." );
            }
        }

        void OnLoadingFailed( object sender, UnhandledExceptionEventArgs e )
        {
            if( !Dispatcher.CheckAccess() )
            {
                Dispatcher.BeginInvoke( (EventHandler<UnhandledExceptionEventArgs>)OnLoadingFailed, sender, e );
                return;
            }

            HideProgress();
            ShowError( e.ExceptionObject );
        }

        void HideProgress()
        {
            ProgressBar.IsIndeterminate = false;
            WaitPanel.Visibility = Visibility.Collapsed;
        }

        void SetContent( IEnumerable<string> lines )
        {
            ContentPanel.Children.Clear();
            ContentScroller.ScrollToVerticalOffset( 0 );

            foreach( string line in lines )
            {
                bool isHeader = line.StartsWith( TextContentParser.HeaderMarker );
                if( isHeader )
                {
                    ContentPanel.Children.Add( new TextBlock
                    {
                        FontSize = ThemeResources.PhoneFontSizeLarge,
                        Foreground = ThemeResources.PhoneAccentBrush,
                        Margin = new Thickness( 0, 10, 0, 5 ),
                        Text = line.Substring( TextContentParser.HeaderMarker.Length ),
                        TextWrapping = TextWrapping.Wrap
                    } );
                }
                else
                {
                    ContentPanel.Children.Add( new TextBlock
                    {
                        TextWrapping = TextWrapping.Wrap,
                        Text = line
                    } );
                }

            }
        }

        void ShowError( object exObj )
        {
            var ex = exObj as Exception;
            string message = ( null != ex ? ex.Message : "Unknown error." );
            ErrorText.Text = message;
            ErrorPanel.Visibility = Visibility.Visible;
        }        
    }
}