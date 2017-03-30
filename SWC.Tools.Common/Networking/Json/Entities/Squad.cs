using System.Runtime.Serialization;

namespace SWC.Tools.Common.Networking.Json.Entities
{
   [DataContract]
    public class Squad
    {
        [DataMember(Name = "nameLowerCase")]
        public string NameLowerCase { get; set; }

        [DataMember(Name = "members")]
        public int Members { get; set; }

        [DataMember(Name = "activeMemberCount")]
        public int ActiveMemberCount { get; set; }

        [DataMember(Name = "openEnrollment")]
        public string OpenEnrollment { get; set; }

        [DataMember(Name = "faction")]
        public string Faction { get; set; }

        [DataMember(Name = "created")]
        public int Created { get; set; }

        [DataMember(Name = "minScore")]
        public int MinScore { get; set; }

        [DataMember(Name = "icon")]
        public string Icon { get; set; }

        [DataMember(Name = "activityRatio")]
        public int ActivityRatio { get; set; }

        [DataMember(Name = "squadWarReadyCount")]
        public int SquadWarReadyCount { get; set; }

        [DataMember(Name = "currentWarId")]
        public string CurrentWarId { get; set; }

        [DataMember(Name = "level")]
        public int Level { get; set; }

        [DataMember(Name = "score")]
        public int Score { get; set; }

        [DataMember(Name = "rank")]
        public int Rank { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "_id")]
        public string Id { get; set; }

        [DataMember(Name = "activeMembers")]
        public int? ActiveMembers { get; set; }
    }
}