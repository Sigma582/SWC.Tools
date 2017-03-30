using System.Runtime.Serialization;

namespace SWC.Tools.Common.Networking.Json.Entities
{
    [DataContract]
    public class GuildInfo
    {
        [DataMember(Name = "guildId")]
        public string GuildId { get; set; }

        [DataMember(Name = "guildName")]
        public string GuildName { get; set; }

        [DataMember(Name = "icon")]
        public string Icon { get; set; }

        [DataMember(Name = "joinDate")]
        public int JoinDate { get; set; }

        [DataMember(Name = "playerHasOutstandingJoinRequest")]
        public bool PlayerHasOutstandingJoinRequest { get; set; }
    }
}