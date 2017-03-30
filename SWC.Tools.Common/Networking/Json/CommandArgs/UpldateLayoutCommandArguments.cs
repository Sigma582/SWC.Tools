using System.Collections.Generic;
using System.Runtime.Serialization;

namespace SWC.Tools.Common.Networking.Json.CommandArgs
{
    [DataContract]
    internal class UpldateLayoutCommandArguments : CommandArguments
    {
        public UpldateLayoutCommandArguments(string playerId, Dictionary<string, Position> positions)
        {
            PlayerId = playerId;
            Positions = positions;
//            Credits = resourses.Credits;
//            Materials = resourses.Materials;
//            Contraband = resourses.Contraband;
//            Crystals = resourses.Crystals;
        }

        [DataMember(Name = "playerId")]
        public string PlayerId { get; set; }

        /// <summary>
        /// wtf?
        /// </summary>
//        [DataMember(Name = "cs")]
//        public int Cs { get; set; }
//
//        [DataMember(Name = "_credits")]
//        public int Credits { get; set; }
//
//        [DataMember(Name = "_materials")]
//        public int Materials { get; set; }
//
//        [DataMember(Name = "_contraband")]
//        public int Contraband { get; set; }
//
//        [DataMember(Name = "_crystals")]
//        public int Crystals { get; set; }

        [DataMember(Name = "positions")]
        public Dictionary<string, Position> Positions { get; set; }
    }
}