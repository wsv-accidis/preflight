using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Accidis.Apps.Preflight.Model;

namespace Accidis.Apps.Preflight.Services.Parsers
{
    /// <summary>
    /// Parser that uses LFV's HTML feeds.
    /// </summary>
    public sealed class HtmlFeedParser : IFeedParser
    {
        readonly Regex _innerExpr = new Regex(
            @"<TR[^>]+>\s*<TD[^>]+>(?<iata>[A-Z]{4})</TD>(\s*<TD[^>]+>[^<]*</TD>)?\s*<TD[^>]+>(?<text>[^<]*)</TD>\s*</TR>",
            RegexOptions.CultureInvariant | RegexOptions.ExplicitCapture | RegexOptions.IgnoreCase );

        readonly Regex _outerExpr = new Regex(
            @"<TABLE[^>]*>(?<content>(\s*<TR[^>]+>\s*<TD[^>]+>[A-Z]{4}</TD>(\s*<TD[^>]+>[^<]*</TD>)?\s*<TD[^>]+>[^<]*</TD>\s*</TR>\s*)+)</TABLE>",
            RegexOptions.CultureInvariant | RegexOptions.ExplicitCapture | RegexOptions.IgnoreCase );

        public void ParseResponse( string response, Feed feed )
        {
            string[] feeds = SplitFeeds( response ).ToArray();
            if( feeds.Length % 2 != 1 )
                throw new InvalidOperationException( "Response is not of the expected format. The service may be down, or the format has changed. Try again later, or check for application updates." );

            for( int i = 0; i < feeds.Length - 1; i += 2 )
            {
                ParseSingleFeed( feeds[i], feed.MetarList );
                ParseSingleFeed( feeds[1 + i], feed.TafList );
            }

            ParseMultiFeed( feeds[feeds.Length - 1], feed.SigMetList );
            feed.Airports = feed.MetarList.Keys.Concat( feed.TafList.Keys ).Distinct().OrderBy( s => s ).ToList();
        }

        public string GenerateUrlFromFeeds( IEnumerable<FeedMetaData> feeds )
        {
            var buffer = new StringBuilder( UrlConsts.MetInfo );
            foreach( FeedMetaData feed in feeds )
            {
                buffer.AppendFormat( UrlConsts.MetInfoFileSuffix, feed.MetarFileHtml );
                buffer.AppendFormat( UrlConsts.MetInfoFileSuffix, feed.TafFileHtml );
            }

            buffer.AppendFormat( UrlConsts.MetInfoFileSuffix, UrlConsts.SigMetEurope );
            return buffer.ToString();
        }

        IEnumerable<string> SplitFeeds( string response )
        {
            Match match = _outerExpr.Match( response );

            while( match.Success )
            {
                yield return match.Groups["content"].Value;
                match = match.NextMatch();
            }
        }

        IEnumerable<KeyValuePair<string, string>> ParseFeed( string tableBody )
        {
            Match match = _innerExpr.Match( tableBody );

            while( match.Success )
            {
                string iataCode = match.Groups["iata"].Value,
                       text = match.Groups["text"].Value;

                yield return new KeyValuePair<string, string>( iataCode, text.Trim() );
                match = match.NextMatch();
            }
        }

        void ParseSingleFeed( string tableBody, IDictionary<string, string> list )
        {
            foreach( var pair in ParseFeed( tableBody ) )
                list[pair.Key] = pair.Value;
        }

        void ParseMultiFeed( string tableBody, IDictionary<string, IList<string>> list )
        {
            foreach( var pair in ParseFeed( tableBody ) )
            {
                IList<string> item;
                if( !list.TryGetValue( pair.Key, out item ) )
                {
                    item = new List<string>( 1 );
                    list.Add( pair.Key, item );
                }

                item.Add( pair.Value );
            }
        }
    }
}