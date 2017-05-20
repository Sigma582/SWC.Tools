using System.Collections.Generic;
using System.Runtime.Serialization;

namespace SWC.Tools.Common.Networking.Json.CommandArgs
{
    [DataContract]
    internal class UpldateLayoutCommandArguments : CommandArguments
    {
        public UpldateLayoutCommandArguments(string playerId, Dictionary<string, Position> positions)
        {
            PlayerId = playerId;
            Positions = positions;
        }

        [DataMember(Name = "positions")]
        public Dictionary<string, Position> Positions { get; set; }
    }
}