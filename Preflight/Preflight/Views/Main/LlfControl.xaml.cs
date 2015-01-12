using System;
using System.Windows;
using System.Windows.Controls;
using Accidis.Apps.Preflight.Model;
using Accidis.Apps.Preflight.Services;
using Accidis.Apps.Preflight.Utils.Windows;

namespace Accidis.Apps.Preflight.Views.Main
{
    public partial class LlfControl : UserControl
    {
        public LlfControl()
        {
            InitializeComponent();
        }

        private void OnAreaAClicked( object sender, RoutedEventArgs e )
        {
            OpenTexts( Texts.LlfAreaASwe );
        }

        private void OnAreaBClicked( object sender, RoutedEventArgs e )
        {
            OpenTexts( Texts.LlfAreaBSwe );
        }

        private void OnAreaCClicked( object sender, RoutedEventArgs e )
        {
            OpenTexts( Texts.LlfAreaCSwe );
        }

        private void OnAreaEClicked( object sender, RoutedEventArgs e )
        {
            OpenTexts( Texts.LlfAreaESwe );
        }

        private static void OpenTexts( TextMetaData text )
        {
            Application.Current.Navigate( "/Views/TextPage.xaml?text={0}", Uri.EscapeUriString( text.Name ) );
        }
    }
}