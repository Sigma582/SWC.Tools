using System;
using System.IO;
using System.Linq;
using System.Text;

namespace SWC.Tools.Common.AuthData
{
    public static class AuthDataProvider
    {
        public static AuthData Get(string fileName)
        {
            var bytes = File.ReadAllBytes(fileName);
            var str = Encoding.UTF8.GetString(bytes);
            var startByte = str.IndexOf("prefPlayerId", StringComparison.InvariantCulture) + 16;

            if (bytes[startByte] == 0)
            {
                startByte++;
            }

            var playerIdBytes = bytes.Skip(startByte).Take(36).ToArray();
            var playerSecretBytes = bytes.Skip(startByte + 61).Take(32).ToArray();

            return new AuthData(Encoding.UTF8.GetString(playerIdBytes), Encoding.UTF8.GetString(playerSecretBytes));
        }
    }

    public class AuthData
    {
        public AuthData(string playerId, string playerSecret)
        {
            PlayerId = playerId;
            PlayerSecret = playerSecret;
        }

        public string PlayerId { get; set; }
        public string PlayerSecret { get; set; }
    }
}