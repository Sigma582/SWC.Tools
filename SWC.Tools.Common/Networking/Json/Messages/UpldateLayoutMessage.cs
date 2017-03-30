using System.Collections.Generic;
using System.Runtime.Serialization;
using SWC.Tools.Common.Networking.Json.CommandArgs;
using SWC.Tools.Common.Networking.Json.Commands;

namespace SWC.Tools.Common.Networking.Json.Messages
{
    [DataContract]
    internal class UpldateLayoutMessage : Message
    {
        public UpldateLayoutMessage(string playerId, Dictionary<string, Position> positions)
        {
            Commands.Add(new UpldateLayoutCommand(playerId, positions));
        }

        protected UpldateLayoutMessage()
        {
            
        }

        public override bool NeedsTime { get { return true; } }
    }
}