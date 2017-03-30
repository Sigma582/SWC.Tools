using System.Runtime.Serialization;

namespace SWC.Tools.Common.Networking.Json.Entities
{
    [DataContract]
    public class WarHistoryEntry
    {
        [DataMember(Name = "warId")]
        public string WarId { get; set; }

        [DataMember(Name = "endDate")]
        public double EndDate { get; set; }

        [DataMember(Name = "score")]
        public int Score { get; set; }

        [DataMember(Name = "opponentScore")]
        public int OpponentScore { get; set; }

        [DataMember(Name = "opponentGuildId")]
        public string OpponentGuildId { get; set; }

        [DataMember(Name = "opponentName")]
        public string OpponentName { get; set; }

        [DataMember(Name = "opponentIcon")]
        public string OpponentIcon { get; set; }
    }
}