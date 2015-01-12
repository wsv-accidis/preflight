using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Accidis.Apps.Preflight.Controls
{
	public sealed class RoundButton : Button
	{
		public static readonly DependencyProperty ImageProperty = DependencyProperty.Register( "Image", typeof( ImageSource ), typeof( RoundButton ), null );
		object _layoutRoot;

		public RoundButton()
		{
			DefaultStyleKey = typeof( RoundButton );
		}

		public ImageSource Image
		{
			get { return (ImageSource) GetValue( ImageProperty ); }
			set { SetValue( ImageProperty, value ); }
		}

		public override void OnApplyTemplate()
		{
			_layoutRoot = GetTemplateChild( "LayoutRoot" ) as Border;
			Debug.Assert( _layoutRoot != null, "LayoutRoot is null" );
			base.OnApplyTemplate();
		}
	}
}