using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;

namespace Accidis.Apps.Preflight.Services.Parsers
{
    public sealed class TextContentParser : ITextContentParser
    {
        public const string HeaderMarker = "#HEAD#";

        private const char _newLine = '\n';
        private const string _doubleNewLine = "\n\n";
        private const string _noTitle = "";

        private readonly Regex _titleExpr = new Regex( "<h1 class=\"tor-link-header\">(?<content>(.+?))</h1>",
                                                        RegexOptions.CultureInvariant | RegexOptions.ExplicitCapture |
                                                        RegexOptions.IgnoreCase | RegexOptions.Singleline );

        private readonly Regex _contentExpr = new Regex( "<pre[^>]*>(?<content>(.+?))</pre>",
                                                        RegexOptions.CultureInvariant | RegexOptions.ExplicitCapture |
                                                        RegexOptions.IgnoreCase | RegexOptions.Singleline );

        private readonly Regex _collapseNewlinesExpr = new Regex( "(\\s*\\n)(\\s*\\n)",
                                                        RegexOptions.CultureInvariant | RegexOptions.Singleline );

        #region ITextContentParser Members

        public void ParseResponse( string response, List<string> results )
        {
            string[] titles = GetContent( response, _titleExpr ).Skip( 1 ).ToArray(),
                contents = GetContent( response, _contentExpr ).ToArray();

            for( int i = 0; i < contents.Length; i++ )
            {
                if( i < titles.Length )
                    results.Add( String.Concat( HeaderMarker, titles[i] ) );

                string[] content = SplitContentByLines( _collapseNewlinesExpr.Replace( contents[i], _doubleNewLine ) );
                results.AddRange( content );
            }
        }

        #endregion

        private IEnumerable<string> GetContent( string response, Regex expr )
        {
            Match match = expr.Match( response );

            while( match.Success )
            {
                yield return HttpUtility.HtmlDecode( match.Groups["content"].Value );
                match = match.NextMatch();
            }
        }

        private static string[] SplitContentByLines( string content )
        {
            return content.Split( _newLine ).Select( line => line.Trim() ).ToArray();
        }
    }
}