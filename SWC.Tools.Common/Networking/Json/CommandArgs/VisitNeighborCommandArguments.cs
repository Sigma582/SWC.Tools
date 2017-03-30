using System.Runtime.Serialization;

namespace SWC.Tools.Common.Networking.Json.CommandArgs
{
    [DataContract]
    internal class VisitNeighborCommandArguments : CommandArguments
    {
        public VisitNeighborCommandArguments(string playerId, string neighborId)
        {
            PlayerId = playerId;
            NeighborId = neighborId;
        }

        [DataMember(Name = "playerId")]
        public string PlayerId { get; set; }

        [DataMember(Name = "neighborId")]
        public string NeighborId { get; set; }
    }
}