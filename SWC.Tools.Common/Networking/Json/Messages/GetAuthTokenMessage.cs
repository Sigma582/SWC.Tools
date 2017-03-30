using System.Runtime.Serialization;
using SWC.Tools.Common.Networking.Json.Commands;

namespace SWC.Tools.Common.Networking.Json.Messages
{
    [DataContract]
    internal class GetAuthTokenMessage : Message
    {
        public GetAuthTokenMessage(string playerId, string secret)
        {
            Commands.Add(new GetAuthTokenCommand(playerId, secret));
        }
    }
}