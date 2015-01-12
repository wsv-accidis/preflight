using Accidis.Apps.Preflight.Model;
using System.Collections.Generic;

namespace Accidis.Apps.Preflight.Services
{
    /// <remarks>
    /// Last checked valid 2014-12-21.
    /// </remarks>
	public static class Feeds
    {
        /// <summary>
        /// Name of default feed to select when starting blank.
        /// </summary>
        public const string Default = "Sweden";        

		static IEnumerable<FeedMetaData> _all;

		public static IEnumerable<FeedMetaData> All
		{
			get
			{
				return _all ?? ( _all = new[]
				                          	{
				                          		new FeedMetaData( "Sweden", "metar.sweden.list", "taffc.sweden.list" ),
				                          		new FeedMetaData( "Norway", "metar.en.list", "taffc.en.list" ),
				                          		new FeedMetaData( "Finland", "metar.ef.list", "taffc.ef.list" ),
				                          		new FeedMetaData( "Denmark", "metar.ek.list", "taffc.ek.list" ),
				                          		new FeedMetaData( "Iceland", "metar.bi.list", "taffc.bi.list" ),
				                          		new FeedMetaData( "UK", "metar.eg.list", "taffc.eg.list" ),
				                          		new FeedMetaData( "Germany", "metar.ed.list", "taffc.ed.list" ),
				                          		new FeedMetaData( "Benelux", "metar.benelux.list", "taffc.benelux.list" ),
				                          		new FeedMetaData( "Austria, Switzerland", "metar.alps.list", "taffc.alps.list" ),
				                          		new FeedMetaData( "France", "metar.lf.list", "taffc.lf.list" ),
				                          		new FeedMetaData( "Spain", "metar.le.list", "taffc.le.list" ),
				                          		new FeedMetaData( "Portugal", "metar.lp.list", "taffc.lp.list" ),
				                          		new FeedMetaData( "Italy", "metar.li.list", "taffc.li.list" ),
				                          		new FeedMetaData( "Baltic", "metar.baltic.list", "taffc.baltic.list" ),
				                          		new FeedMetaData( "Other", "metar.others.list", "taffc.others.list" ),
				                          	} );
			}
		}
	}
}