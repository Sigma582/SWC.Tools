using System.Collections.Generic;
using System.Runtime.Serialization;

namespace SWC.Tools.Common.Networking.Json.Entities
{
    [DataContract]
    public class Upgrades
    {
        [DataMember(Name = "troop")]
        public Dictionary<string, int> Troop { get; set; }

        [DataMember(Name = "specialAttack")]
        public Dictionary<string, int> SpecialAttack { get; set; }

        [DataMember(Name = "equipment")]
        public Dictionary<string, int> Equipment { get; set; }
    }
}