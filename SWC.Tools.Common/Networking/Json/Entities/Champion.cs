using System.Collections.Generic;
using System.Runtime.Serialization;

namespace SWC.Tools.Common.Networking.Json.Entities
{
    [DataContract]
    public class Champion
    {
        [DataMember(Name = "storage")]
        public Dictionary<string, object> Storage { get; set; }
    }

    [DataContract]
    public class ChampionStorage
    {
        [DataMember(Name="troopChampionEmpireDroideka9")]
        public Dummy Deka { get; set; }
    }
}