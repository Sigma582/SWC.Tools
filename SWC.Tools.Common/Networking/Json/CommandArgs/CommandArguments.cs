using System.Runtime.Serialization;

namespace SWC.Tools.Common.Networking.Json.CommandArgs
{
    [DataContract]
    internal class CommandArguments
    {
        [DataMember(Name = "playerId")]
        public string PlayerId { get; set; }

        public CommandArguments()
        {
            
        }
        
        public CommandArguments(string playerId) : this()
        {
            PlayerId = playerId;
        }
    }
}