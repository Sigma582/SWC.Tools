using System.Runtime.Serialization;
using SWC.Tools.Common.Networking.Json.Commands;

namespace SWC.Tools.Common.Networking.Json.Messages
{
    [DataContract]
    internal class SearchSquadsMessage : Message
    {
        public SearchSquadsMessage(string playerId, string searchString)
        {
            Commands.Add(new SearchSquadsCommand(playerId, searchString));
        }

        public override bool NeedsTime { get { return true; } }
    }
}