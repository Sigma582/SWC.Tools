using System.Runtime.Serialization;
using SWC.Tools.Common.Networking.Json.CommandArgs;

namespace SWC.Tools.Common.Networking.Json.Commands
{
    [DataContract]
    [KnownType(typeof(GetAuthTokenCommand))]
    internal abstract class Command
    {
        private static int _requestCounter = 1;

        protected Command()
        {
            Action = GetAction();
//            Time = NeedsTime ? TimeHelper.GetTimestampSec() : 0;

            //todo make thread safe
            RequestId = _requestCounter++;
        }

        protected abstract string GetAction();

        public virtual bool NeedsTime { get { return false; } }

        [DataMember(Name = "action")]
        public string Action { get; set; }

        [DataMember(Name = "args")]
        public CommandArguments Args { get; set; }

        [DataMember(Name = "requestId")]
        public int RequestId { get; set; }

        [DataMember(Name = "time")]
        public double TimeSec { get; set; }

        /// <summary>
        /// Random GUID unique per command
        /// </summary>
        [DataMember(Name = "token")]
        public string Token { get; set; }
    }
}