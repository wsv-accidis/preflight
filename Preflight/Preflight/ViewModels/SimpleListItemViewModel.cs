namespace Accidis.Apps.Preflight.ViewModels
{
	public sealed class SimpleListItemViewModel : ViewModelBase
	{
		string _content;
		string _title;

		public string Title
		{
			get { return _title; }
			set { SetProperty( ref _title, value, "Title" ); }
		}

		public string Content
		{
			get { return _content; }
			set { SetProperty( ref _content, value, "Content" ); }
		}
	}
}