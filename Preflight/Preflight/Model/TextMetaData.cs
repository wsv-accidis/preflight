using Accidis.Apps.Preflight.Services;
using System;

namespace Accidis.Apps.Preflight.Model
{
    public sealed class TextMetaData
    {
        public TextMetaData( string name, int torLinkId, string torType )
        {
            Name = name;
            Url = GenerateUrl( torLinkId, torType, UrlConsts.TorLink );
        }

        public string Name { get; private set; }

        public string Url { get; private set; }

        public override string ToString()
        {
            return Name;
        }

        static string GenerateUrl( int torLinkId, string torType, string baseUrl )
        {
            return String.Format( baseUrl, torLinkId, torType );
        }
    }
}