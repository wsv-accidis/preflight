using Accidis.Apps.Preflight.Services.Parsers;
using System.Collections.Generic;
using System.Diagnostics;
using Accidis.Apps.Preflight.Utils.Diagnostics;
using Accidis.Apps.Preflight.Utils.Web;

namespace Accidis.Apps.Preflight.Services
{
	public sealed class TextContentDownloader : StringDownloaderBase<List<string>>
	{
		readonly ITextContentParser _parser = new TextContentParser();

		public IReadOnlyList<string> Lines { get; private set; }

		public void BeginDownloadText( string textUri )
		{
			Debug.WriteLine( "TextContentDownloader: Downloading text from " + textUri );
			BeginDownload( textUri );
		}

		protected override void ReadResponseString( string response, List<string> result )
		{
			using( new LoggingTimer( _parser.GetType().Name + ".ParseResponse" ) )
				_parser.ParseResponse( response, result );
		}

        protected override void AfterDownloadCompleted( List<string> result )
		{
            Lines = result;
		}
	}
}