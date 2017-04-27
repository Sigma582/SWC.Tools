using System.Runtime.Serialization;

namespace SWC.Tools.Common.Networking.Json.Entities
{
    [DataContract]
    public class BattleParticipant
    {
        [DataMember(Name = "playerId")]
        public string PlayerId { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "faction")]
        public string Faction { get; set; }

        [DataMember(Name = "guildId")]
        public string GuildId { get; set; }

        [DataMember(Name = "guildName")]
        public string GuildName { get; set; }

        [DataMember(Name = "attackRating")]
        public string AttackRating { get; set; }

        [DataMember(Name = "attackRatingDelta")]
        public string AttackRatingDelta { get; set; }

        [DataMember(Name = "defenseRating")]
        public string DefenseRating { get; set; }

        [DataMember(Name = "defenseRatingDelta")]
        public string DefenseRatingDelta { get; set; }

        [DataMember(Name = "tournamentRating")]
        public string TournamentRating { get; set; }

        [DataMember(Name = "tournamentRatingDelta")]
        public string TournamentRatingDelta { get; set; }
    }
}