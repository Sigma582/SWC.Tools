using System.Runtime.Serialization;
using SWC.Tools.Common.Networking.Json.CommandArgs;

namespace SWC.Tools.Common.Networking.Json.Commands
{
    [DataContract]
    internal class GetAuthTokenCommand : Command
    {
        public GetAuthTokenCommand(string playerId, string secret)
        {
            Args = new GetAuthTokenCommandArguments(playerId, secret);
        }

        protected override string GetAction()
        {
            return "auth.getAuthToken";
        }
    }
}