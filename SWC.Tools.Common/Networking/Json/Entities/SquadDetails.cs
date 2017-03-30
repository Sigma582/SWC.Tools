using System.Collections.Generic;
using System.Runtime.Serialization;

namespace SWC.Tools.Common.Networking.Json.Entities
{
    [DataContract]
    public class SquadDetails
    {
        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "icon")]
        public string Icon { get; set; }

        [DataMember(Name = "description")]
        public string Description { get; set; }

        [DataMember(Name = "membershipRestrictions")]
        public MembershipRestrictions MembershipRestrictions { get; set; }

        [DataMember(Name = "members")]
        public IList<Member> Members { get; set; }

        [DataMember(Name = "created")]
        public int Created { get; set; }

        [DataMember(Name = "highestRankAchieved")]
        public int? HighestRankAchieved { get; set; }

        [DataMember(Name = "activeMemberCount")]
        public int ActiveMemberCount { get; set; }

        [DataMember(Name = "squadWarReadyCount")]
        public int SquadWarReadyCount { get; set; }

        [DataMember(Name = "currentWarId")]
        public string CurrentWarId { get; set; }

        [DataMember(Name = "warSignUpTime")]
        public int? WarSignUpTime { get; set; }

        [DataMember(Name = "warRating")]
        public int? WarRating { get; set; }

        [DataMember(Name = "isSameFactionWarAllowed")]
        public bool? IsSameFactionWarAllowed { get; set; }

        [DataMember(Name = "score")]
        public int Score { get; set; }

        [DataMember(Name = "rank")]
        public int Rank { get; set; }

        [DataMember(Name = "id")]
        public string Id { get; set; }

        [DataMember(Name = "memberCount")]
        public int MemberCount { get; set; }

        [DataMember(Name = "warHistory")]
        public List<WarHistoryEntry> WarHistory { get; set; }

        [DataMember(Name = "level")]
        public int Level { get; set; }

        [DataMember(Name = "totalRepInvested")]
        public int TotalRepInvested { get; set; }

        [DataMember(Name = "perks")]
        public Perks Perks { get; set; }

        [DataMember(Name = "lastPerkNotif")]
        public int LastPerkNotif { get; set; }
    }
}