using System.Runtime.Serialization;

namespace SWC.Tools.Common.Networking.Json.CommandArgs
{
    [DataContract]
    public class Position
    {
        public Position()
        {

        }

        public Position(int x, int z)
        {
            X = x;
            Z = z;
        }

        [DataMember(Name = "x")]
        public int X { get; set; }

        [DataMember(Name = "z")]
        public int Z { get; set; }
    }
}