using System.Runtime.Serialization;

namespace SWC.Tools.Common.Networking.Json.Entities
{
    public class Loot
    {
        [DataMember(Name = "contraband")]
        public string Contraband { get; set; }

        [DataMember(Name = "credits")]
        public string Credits { get; set; }

        [DataMember(Name = "materials")]
        public string Materials { get; set; }
    }
}