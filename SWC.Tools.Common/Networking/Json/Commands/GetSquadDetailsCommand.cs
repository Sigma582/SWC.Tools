using System.Runtime.Serialization;
using SWC.Tools.Common.Networking.Json.CommandArgs;

namespace SWC.Tools.Common.Networking.Json.Commands
{
    [DataContract]
    internal class GetSquadDetailsCommand : Command
    {
        public GetSquadDetailsCommand(string playerId, string squadId)
        {
            Args = new GetSquadDetailsCommandArguments(playerId, squadId);
        }

        //public override bool NeedsTime { get { return true; } }

        protected override string GetAction()
        {
            return "guild.get.public";
        }
    }
}