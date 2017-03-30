using System;
using System.Runtime.Serialization;

namespace SWC.Tools.Common.Networking.Json.CommandArgs
{
    [DataContract]
    internal class PlayerLoginCommandArguments : CommandArguments
    {
        public PlayerLoginCommandArguments(string playerId)
        {
            PlayerId = playerId;
            Locale = "en_US";
            DeviceToken = "";
            DeviceType = "f";
            TimeZoneOffset = 0;
            ClientVersion = "4.7.0.2";
            Model = "20354 (LENOVO)";
            Os = "ws";
            OsVersion = "8.16.3.9600";
            Platform = "ws";
            SessionId = Guid.NewGuid().ToString(); //"d66d7a21-d635-4646-a7f8-a46ab957b54b";
            DeviceId = "UnknownPlayerId";
            DeviceIdType = "uid";
        }

        [DataMember(Name = "playerId")]
        public string PlayerId { get; set; }

        [DataMember(Name = "locale")]
        public string Locale { get; set; }

        [DataMember(Name = "deviceToken")]
        public string DeviceToken { get; set; }

        [DataMember(Name = "deviceType")]
        public string DeviceType { get; set; }

        [DataMember(Name = "timeZoneOffset")]
        public int TimeZoneOffset { get; set; }

        [DataMember(Name = "clientVersion")]
        public string ClientVersion { get; set; }

        [DataMember(Name = "model")]
        public string Model { get; set; }

        [DataMember(Name = "os")]
        public string Os { get; set; }

        [DataMember(Name = "osVersion")]
        public string OsVersion { get; set; }

        [DataMember(Name = "platform")]
        public string Platform { get; set; }

        [DataMember(Name = "sessionId")]
        public string SessionId { get; set; }

        [DataMember(Name = "deviceId")]
        public string DeviceId { get; set; }

        [DataMember(Name = "deviceIdType")]
        public string DeviceIdType { get; set; }

    }
}