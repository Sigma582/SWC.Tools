using System.Runtime.Serialization;

namespace SWC.Tools.Common.Networking.Json.CommandArgs
{
    [DataContract]
    internal class GetWarParticipantCommandArguments : CommandArguments
    {
        public GetWarParticipantCommandArguments(string playerId)
        {
            PlayerId = playerId;
        }

        [DataMember(Name = "playerId")]
        public string PlayerId { get; set; }
    }
}