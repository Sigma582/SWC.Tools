using System.Runtime.Serialization;
using SWC.Tools.Common.Networking.Json.Commands;

namespace SWC.Tools.Common.Networking.Json.Messages
{
    [DataContract]
    internal class VisitNeighborMessage : Message
    {
        public VisitNeighborMessage(string playerId, string neighborId)
        {
            Commands.Add(new VisitNeighborCommand(playerId, neighborId));
        }
    }
}