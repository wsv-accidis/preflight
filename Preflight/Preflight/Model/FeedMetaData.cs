namespace Accidis.Apps.Preflight.Model
{
	public sealed class FeedMetaData
	{
		readonly string _metarFile;
		readonly string _tafFile;

		public FeedMetaData( string name, string metar, string taf )
		{
			Name = name;
			_metarFile = metar;
			_tafFile = taf;
		}

		public string Name { get; private set; }

		public string MetarFileText
		{
			get { return _metarFile + ".txt"; }
		}

		public string TafFileText
		{
			get { return _tafFile + ".txt"; }
		}

		public string MetarFileHtml
		{
			get { return _metarFile + ".htm"; }
		}

		public string TafFileHtml
		{
			get { return _tafFile + ".htm"; }
		}
	}
}