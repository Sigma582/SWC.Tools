using System.Runtime.Serialization;

namespace SWC.Tools.Common.Networking.Json.Entities
{
    [DataContract]
    public class Inventory
    {
        [DataMember(Name="storage")]
        public Storage Storage { get; set; }

        [DataMember(Name="capacity")]
        public int Capacity { get; set; }

        [DataMember(Name="subStorage")]
        public SubStorage SubStorage { get; set; }
    }
}