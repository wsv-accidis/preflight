using Accidis.Apps.Preflight.Model;
using System.Collections.Generic;

namespace Accidis.Apps.Preflight.Services
{
    public static class Texts
    {
        public static readonly TextMetaData LlfAreaASwe = new TextMetaData( "LLfAreaASwe", 307, UrlConsts.TorTypeMet );
        public static readonly TextMetaData LlfAreaBSwe = new TextMetaData( "LLfAreaBSwe", 308, UrlConsts.TorTypeMet );
        public static readonly TextMetaData LlfAreaCSwe = new TextMetaData( "LlfAreaCSwe", 309, UrlConsts.TorTypeMet );
        public static readonly TextMetaData LlfAreaESwe = new TextMetaData( "LlfAreaESwe", 310, UrlConsts.TorTypeMet );

        public static readonly TextMetaData NotamSwedenVfr = new TextMetaData( "NotamSwedenVfr", 162, UrlConsts.TorTypeAis );
        public static readonly TextMetaData NotamSwedenIfr = new TextMetaData( "NotamSwedenIfr", 159, UrlConsts.TorTypeAis );
        public static readonly TextMetaData NotamSwedenFourDay = new TextMetaData( "NotamSwedenFourDay", 161, UrlConsts.TorTypeAis );
        public static readonly TextMetaData NotamSwedenHeli = new TextMetaData( "NotamSwedenHeli", 290, UrlConsts.TorTypeAis );
        public static readonly TextMetaData NotamNorway = new TextMetaData( "NotamNorway", 163, UrlConsts.TorTypeAis );
        public static readonly TextMetaData NotamFinland = new TextMetaData( "NotamFinland", 164, UrlConsts.TorTypeAis );
        public static readonly TextMetaData NotamDenmark = new TextMetaData( "NotamDenmark", 160, UrlConsts.TorTypeAis );
        public static readonly TextMetaData NotamEstonia = new TextMetaData( "NotamEstonia", 166, UrlConsts.TorTypeAis );
        public static readonly TextMetaData NotamLatvia = new TextMetaData( "NotamLatvia", 167, UrlConsts.TorTypeAis );
        public static readonly TextMetaData NotamLithuania = new TextMetaData( "NotamLithuania", 168, UrlConsts.TorTypeAis );
        public static readonly TextMetaData NotamPoland = new TextMetaData( "NotamPoland", 169, UrlConsts.TorTypeAis );
        public static readonly TextMetaData NotamGermany = new TextMetaData( "NotamGermany", 170, UrlConsts.TorTypeAis );

        private static IEnumerable<TextMetaData> _all;

        public static IEnumerable<TextMetaData> All
        {
            get
            {
                return _all ?? ( _all = new[]
                                           {
                                               LlfAreaASwe,
                                               LlfAreaBSwe,
                                               LlfAreaCSwe,
                                               LlfAreaESwe,
                                               NotamSwedenVfr,
                                               NotamSwedenIfr,
                                               NotamSwedenFourDay,
                                               NotamSwedenHeli,
                                               NotamNorway,
                                               NotamFinland,
                                               NotamDenmark,
                                               NotamEstonia,
                                               NotamLatvia,
                                               NotamLithuania,
                                               NotamPoland,
                                               NotamGermany
                                           } );
            }
        }
    }
}