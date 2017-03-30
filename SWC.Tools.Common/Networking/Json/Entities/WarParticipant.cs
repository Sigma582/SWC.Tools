using System.Collections.Generic;
using System.Runtime.Serialization;

namespace SWC.Tools.Common.Networking.Json.Entities
{
    [DataContract]
    public class WarParticipant
    {
        [DataMember(Name = "warMap")]
        public Map WarMap { get; set; }

        [DataMember(Name = "champions")]
        public Dictionary<string, int> Champions { get; set; }

        [DataMember(Name = "donatedTroops")]
        public Dictionary<string, Dictionary<string, int>> DonatedTroops { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "turns")]
        public int Turns { get; set; }

        [DataMember(Name = "victoryPoints")]
        public int VictoryPoints { get; set; }

        [DataMember(Name = "score")]
        public int Score { get; set; }

        [DataMember(Name = "level")]
        public int Level { get; set; }

        [DataMember(Name = "currentlyDefending")]
        public string CurrentlyDefending { get; set; }

        [DataMember(Name = "scoutingStatus")]
        public string ScoutingStatus { get; set; }

        [DataMember(Name = "id")]
        public string Id { get; set; }
    }
}