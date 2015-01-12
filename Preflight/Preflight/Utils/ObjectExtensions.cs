namespace Accidis.Apps.Preflight.Utils
{
	public static class ObjectExtensions
	{
		public static T Is<T>( this object obj )
		{
			return (T) obj;
		}
	}
}