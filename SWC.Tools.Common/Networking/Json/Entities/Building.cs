using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace SWC.Tools.Common.Networking.Json.Entities
{
    [DataContract]
    public class Building
    {
        private static readonly string[] JunkTypes =
            {"rockSmall", "rockMedium", "rockLarge", "junkSmall", "junkMedium", "junkLarge"};

        private string _uid;

        public const string FACTORY = "Factory";
        public const string BARRACKS = "Barracks";

        /// <summary>
        /// Building unique ID, e.g. "bld_62"
        /// </summary>
        [DataMember(Name = "key")]
        public string Key { get; set; }

        [DataMember(Name = "x")]
        public int X { get; set; }

        [DataMember(Name = "z")]
        public int Z { get; set; }

        /// <summary>
        /// Composite building type, e.g. "empireWall8"
        /// </summary>
        [DataMember(Name = "uid")]
        public string Uid
        {
            get { return _uid; }
            set
            {
                _uid = value;
                if (value.StartsWith("empire"))
                {
                    Faction = "empire";
                    value = value.Replace("empire", "");
                }
                else
                {
                    Faction = "rebel";
                    value = value.Replace("rebel", "");
                }

                int level;
                int.TryParse(value.Substring(value.Length - 2, 2), out level);
                if(level > 0)
                {
                    Level = level;
                    Type = value.Substring(0, value.Length - 2);
                }
                else
                {
                    Level = int.Parse(value.Substring(value.Length - 1, 1));
                    Type = value.Substring(0, value.Length - 1);
                }
            }
        }

        public string Type { get; private set; }

        public int Level { get; private set; }

        public string Faction { get; private set; }

        //these are useless

        //[DataMember(Name = "lastCollectTime")]
        //public int? LastCollectTime { get; set; }

        //[DataMember(Name = "currentStorage")]
        //public int CurrentStorage { get; set; }

        public bool IsJunk
        {
            get { return JunkTypes.Any(t => Type == t); }
        }

        public bool IsReplaceable(Building b)
        {
            return _replaceableTypes.ContainsKey(Type)
                && _replaceableTypes[Type].Contains(b.Type);
        }

        private static Dictionary<string, List<string>> _replaceableTypes = new Dictionary<string, List<string>>
        {
            //4x4
            {"PlatformDroideka", new List<string>{ "PlatformHeavyDroideka" } },
            {"PlatformHeavyDroideka", new List<string>{ "PlatformDroideka" } },
            
            {"Armory", new List<string>{ "TacticalCommand", "FleetCommand" } },
            {"TacticalCommand", new List<string>{ "Armory", "FleetCommand" } },
            {"FleetCommand", new List<string>{ "TacticalCommand", "Armory" } },
            
            //3x3
            {"Barracks", new List<string>{
                "ContrabandCantina", "NavigationCenter", "OffenseLab", "ScoutTower",
                "CreditGenerator", "MaterialsGenerator", "ContrabandGenerator",
                "MaterialsStorage", "CreditStorage", "ContrabandStorage"
            } },
            {"ContrabandCantina", new List<string>{
                "Barracks", "NavigationCenter", "OffenseLab", "ScoutTower",
                "CreditGenerator", "MaterialsGenerator", "ContrabandGenerator",
                "MaterialsStorage", "CreditStorage", "ContrabandStorage"
            } },
            {"NavigationCenter", new List<string>{
                "ContrabandCantina", "Barracks", "OffenseLab", "ScoutTower",
                "CreditGenerator", "MaterialsGenerator", "ContrabandGenerator",
                "MaterialsStorage", "CreditStorage", "ContrabandStorage"
            } },
            {"OffenseLab", new List<string>{
                "ContrabandCantina", "NavigationCenter", "Barracks", "ScoutTower",
                "CreditGenerator", "MaterialsGenerator", "ContrabandGenerator",
                "MaterialsStorage", "CreditStorage", "ContrabandStorage"
            } },
            {"ScoutTower", new List<string>{
                "ContrabandCantina", "NavigationCenter", "OffenseLab", "Barracks",
                "CreditGenerator", "MaterialsGenerator", "ContrabandGenerator",
                "MaterialsStorage", "CreditStorage", "ContrabandStorage"
            } },
            
            {"ContrabandGenerator", new List<string>{
                "CreditGenerator", "MaterialsGenerator",
                "Barracks", "ContrabandCantina", "NavigationCenter", "OffenseLab", "ScoutTower",
                "MaterialsStorage" , "CreditStorage", "ContrabandStorage"
            } },
            {"CreditGenerator", new List<string>{
                "ContrabandGenerator", "MaterialsGenerator" ,
                "Barracks", "ContrabandCantina", "NavigationCenter", "OffenseLab", "ScoutTower",
                "MaterialsStorage" , "CreditStorage", "ContrabandStorage"
            } },
            {"MaterialsGenerator", new List<string>{
                "CreditGenerator", "ContrabandGenerator" ,
                "Barracks", "ContrabandCantina", "NavigationCenter", "OffenseLab", "ScoutTower",
                "MaterialsStorage" , "CreditStorage", "ContrabandStorage"
            } },
            
            {"ContrabandStorage", new List<string>{
                "CreditStorage", "MaterialsStorage",
                "CreditGenerator", "MaterialsGenerator", "ContrabandGenerator",
                "Barracks", "ContrabandCantina", "NavigationCenter", "OffenseLab", "ScoutTower",
            } },
            {"CreditStorage", new List<string>{
                "ContrabandStorage", "MaterialsStorage",
                "CreditGenerator", "MaterialsGenerator", "ContrabandGenerator",
                "Barracks", "ContrabandCantina", "NavigationCenter", "OffenseLab", "ScoutTower",
            } },
            {"MaterialsStorage", new List<string>{
                "CreditStorage", "ContrabandStorage",
                "CreditGenerator", "MaterialsGenerator", "ContrabandGenerator",
                "Barracks", "ContrabandCantina", "NavigationCenter", "OffenseLab", "ScoutTower",
            } },

            //2x2
            {"BurstTurret", new List<string>{ "RapidFireTurret", "RocketTurret", "Mortar", "SonicTurret" } },
            {"Mortar", new List<string>{ "SonicTurret", "RapidFireTurret", "RocketTurret", "BurstTurret" } },
            {"RapidFireTurret", new List<string>{ "BurstTurret", "RocketTurret", "Mortar", "SonicTurret" } },
            {"RocketTurret", new List<string>{ "RapidFireTurret", "BurstTurret", "Mortar", "SonicTurret" } },
            {"SonicTurret", new List<string>{ "Mortar", "RapidFireTurret", "RocketTurret", "BurstTurret" } },

            //traps
            {"TrapDropship", new List<string>{ "TrapStrikeAOE", "TrapStrikeGeneric", "TrapStrikeHeavy" } },
            {"TrapStrikeAOE", new List<string>{ "TrapDropship", "TrapStrikeGeneric", "TrapStrikeHeavy" } },
            {"TrapStrikeGeneric", new List<string>{ "TrapStrikeAOE", "TrapDropship", "TrapStrikeHeavy" } },
            {"TrapStrikeHeavy", new List<string>{ "TrapStrikeAOE", "TrapStrikeGeneric", "TrapDropship" } },
        };
    }
}