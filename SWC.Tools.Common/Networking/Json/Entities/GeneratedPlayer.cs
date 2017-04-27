using System.Runtime.Serialization;

namespace SWC.Tools.Common.Networking.Json.Entities
{
    [DataContract]
    public class GeneratedPlayer
    {
        [DataMember(Name = "playerId")]
        public string PlayerId { get; set; }

        [DataMember(Name = "secret")]
        public string Secret { get; set; }

    }
}