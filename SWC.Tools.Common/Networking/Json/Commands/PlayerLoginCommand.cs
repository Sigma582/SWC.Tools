using System.Runtime.Serialization;
using SWC.Tools.Common.Networking.Json.CommandArgs;

namespace SWC.Tools.Common.Networking.Json.Commands
{
    [DataContract]
    internal class PlayerLoginCommand : Command
    {
        public PlayerLoginCommand(string playerId)
        {
            Args = new PlayerLoginCommandArguments(playerId);
        }

        protected override string GetAction()
        {
            return "player.login";
        }
    }
}