using System.Diagnostics;
using System.IO;
using System.Text;
using Accidis.Apps.Preflight.Utils.Diagnostics;

namespace Accidis.Apps.Preflight.Utils.Web
{
	public abstract class StringDownloaderBase<TResult> : WebDownloaderBase<TResult> where TResult : new()
	{
		protected virtual Encoding ContentEncoding
		{
			get { return Encoding.UTF8; }
		}

		protected override void ReadResponse( Stream stream, TResult result )
		{
			string content;

			using( new LoggingTimer( "StringDownloaderBase.ReadResponse" ) )
			using( var reader = new StreamReader( stream, ContentEncoding ) )
				content = reader.ReadToEnd();

			Debug.WriteLine( "Downloaded {0} bytes.", content.Length );
			ReadResponseString( content, result );
		}

		protected abstract void ReadResponseString( string response, TResult result );
	}
}