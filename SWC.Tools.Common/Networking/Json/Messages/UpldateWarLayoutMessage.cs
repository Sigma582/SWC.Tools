using System.Collections.Generic;
using System.Runtime.Serialization;
using SWC.Tools.Common.Networking.Json.CommandArgs;
using SWC.Tools.Common.Networking.Json.Commands;

namespace SWC.Tools.Common.Networking.Json.Messages
{
    [DataContract]
    internal class UpldateWarLayoutMessage : UpldateLayoutMessage
    {
        public UpldateWarLayoutMessage(string playerId, Dictionary<string, Position> positions)
        {
            Commands.Add(new UpdateWarLayoutCommand(playerId, positions));
        }
    }
}