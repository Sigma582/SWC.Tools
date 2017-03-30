using System;
using System.IO;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using System.Text;
using SWC.Tools.Common.Util;

namespace SWC.Tools.Common.Networking.Json.CommandArgs
{
    [DataContract]
    internal class GetAuthTokenCommandArguments : CommandArguments
    {
        public GetAuthTokenCommandArguments(string playerId, string secret)
        {
            PlayerId = playerId;
            RequestToken = GenerateToken(playerId, secret);
        }

        private static string GenerateToken(string playerId, string secret)
        {
            var timestamp = TimeHelper.GetTimestampMs()+1000;
            var str = String.Format("{{\"userId\":\"{0}\",\"expires\":{1}}}", playerId, timestamp);

            string token;

            using (var stream = new MemoryStream(Encoding.UTF8.GetBytes(str)))
            {
                using (var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(secret)))
                {
                    var hash = hmac.ComputeHash(stream);
                    var hashStr = BitConverter.ToString(hash);

                    token = string.Format("{0}.{1}", hashStr.Replace("-", ""), str);
                }
            }

            return Convert.ToBase64String(Encoding.UTF8.GetBytes(token));
        }

        [DataMember(Name = "playerId")]
        public string PlayerId { get; set; }

        [DataMember(Name = "requestToken")]
        public string RequestToken { get; set; }

    }
}