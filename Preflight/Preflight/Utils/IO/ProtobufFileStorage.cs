using System;
using System.Diagnostics;
using System.IO;
using System.IO.IsolatedStorage;
using Accidis.Apps.Preflight.Utils.Diagnostics;
using ProtoBuf;

namespace Accidis.Apps.Preflight.Utils.IO
{
	public static class ProtoBufFileStorage<T>
		where T : class
	{
		public static void Persist( T obj, string storeName )
		{
			using( new LoggingTimer( "ProtoBufFileStorage.Persist" ) )
			using( IsolatedStorageFile filesystem = IsolatedStorageFile.GetUserStoreForApplication() )
				try
				{
					using( var fs = new IsolatedStorageFileStream( storeName, FileMode.Create, filesystem ) )
						Serializer.Serialize( fs, obj );
				}
				catch( Exception ex )
				{
					Debug.WriteLine( "Exception persisting feed to isolated storage:" );
					Debug.WriteLine( ex );
				}
		}

		public static bool TryLoad( out T obj, string storeName )
		{
			using( new LoggingTimer( "ProtoBufFileStorage.TryLoad" ) )
			using( IsolatedStorageFile filesystem = IsolatedStorageFile.GetUserStoreForApplication() )
			{
				if( !filesystem.FileExists( storeName ) )
				{
					obj = null;
					return false;
				}

				try
				{
					using( var fs = new IsolatedStorageFileStream( storeName, FileMode.Open, filesystem ) )
					{
						obj = Serializer.Deserialize<T>( fs );
						return ( obj != null );
					}
				}
				catch( Exception ex )
				{
					Debug.WriteLine( "Exception loading feed from isolated storage:" );
					Debug.WriteLine( ex );

					obj = null;
					return false;
				}
			}
		}
	}
}