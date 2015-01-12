using System.Windows;
using System.Windows.Media;

namespace Accidis.Apps.Preflight.Utils.Windows
{
	public static class ThemeResources
	{
		public static double PhoneFontSizeLarge
		{
			get { return Application.Current.Resources["PhoneFontSizeLarge"].Is<double>(); }
		}

		public static SolidColorBrush PhoneAccentBrush
		{
			get { return Application.Current.Resources["PhoneAccentBrush"].Is<SolidColorBrush>(); }
		}

		public static Color PhoneAccentColor
		{
			get { return Application.Current.Resources["PhoneAccentColor"].Is<Color>(); }
		}

		public static SolidColorBrush PhoneDisabledBrush
		{
			get { return Application.Current.Resources["PhoneDisabledBrush"].Is<SolidColorBrush>(); }
		}

		public static SolidColorBrush PhoneForegroundBrush
		{
			get { return Application.Current.Resources["PhoneForegroundBrush"].Is<SolidColorBrush>(); }
		}

		public static bool UsesLightTheme
		{
			get { return Application.Current.Resources["PhoneLightThemeVisibility"].Is<Visibility>() == Visibility.Visible; }
		}
	}
}