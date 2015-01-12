using System;
using System.Windows;
using Microsoft.Phone.Controls;

namespace Accidis.Apps.Preflight.Utils.Windows
{
	public static class AppExtensions
	{
		public static void Navigate( this Application app, string format, params object[] args )
		{
			app.RootVisual.Is<PhoneApplicationFrame>().Navigate( new Uri( String.Format( format, args ), UriKind.Relative ) );
		}
	}
}