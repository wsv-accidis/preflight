using System;
using System.Diagnostics;

namespace Accidis.Apps.Preflight.Utils.Diagnostics
{
	public sealed class Logger
	{
		readonly string _typeName;

		Logger( Type loggedType )
		{
			_typeName = loggedType.FullName;
		}

		public static Logger Create<T>()
		{
			return new Logger( typeof( T ) );
		}

		public static Logger Create( Type loggedType )
		{
			return new Logger( loggedType );
		}

		[Conditional( "DEBUG" )]
		public void Debug( string str )
		{
			System.Diagnostics.Debug.WriteLine( "{0}: {1}", _typeName, str );
		}

		[Conditional( "DEBUG" )]
		public void Debug( string format, params object[] args )
		{
			string str = String.Format( format, args );
			Debug( str );
		}
	}
}