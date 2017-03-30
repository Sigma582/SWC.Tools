using System.Collections.Generic;
using System.Runtime.Serialization;

namespace SWC.Tools.Common.Networking.Json.Entities
{
    [DataContract]
    public class PlayerModel
    {
        [DataMember(Name="guildInfo")]
        public GuildInfo GuildInfo { get; set; }

        [DataMember(Name="upgrades")]
        public Upgrades Upgrades { get; set; }

        [DataMember(Name="donatedTroops")]
        public Dictionary<string, Dictionary<string, int>> DonatedTroops { get; set; }

        [DataMember(Name="buildingsUnderConstruction")]
        public IList<string> BuildingsUnderConstruction { get; set; }

        [DataMember(Name="map")]
        public Map Map { get; set; }

        [DataMember(Name="inventory")]
        public Inventory Inventory { get; set; }

        [DataMember(Name="faction")]
        public string Faction { get; set; }

        [DataMember(Name="protectedUntil")]
        public int? ProtectedUntil { get; set; }

        [DataMember(Name="protectionFrom")]
        public int? ProtectionFrom { get; set; }

        [DataMember(Name="timeZoneOffset")]
        public double TimeZoneOffset { get; set; }

        [DataMember(Name="lastPaymentTime")]
        public object LastPaymentTime { get; set; }

        [DataMember(Name="lastWarParticipationTime")]
        public int? LastWarParticipationTime { get; set; }

        [DataMember(Name="unlockedPlanets")]
        public IList<string> UnlockedPlanets { get; set; }

        [DataMember(Name="activeArmory")]
        public ActiveArmory ActiveArmory { get; set; }

        [DataMember(Name="shards")]
        public Dictionary<string, int> Shards { get; set; }
    }
}