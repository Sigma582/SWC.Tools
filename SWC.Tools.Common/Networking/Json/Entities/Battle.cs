using System.Collections.Generic;
using System.Runtime.Serialization;

namespace SWC.Tools.Common.Networking.Json.Entities
{
    [DataContract]
    public class Battle
    {
        [DataMember(Name = "battleId")]
        public string BattleId { get; set; }

        [DataMember(Name = "attacker")]
        public BattleParticipant Attacker { get; set; }

        [DataMember(Name = "defender")]
        public BattleParticipant Defender { get; set; }

        [DataMember(Name = "missionId")]
        public string MissionId { get; set; }

        [DataMember(Name = "attackDate")]
        public int AttackDate { get; set; }

        [DataMember(Name = "looted")]
        public Loot Looted { get; set; }

        [DataMember(Name = "earned")]
        public Loot Earned { get; set; }

        [DataMember(Name = "maxLootable")]
        public Loot MaxLootable { get; set; }

        [DataMember(Name = "troopsExpended")]
        public Dictionary<string, int> TroopsExpended { get; set; }

        [DataMember(Name = "attackerGuildTroopsExpended")]
        public Dictionary<string, int> AttackerGuildTroopsExpended { get; set; }

        [DataMember(Name = "defenderGuildTroopsExpended")]
        public Dictionary<string, int> DefenderGuildTroopsExpended { get; set; }

        [DataMember(Name = "baseDamagePercent")]
        public int BaseDamagePercent { get; set; }

        [DataMember(Name = "stars")]
        public int Stars { get; set; }

        [DataMember(Name = "manifestVersion")]
        public int ManifestVersion { get; set; }

        [DataMember(Name = "potentialMedalGain")]
        public int PotentialMedalGain { get; set; }

        [DataMember(Name = "defenderPotentialMedalGain")]
        public int DefenderPotentialMedalGain { get; set; }

        [DataMember(Name = "revenged")]
        public bool Revenged { get; set; }

        [DataMember(Name = "battleVersion")]
        public string BattleVersion { get; set; }

        [DataMember(Name = "cmsVersion")]
        public string CmsVersion { get; set; }

        [DataMember(Name = "server")]
        public bool Server { get; set; }

        [DataMember(Name = "attackerEquipment")]
        public List<string> AttackerEquipment { get; set; }

        [DataMember(Name = "defenderEquipment")]
        public List<string> DefenderEquipment { get; set; }

        [DataMember(Name = "planetId")]
        public string PlanetId { get; set; }
    }
}