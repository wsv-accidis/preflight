using ProtoBuf;
using System.Collections.Generic;
using Accidis.Apps.Preflight.Utils.IO;

namespace Accidis.Apps.Preflight.Model
{
	[ProtoContract]
	public sealed class Feed
	{
		const string _storeName = "AirportWeatherFeed";

		public Feed()
		{
			Airports = new List<string>();
			TafList = new Dictionary<string, string>();
			MetarList = new Dictionary<string, string>();
            SigMetList = new Dictionary<string, IList<string>>();
        }

		[ProtoMember( 1 )]
		public IList<string> Airports { get; set; }

		[ProtoMember( 2 )]
		public IDictionary<string, string> TafList { get; set; }

		[ProtoMember( 3 )]
		public IDictionary<string, string> MetarList { get; set; }

        [ProtoMember( 4 )]
        public IDictionary<string, IList<string>> SigMetList { get; set; }

		public bool HasContent
		{
            get { return TafList.Count > 0 || MetarList.Count > 0 || SigMetList.Count > 0; }
		}

		public static void Persist( Feed feed )
		{
			ProtoBufFileStorage<Feed>.Persist( feed, _storeName );
		}

		public static bool TryLoad( out Feed feed )
		{
			return ProtoBufFileStorage<Feed>.TryLoad( out feed, _storeName );
		}
	}
}