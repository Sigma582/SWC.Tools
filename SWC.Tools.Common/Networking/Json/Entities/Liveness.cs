using System.Runtime.Serialization;

namespace SWC.Tools.Common.Networking.Json.Entities
{
    [DataContract]
    public class Liveness
    {
        [DataMember(Name = "installDate")]
        public int InstallDate { get; set; }

        [DataMember(Name = "commandCount")]
        public int CommandCount { get; set; }

        [DataMember(Name = "sessionCountChecked")]
        public int SessionCountChecked { get; set; }

        [DataMember(Name = "sessionCountToday")]
        public int SessionCountToday { get; set; }

        [DataMember(Name = "lastLoginTime")]
        public int LastLoginTime { get; set; }

        [DataMember(Name = "lastTestImpressionBiLogTime")]
        public int LastTestImpressionBiLogTime { get; set; }

        [DataMember(Name = "consecutiveLiveDays")]
        public int ConsecutiveLiveDays { get; set; }

        [DataMember(Name = "keepAliveTime")]
        public int KeepAliveTime { get; set; }

    }
}