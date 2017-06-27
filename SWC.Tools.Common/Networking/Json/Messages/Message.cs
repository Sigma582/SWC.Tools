using System.Collections.Generic;
using System.Runtime.Serialization;
using SWC.Tools.Common.Networking.Json.Commands;

namespace SWC.Tools.Common.Networking.Json.Messages
{
    [DataContract]
    internal class Message
    {
        public Message(bool needsTime, params Command[] commands) : this(commands)
        {
            NeedsTime = needsTime;
        }

        public Message(params Command[] commands) : this()
        {
            if (commands != null)
            {
                Commands.AddRange(commands);
            }
        }

        protected Message()
        {
            AuthKey = string.Empty;
            PickupMessages = true;
            Commands = new List<Command>();
        }

        public bool NeedsTime { get; set; }

        [DataMember(Name = "authKey")]
        public string AuthKey { get; set; }

        [DataMember(Name = "pickupMessages")]
        public bool PickupMessages { get; set; }

        [DataMember(Name = "lastLoginTime")]
        public double LastLoginTime { get; set; }

        [DataMember(Name = "commands")]
        public List<Command> Commands { get; private set; }

    }
}