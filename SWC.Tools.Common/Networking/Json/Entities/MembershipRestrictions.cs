using System.Runtime.Serialization;

namespace SWC.Tools.Common.Networking.Json.Entities
{
    [DataContract]
    public class MembershipRestrictions
    {
        [DataMember(Name = "openEnrollment")]
        public bool OpenEnrollment { get; set; }

        [DataMember(Name = "maxSize")]
        public int MaxSize { get; set; }

        [DataMember(Name = "minScoreAtEnrollment")]
        public int MinScoreAtEnrollment { get; set; }

        [DataMember(Name = "faction")]
        public string Faction { get; set; }
    }
}