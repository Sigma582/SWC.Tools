using System.Collections.Generic;
using System.Runtime.Serialization;

namespace SWC.Tools.Common.Networking.Json.Entities
{
    [DataContract]
    public class Map
    {
        [DataMember(Name = "next")]
        public int Next { get; set; }

        [DataMember(Name = "planet")]
        public string Planet { get; set; }

        [DataMember(Name = "buildings")]
        public IList<Building> Buildings { get; set; }
    }
}