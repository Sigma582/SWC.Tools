using System.Collections.Generic;
using System.Runtime.Serialization;

namespace SWC.Tools.Common.Networking.Json.Entities
{
    [DataContract]
    public class Perks
    {
        [DataMember(Name = "available")]
        public Dictionary<string, string> Available { get; set; }

        [DataMember(Name = "inProgress")]
        public Dictionary<string, int> InProgress { get; set; }
    }
}