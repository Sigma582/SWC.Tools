using System.Runtime.Serialization;

namespace SWC.Tools.Common.Networking.Json.CommandArgs
{
    [DataContract]
    internal class GetSquadDetailsCommandArguments : CommandArguments
    {
        public GetSquadDetailsCommandArguments(string playerId, string squadId)
        {
            PlayerId = playerId;
            SquadId = squadId;
        }

        [DataMember(Name = "guildId")]
        public string SquadId { get; set; }
    }
}