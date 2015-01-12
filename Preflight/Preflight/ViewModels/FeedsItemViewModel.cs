namespace Accidis.Apps.Preflight.ViewModels
{
	public sealed class FeedsItemViewModel : ViewModelBase
	{
		string _title;
		bool _selected;

		public string Title
		{
			get { return _title; }
			set { SetProperty( ref _title, value, "Title" ); }
		}

		public bool Selected
		{
			get { return _selected; }
			set { SetProperty( ref _selected, value, "Selected" ); }
		}
	}
}