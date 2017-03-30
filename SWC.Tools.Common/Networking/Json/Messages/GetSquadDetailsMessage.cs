using System.Runtime.Serialization;
using SWC.Tools.Common.Networking.Json.Commands;

namespace SWC.Tools.Common.Networking.Json.Messages
{
    [DataContract]
    internal class GetSquadDetailsMessage : Message
    {
        public GetSquadDetailsMessage(string playerId, string squadId)
        {
            Commands.Add(new GetSquadDetailsCommand(playerId, squadId));
        }

        public override bool NeedsTime { get { return true; } }
    }
}