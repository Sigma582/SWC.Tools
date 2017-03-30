using System.Collections.Generic;
using System.Runtime.Serialization;

namespace SWC.Tools.Common.Networking.Json.Responses
{
    [DataContract]
    internal class Response<TResult>
    {
        [DataMember(Name = "protocolVersion")]
        public int ProtocolVersion { get; set; }

        [DataMember(Name = "data")]
        public List<Data<TResult>> Data { get; set; }

        [DataMember(Name = "serverTime")]
        public string ServerTime { get; set; }

        [DataMember(Name = "serverTimestamp")]
        public int ServerTimestamp { get; set; }

    }
}