using Microsoft.Phone.Controls;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;

namespace Accidis.Apps.Preflight.Views
{
    public partial class ChartPage : PhoneApplicationPage
    {
        const double MaxScale = 3;

        BitmapImage _bitmap;
        double _coercedScale;
        double _minScale;
        double _originalScale;
        bool _pinching;
        double _scale = 1.0;
        Point _screenMidpoint;
        Point _relativeMidpoint;
        Size _viewportSize;

        public ChartPage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo( NavigationEventArgs e )
        {
            string chartUri = NavigationContext.QueryString["chart"];

            ShowProgress();
            BeginLoad( chartUri );

            base.OnNavigatedTo( e );
        }

        void BeginLoad( string uri )
        {
            Debug.WriteLine( "ChartPage: Downloading chart from " + uri );
            BitmapImage bmi = new BitmapImage();
            bmi.CreateOptions = BitmapCreateOptions.IgnoreImageCache;
            bmi.ImageOpened += OnImageDownloaded;
            bmi.ImageFailed += OnImageFailed;
            bmi.DownloadProgress += OnImageDownloadProgress;
            bmi.UriSource = new Uri( uri, UriKind.Absolute );            
        }

        void CoerceScale( bool recompute )
        {
            if( recompute && _bitmap != null && ImagePort != null )
            {
                // Calculate the minimum scale to fit the viewport 
                double minX = ImagePort.ActualWidth / _bitmap.PixelWidth;
                double minY = ImagePort.ActualHeight / _bitmap.PixelHeight;

                _minScale = Math.Min( minX, minY );
            }

            _coercedScale = Math.Min( MaxScale, Math.Max( _scale, _minScale ) );
        }

        void HideProgress()
        {
            WaitPanel.Visibility = Visibility.Collapsed;
        }

        void OnImageDownloaded( object sender, RoutedEventArgs e )
        {                      
            RootGrid.Background = new SolidColorBrush( Colors.White );
            HideProgress();

            BitmapImage source = (BitmapImage)sender;
            _bitmap = source;
            Image.Source = source; 
            Image.Visibility = Visibility.Visible;

            _scale = 0;
            CoerceScale( true );
            _scale = _coercedScale;
            ResizeImage( true );
        }

        void OnImageDownloadProgress( object sender, DownloadProgressEventArgs e )
        {
            ProgressBar.Value = ( e.Progress == 100 ) ? 0 : e.Progress;
            Debug.WriteLine( e.Progress );
        }

        void OnImageFailed( object sender, ExceptionRoutedEventArgs e )
        {
            HideProgress();
            ShowError( e.ErrorException );
        }        

        void OnManipulationCompleted( object sender, ManipulationCompletedEventArgs e )
        {
            _pinching = false;
            _scale = _coercedScale;
        }

        void OnManipulationDelta( object sender, ManipulationDeltaEventArgs e )
        {
            if( e.PinchManipulation != null )
            {
                e.Handled = true;

                if( !_pinching )
                {
                    _pinching = true;
                    Point center = e.PinchManipulation.Original.Center;
                    _relativeMidpoint = new Point( center.X / Image.ActualWidth, center.Y / Image.ActualHeight );

                    var xform = Image.TransformToVisual( ImagePort );
                    _screenMidpoint = xform.Transform( center );
                }

                _scale = _originalScale * e.PinchManipulation.CumulativeScale;

                CoerceScale( false );
                ResizeImage( false );
            }
            else if( _pinching )
            {
                _pinching = false;
                _originalScale = _scale = _coercedScale;
            }
        }

        void OnManipulationStarted( object sender, ManipulationStartedEventArgs e )
        {
            _pinching = false;
            _originalScale = _scale;
        }

        void OnViewportChanged( object sender, ViewportChangedEventArgs e )
        {
            Size newSize = new Size( ImagePort.Viewport.Width, ImagePort.Viewport.Height );
            if( newSize != _viewportSize )
            {
                _viewportSize = newSize;
                CoerceScale( true );
                ResizeImage( false );
            }
        }

        void ResizeImage( bool center )
        {
            if( _coercedScale != 0 && _bitmap != null )
            {
                double newWidth = ImageCanvas.Width = Math.Round( _bitmap.PixelWidth * _coercedScale );
                double newHeight = ImageCanvas.Height = Math.Round( _bitmap.PixelHeight * _coercedScale );

                ImageTransform.ScaleX = ImageTransform.ScaleY = _coercedScale;

                ImagePort.Bounds = new Rect( 0, 0, newWidth, newHeight );

                if( center )
                {
                    ImagePort.SetViewportOrigin(
                        new Point(
                            Math.Round( ( newWidth - ImagePort.ActualWidth ) / 2 ),
                            Math.Round( ( newHeight - ImagePort.ActualHeight ) / 2 )
                            ) );
                }
                else
                {
                    Point newImgMid = new Point( newWidth * _relativeMidpoint.X, newHeight * _relativeMidpoint.Y );
                    Point origin = new Point( newImgMid.X - _screenMidpoint.X, newImgMid.Y - _screenMidpoint.Y );
                    ImagePort.SetViewportOrigin( origin );
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

        void ShowProgress()
        {
            Image.Visibility = Visibility.Collapsed;
            ErrorPanel.Visibility = Visibility.Collapsed;
            WaitPanel.Visibility = Visibility.Visible;
        }
    }
}