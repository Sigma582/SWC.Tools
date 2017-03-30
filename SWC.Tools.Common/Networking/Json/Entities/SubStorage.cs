using System.Runtime.Serialization;

namespace SWC.Tools.Common.Networking.Json.Entities
{
    [DataContract]
    public class SubStorage
    {
        [DataMember(Name="champion")]
        public Champion Champion { get; set; }
    }
}