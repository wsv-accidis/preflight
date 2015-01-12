using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Accidis.Apps.Preflight.Model;
using Accidis.Apps.Preflight.Services.Parsers;
using Accidis.Apps.Preflight.Utils.Diagnostics;
using Accidis.Apps.Preflight.Utils.Web;

namespace Accidis.Apps.Preflight.Services
{
	public sealed class FeedRepository : StringDownloaderBase<Feed>
	{
		readonly IFeedParser _feedParser = new HtmlFeedParser();

		public FeedRepository()
		{
			Feed = new Feed();
		}

		public Feed Feed { get; private set; }

		public event EventHandler FeedRefreshed;

		public void LoadOfflineFeed()
		{
			Feed feed;
			if( Feed.TryLoad( out feed ) )
			{
				Debug.WriteLine( "FeedRepository: Feed restored from isolated storage." );
				Feed = feed;

				if( null != FeedRefreshed )
					FeedRefreshed( this, EventArgs.Empty );
			}
		}

		public void PersistOfflineFeed()
		{
			Feed.Persist( Feed );
		}

		public void BeginDownloadFeed()
		{
			IEnumerable<FeedMetaData> feeds = Feeds.All.Where( f => App.Settings.SelectedFeeds.Contains( f.Name ) );
			string url = _feedParser.GenerateUrlFromFeeds( feeds );

			Debug.WriteLine( "FeedRepository: Downloading feed from: " + url );
			BeginDownload( url );
		}

		protected override void ReadResponseString( string response, Feed feed )
		{
			using( new LoggingTimer( _feedParser.GetType().Name + ".ParseResponse" ) )
				_feedParser.ParseResponse( response, feed );
		}

		protected override void AfterDownloadCompleted( Feed result )
		{
			// Only update public feed if requests actually got some data
			if( result.HasContent )
			{
				Feed = result;

				if( null != FeedRefreshed )
					FeedRefreshed( this, EventArgs.Empty );
			}
		}
	}
}