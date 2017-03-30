using System.Runtime.Serialization;

namespace SWC.Tools.Common.Networking.Json.Entities
{
    [DataContract]
    public class PlayerWrapper
    {
        [DataMember(Name="player")]
        public Player Player { get; set; }
    }
}