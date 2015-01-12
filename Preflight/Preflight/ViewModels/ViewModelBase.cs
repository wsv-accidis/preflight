using System.ComponentModel;

namespace Accidis.Apps.Preflight.ViewModels
{
	public class ViewModelBase : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		protected void SetProperty<T>( ref T field, T value, string propertyName )
		{
			if( !Equals( field, value ) )
			{
				field = value;
				OnPropertyChanged( propertyName );
			}
		}

		protected virtual void OnPropertyChanged( string propertyName )
		{
			OnPropertyChanged( new PropertyChangedEventArgs( propertyName ) );
		}

		protected virtual void OnPropertyChanged( PropertyChangedEventArgs args )
		{
			PropertyChangedEventHandler handler = PropertyChanged;
			if( handler != null )
				handler( this, args );
		}
	}
}