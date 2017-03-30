using System.Collections.Generic;
using System.Runtime.Serialization;
using SWC.Tools.Common.Networking.Json.Commands;

namespace SWC.Tools.Common.Networking.Json.Messages
{
    [DataContract]
    [KnownType(typeof(GetAuthTokenMessage))]
    internal abstract class Message
    {
        protected Message()
        {
            AuthKey = string.Empty;
            PickupMessages = true;
//            LastLoginTime = NeedsTime ? TimeHelper.LastLoginTimeSec : 0;
            Commands = new List<Command>();
        }

        public virtual bool NeedsTime { get { return false; } }

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