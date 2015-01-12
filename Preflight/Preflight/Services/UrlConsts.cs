using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accidis.Apps.Preflight.Services
{
    /// <remarks>
    /// Last checked valid 2014-12-21.
    /// </remarks>
    public static class UrlConsts
    {
        /// <summary>
        /// Legacy text feeds for METAR, TAF and SIGMET.
        /// </summary>
        /// <remarks>
        /// These are no longer publically linked from the LFV/AROWeb site
        /// since the latest redesign. Probably only being kept around as an
        /// integration point. Still working and up to date as of 2014-12-21.
        /// Can't use for LHP since swedish chars are b0rked.
        /// </remarks>
        public const string MetInfo = "http://www.lfv.se/MetInfo.asp?T=&Frequency=120";

        /// <summary>
        /// Appends to MetInfo for each file to get.
        /// </summary>
        public const string MetInfoFileSuffix = "&TextFile={0}&SubTitle=";

        /// <summary>
        /// Filename for the SIGMET table, for use with MetInfo.
        /// </summary>
        public const string SigMetEurope = "sigmettable.htm";

        /// <summary>
        /// Text feeds for LHP and SIGMET.
        /// </summary>
        /// <remarks>
        /// The public links on the new site since the latest redesign.
        /// </remarks>
        public const string TorLink = "https://aro.lfv.se/Links/Link/ViewLink?TorLinkId={0}&type={1}";

        /// <summary>
        /// Type parameter for TorLink for MET feeds.
        /// </summary>
        public const string TorTypeMet = "MET";

        /// <summary>
        /// Type parameter for TorLink for AIS feeds.
        /// </summary>
        public const string TorTypeAis = "AIS";
    }
}
