﻿using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Accidis.Apps.Preflight.Controls
{
	public sealed class ImageBrushConverter : IValueConverter
	{
		public object Convert( object value, Type targetType, object parameter, CultureInfo culture )
		{
			var image = (BitmapImage) value;
			var imageBrush = new ImageBrush();

			if( image != null )
				imageBrush.ImageSource = image;
			return imageBrush;
		}

		public object ConvertBack( object value, Type targetType, object parameter, CultureInfo culture )
		{
			throw new NotImplementedException();
		}
	}
}