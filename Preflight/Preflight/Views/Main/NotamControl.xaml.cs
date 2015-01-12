using System;
using System.Windows;
using System.Windows.Controls;
using Accidis.Apps.Preflight.Model;
using Accidis.Apps.Preflight.Services;
using Accidis.Apps.Preflight.Utils.Windows;

namespace Accidis.Apps.Preflight.Views.Main
{
	public partial class NotamControl : UserControl
	{
		public NotamControl()
		{
			InitializeComponent();
		}

		void OnSwedenVfrClicked( object sender, RoutedEventArgs e )
		{
			OpenTexts( Texts.NotamSwedenVfr );
		}

		void OnSwedenIfrClicked( object sender, RoutedEventArgs e )
		{
			OpenTexts( Texts.NotamSwedenIfr );
		}

		void OnSwedenFourDayClicked( object sender, RoutedEventArgs e )
		{
			OpenTexts( Texts.NotamSwedenFourDay );
		}

        void OnSwedenHeliClicked( object sender, RoutedEventArgs e )
        {
            OpenTexts( Texts.NotamSwedenHeli );
        }

		void OnNorwayClicked( object sender, RoutedEventArgs e )
		{
			OpenTexts( Texts.NotamNorway );
		}

		void OnFinlandClicked( object sender, RoutedEventArgs e )
		{
			OpenTexts( Texts.NotamFinland );
		}

		void OnGermanyClicked( object sender, RoutedEventArgs e )
		{
			OpenTexts( Texts.NotamGermany );
		}

		void OnDenmarkClicked( object sender, RoutedEventArgs e )
		{
			OpenTexts( Texts.NotamDenmark );
		}

		void OnEstoniaClicked( object sender, RoutedEventArgs e )
		{
			OpenTexts( Texts.NotamEstonia );
		}

		void OnPolandClicked( object sender, RoutedEventArgs e )
		{
			OpenTexts( Texts.NotamPoland );
		}

		void OnLatviaClicked( object sender, RoutedEventArgs e )
		{
			OpenTexts( Texts.NotamLatvia );
		}

		void OnLithuaniaClicked( object sender, RoutedEventArgs e )
		{
			OpenTexts( Texts.NotamLithuania );
		}

		static void OpenTexts( TextMetaData text )
		{
			Application.Current.Navigate( "/Views/TextPage.xaml?text={0}", Uri.EscapeUriString( text.Name ) );
		}
	}
}