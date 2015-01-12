using System.Windows.Controls;

namespace Accidis.Apps.Preflight.ViewModels
{
	public class SwcChartViewModel : ViewModelBase
	{
		Image _source;

		public Image Source
		{
			get { return _source; }
			set { SetProperty( ref _source, value, "Source" ); }
		}
	}
}