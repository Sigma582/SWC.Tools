using System.Runtime.Serialization;
using SWC.Tools.Common.Networking.Json.CommandArgs;

namespace SWC.Tools.Common.Networking.Json.Commands
{
    [DataContract]
    internal class GetWarParticipantCommand : Command
    {
        public GetWarParticipantCommand(string playerId)
        {
            Args = new CommandArguments(playerId);
        }

        protected override string GetAction()
        {
            return "guild.war.getParticipant";
        }

        public override bool NeedsTime { get { return true; } }
    }
}