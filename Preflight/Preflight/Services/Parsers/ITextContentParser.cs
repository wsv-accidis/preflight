using System;
using System.Collections.Generic;

namespace Accidis.Apps.Preflight.Services.Parsers
{
	public interface ITextContentParser
	{
        void ParseResponse( string response, List<string> results );
	}
}