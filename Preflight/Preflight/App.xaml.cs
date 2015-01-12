using System.Diagnostics;
using System.Windows;
using System.Windows.Navigation;
using Accidis.Apps.Preflight.Model;
using Accidis.Apps.Preflight.Services;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using ProtoBuf;

namespace Accidis.Apps.Preflight
{
	public partial class App
	{
		static readonly FeedRepository _repository = new FeedRepository();
		static AppSettings _settings;

		/// <summary>
		/// Constructor for the Application object.
		/// </summary>
		public App()
		{
			UnhandledException += OnApplicationUnhandledException;
			InitializeComponent();
			InitializePhoneApplication();

			Serializer.PrepareSerializer<Feed>();
			Serializer.PrepareSerializer<AppSettings>();

			if( Debugger.IsAttached )
			{
				// Display the current frame rate counters.
				//Current.Host.Settings.EnableFrameRateCounter = true;

				// Show the areas of the app that are being redrawn in each frame.
				//Application.Current.Host.Settings.EnableRedrawRegions = true;

				// Enable non-production analysis visualization mode, 
				// which shows areas of a page that are handed off to GPU with a colored overlay.
				//Application.Current.Host.Settings.EnableCacheVisualization = true;

				// Disable the application idle detection by setting the UserIdleDetectionMode property of the
				// application's PhoneApplicationService object to Disabled.
				// Caution:- Use this under debug mode only. Application that disables user idle detection will continue to run
				// and consume battery power when the user is not using the phone.
				PhoneApplicationService.Current.UserIdleDetectionMode = IdleDetectionMode.Disabled;
			}
		}

		public static FeedRepository Repository
		{
			get { return _repository; }
		}

		public static AppSettings Settings
		{
			get { return _settings; }
		}

		/// <summary>
		/// Provides easy access to the root frame of the Phone Application.
		/// </summary>
		/// <returns>The root frame of the Phone Application.</returns>
		public PhoneApplicationFrame RootFrame { get; private set; }

		void OnApplicationLaunching( object sender, LaunchingEventArgs e )
		{
			Debug.WriteLine( "App: Launching" );
			LoadState();
		}

		void OnApplicationActivated( object sender, ActivatedEventArgs e )
		{
			Debug.WriteLine( "App: Activated" );
			LoadState();
		}

		void OnApplicationDeactivated( object sender, DeactivatedEventArgs e )
		{
			Debug.WriteLine( "App: Deactivated" );
			PersistState();
		}

		void OnApplicationClosing( object sender, ClosingEventArgs e )
		{
			Debug.WriteLine( "App: Closing" );
			PersistState();
		}

		static void LoadState()
		{
			if( !AppSettings.TryLoad( out _settings ) )
				_settings = new AppSettings();

			Repository.LoadOfflineFeed();
		}

		static void PersistState()
		{
			AppSettings.Persist( _settings );
			Repository.PersistOfflineFeed();
		}

		// Code to execute if a navigation fails
		static void OnRootFrameNavigationFailed( object sender, NavigationFailedEventArgs e )
		{
			if( Debugger.IsAttached )
				Debugger.Break();
		}

		// Code to execute on Unhandled Exceptions
		static void OnApplicationUnhandledException( object sender, ApplicationUnhandledExceptionEventArgs e )
		{
			if( Debugger.IsAttached )
				Debugger.Break();
		}

		#region Phone application initialization

		// Avoid double-initialization
		bool _phoneApplicationInitialized;

		// Do not add any additional code to this method
		void InitializePhoneApplication()
		{
			if( _phoneApplicationInitialized )
				return;

			// Create the frame but don't set it as RootVisual yet; this allows the splash
			// screen to remain active until the application is ready to render.
			RootFrame = new TransitionFrame();
			RootFrame.Navigated += CompleteInitializePhoneApplication;

			// Handle navigation failures
			RootFrame.NavigationFailed += OnRootFrameNavigationFailed;

			// Ensure we don't initialize again
			_phoneApplicationInitialized = true;
		}

		// Do not add any additional code to this method
		void CompleteInitializePhoneApplication( object sender, NavigationEventArgs e )
		{
			// Set the root visual to allow the application to render
			if( RootVisual != RootFrame )
				RootVisual = RootFrame;

			// Remove this handler since it is no longer needed
			RootFrame.Navigated -= CompleteInitializePhoneApplication;
		}

		#endregion
	}
}