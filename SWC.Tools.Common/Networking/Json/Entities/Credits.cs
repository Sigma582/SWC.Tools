using System.Runtime.Serialization;

namespace SWC.Tools.Common.Networking.Json.Entities
{
    [DataContract]
    public class Credits
    {

        [DataMember(Name="amount")]
        public int Amount { get; set; }

        [DataMember(Name="capacity")]
        public int Capacity { get; set; }

        [DataMember(Name="scale")]
        public int Scale { get; set; }
    }
}