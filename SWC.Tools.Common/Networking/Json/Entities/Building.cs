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

                if (value.EndsWith("10"))
                {
                    Level = 10;
                    Type = value.Substring(0, value.Length - 2);
                }
                else
                {
                    int level;
                    int.TryParse(value.Substring(value.Length - 1, 1), out level);
                    Level = level;
                    Type = value.Substring(0, value.Length - 1);
                }
            }
        }

        public string Type { get; set; }

        public int Level { get; set; }

        public string Faction { get; private set; }

        [DataMember(Name = "lastCollectTime")]
        public int? LastCollectTime { get; set; }

        [DataMember(Name = "currentStorage")]
        public int CurrentStorage { get; set; }

        public bool IsJunk
        {
            get { return JunkTypes.Any(t => Type == t); }
        }
    }
}