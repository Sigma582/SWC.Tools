using System.Collections.Generic;
using System.Runtime.Serialization;

namespace SWC.Tools.Common.Networking.Json.Entities
{
    [DataContract]
    public class Player
    {
        [DataMember(Name="playerId")]
        public string PlayerId { get; set; }

        [DataMember(Name="locale")]
        public string Locale { get; set; }

        [DataMember(Name="playerModel")]
        public PlayerModel PlayerModel { get; set; }

        [DataMember(Name="created")]
        public int Created { get; set; }

        [DataMember(Name="scalars")]
        public Scalars Scalars { get; set; }

        [DataMember(Name = "liveness")]
        public Liveness Liveness { get; set; }

        [DataMember(Name="name")]
        public string Name { get; set; }
    }
}
