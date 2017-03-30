using System.Collections.Generic;
using System.Runtime.Serialization;
using SWC.Tools.Common.Networking.Json.CommandArgs;

namespace SWC.Tools.Common.Networking.Json.Commands
{
    [DataContract]
    internal class UpldateLayoutCommand : Command
    {
        public UpldateLayoutCommand(string playerId, Dictionary<string, Position> positions)
        {
            Args = new UpldateLayoutCommandArguments(playerId, positions);
        }

        protected override string GetAction()
        {
            return "player.building.multimove";
        }

//        public override bool NeedsTime { get { return true; } }
    }
}