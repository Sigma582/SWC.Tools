using System.Runtime.Serialization;

namespace SWC.Tools.Common.Networking.Json.Entities
{
    [DataContract]
    public class Storage
    {
        [DataMember(Name="credits")]
        public Credits Credits { get; set; }

        [DataMember(Name="materials")]
        public Materials Materials { get; set; }

        [DataMember(Name="contraband")]
        public Contraband Contraband { get; set; }

        [DataMember(Name="reputation")]
        public Reputation Reputation { get; set; }

        [DataMember(Name="crystals")]
        public Crystals Crystals { get; set; }

        [DataMember(Name="droids")]
        public Droids Droids { get; set; }

        [DataMember(Name="xp")]
        public Xp Xp { get; set; }
    }
}