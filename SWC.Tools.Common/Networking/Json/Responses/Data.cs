using System.Runtime.Serialization;

namespace SWC.Tools.Common.Networking.Json.Responses
{
    [DataContract]
    internal class Data<TResult>
    {
        [DataMember(Name = "requestId")]
        public int RequestId { get; set; }

        [DataMember(Name = "result")]
        public TResult Result { get; set; }

//        [DataMember(Name = "messages")]
//        public string Messages { get; set; }

        [DataMember(Name = "status")]
        public int Status { get; set; }

    }
}