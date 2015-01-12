using System.Collections.Generic;
using Accidis.Apps.Preflight.Model;

namespace Accidis.Apps.Preflight.Services.Parsers
{
	public interface IFeedParser
	{
		void ParseResponse( string response, Feed feed );
		string GenerateUrlFromFeeds( IEnumerable<FeedMetaData> feeds );
	}
}