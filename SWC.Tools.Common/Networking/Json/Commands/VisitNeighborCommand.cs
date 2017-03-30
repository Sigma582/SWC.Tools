using System.Runtime.Serialization;
using SWC.Tools.Common.Networking.Json.CommandArgs;

namespace SWC.Tools.Common.Networking.Json.Commands
{
    [DataContract]
    internal class VisitNeighborCommand : Command
    {
        public VisitNeighborCommand(string playerId, string neighborId)
        {
            Args = new VisitNeighborCommandArguments(playerId, neighborId);
        }

        protected override string GetAction()
        {
            return "player.neighbor.visit";
        }
    }
}