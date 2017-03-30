using System.Runtime.Serialization;

namespace SWC.Tools.Common.Networking.Json.Entities
{
    [DataContract]
    public class Scalars
    {

        [DataMember(Name="attacksLost")]
        public int AttacksLost { get; set; }

        [DataMember(Name="attacksWon")]
        public int AttacksWon { get; set; }

        [DataMember(Name="defensesLost")]
        public int DefensesLost { get; set; }

        [DataMember(Name="defensesWon")]
        public int DefensesWon { get; set; }

        [DataMember(Name="attacksStarted")]
        public int AttacksStarted { get; set; }

        [DataMember(Name="attacksCompleted")]
        public int AttacksCompleted { get; set; }

        [DataMember(Name="attackRating")]
        public int AttackRating { get; set; }

        [DataMember(Name="defenseRating")]
        public int DefenseRating { get; set; }

        [DataMember(Name="xp")]
        public int Xp { get; set; }

        [DataMember(Name="softCash")]
        public int SoftCash { get; set; }

        [DataMember(Name="NF1219")]
        public int Nf1219 { get; set; }
    }
}