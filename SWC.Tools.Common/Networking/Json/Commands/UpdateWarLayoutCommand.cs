using System.Collections.Generic;
using System.Runtime.Serialization;
using SWC.Tools.Common.Networking.Json.CommandArgs;

namespace SWC.Tools.Common.Networking.Json.Commands
{
    [DataContract]
    internal class UpdateWarLayoutCommand : Command
    {
        public UpdateWarLayoutCommand(string playerId, Dictionary<string, Position> positions)
        {
            Args = new UpldateLayoutCommandArguments(playerId, positions);
        }

        protected override string GetAction()
        {
            return "guild.war.base.save";
        }

//        public override bool NeedsTime { get { return true; } }
    }
}