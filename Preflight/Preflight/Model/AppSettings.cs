using System;
using System.Collections.Generic;
using System.Diagnostics;
using Accidis.Apps.Preflight.Services;
using Accidis.Apps.Preflight.Utils.IO;
using ProtoBuf;

namespace Accidis.Apps.Preflight.Model
{
	[ProtoContract]
	public sealed class AppSettings
	{
		const string _storeName = "AppSettings";

		public AppSettings()
		{
			PinnedAirports = new List<string>();
			SelectedFeeds = new List<string>( new[] { Feeds.Default } );
		}

		[ProtoMember( 1 )]
		public IList<string> PinnedAirports { get; set; }

		[ProtoMember( 2 )]
		public IList<string> SelectedFeeds { get; set; }

		[ProtoMember( 3 )]
		public bool UseSwedishFeeds { get; set; }

		public event EventHandler Refresh;

		public void TriggerRefresh()
		{
			Debug.WriteLine( "AppSettings: Triggering a refresh." );
			if( null != Refresh )
				Refresh( this, EventArgs.Empty );
		}

		public static void Persist( AppSettings settings )
		{
			ProtoBufFileStorage<AppSettings>.Persist( settings, _storeName );
		}

		public static bool TryLoad( out AppSettings settings )
		{
			return ProtoBufFileStorage<AppSettings>.TryLoad( out settings, _storeName );
		}
	}
}