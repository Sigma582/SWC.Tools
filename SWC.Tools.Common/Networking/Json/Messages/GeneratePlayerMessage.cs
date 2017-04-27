using System.Runtime.Serialization;
using SWC.Tools.Common.Networking.Json.Commands;

namespace SWC.Tools.Common.Networking.Json.Messages
{
    [DataContract]
    internal class GeneratePlayerMessage : Message
    {
        public GeneratePlayerMessage()
        {
            Commands.Add(new GeneratePlayerCommand());
        }
    }
}