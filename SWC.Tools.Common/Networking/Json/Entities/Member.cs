using System.Collections.Generic;
using System.Runtime.Serialization;

namespace SWC.Tools.Common.Networking.Json.Entities
{
    [DataContract]
    public class Member
    {
        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "isOwner")]
        public string IsOwner { get; set; }

        [DataMember(Name = "isOfficer")]
        public string IsOfficer { get; set; }

        [DataMember(Name = "joinDate")]
        public string JoinDate { get; set; }

        [DataMember(Name = "troopsDonated")]
        public string TroopsDonated { get; set; }

        [DataMember(Name = "troopsReceived")]
        public string TroopsReceived { get; set; }

        [DataMember(Name = "rank")]
        public string Rank { get; set; }

        [DataMember(Name = "hqLevel")]
        public string HqLevel { get; set; }

        [DataMember(Name = "reputationInvested")]
        public string ReputationInvested { get; set; }

        [DataMember(Name = "xp")]
        public string Xp { get; set; }

        [DataMember(Name = "score")]
        public string Score { get; set; }

        [DataMember(Name = "warParty")]
        public string WarParty { get; set; }

        [DataMember(Name = "tournamentRating")]
        public int TournamentRating { get; set; }

        [DataMember(Name = "tournamentScores")]
        public Dictionary<string, int> TournamentScores { get; set; }

        [DataMember(Name = "attacksWon")]
        public string AttacksWon { get; set; }

        [DataMember(Name = "defensesWon")]
        public string DefensesWon { get; set; }

        [DataMember(Name = "planet")]
        public string Planet { get; set; }

        [DataMember(Name = "lastLoginTime")]
        public string LastLoginTime { get; set; }

        [DataMember(Name = "lastUpdated")]
        public string LastUpdated { get; set; }

        [DataMember(Name = "hasPlanetaryCommand")]
        public string HasPlanetaryCommand { get; set; }

        [DataMember(Name = "playerId")]
        public string PlayerId { get; set; }

    }
}