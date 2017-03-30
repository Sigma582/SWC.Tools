using System.Runtime.Serialization;
using SWC.Tools.Common.Networking.Json.Commands;

namespace SWC.Tools.Common.Networking.Json.Messages
{
    [DataContract]
    internal class GetWarParticipantMessage : Message
    {
        public GetWarParticipantMessage(string playerId)
        {
            Commands.Add(new GetWarParticipantCommand(playerId));
        }

        public override bool NeedsTime => true;
    }
}