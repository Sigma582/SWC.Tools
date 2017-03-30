using System.Runtime.Serialization;
using SWC.Tools.Common.Networking.Json.Commands;

namespace SWC.Tools.Common.Networking.Json.Messages
{
    [DataContract]
    internal class PlayerLoginMessage : Message
    {
        public PlayerLoginMessage(string playerId, string authToken)
        {
            AuthKey = authToken;
            Commands.Add(new PlayerLoginCommand(playerId));
        }
    }
}