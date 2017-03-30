using System.Collections.Generic;
using System.Runtime.Serialization;

namespace SWC.Tools.Common.Networking.Json.Entities
{
    [DataContract]
    public class ActiveArmory
    {
        [DataMember(Name="equipment")]
        public IList<string> Equipment { get; set; }

        [DataMember(Name="capacity")]
        public int? Capacity { get; set; }
    }
}