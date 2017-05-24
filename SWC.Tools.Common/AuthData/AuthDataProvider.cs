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
			String idKey = "prefPlayerId$";
			String pwKey = "prefPlayerSecret ";

			var bytes = File.ReadAllBytes(fileName);
			var str = Encoding.ASCII.GetString(bytes);

			var startByte = str.IndexOf(idKey, StringComparison.InvariantCulture) + idKey.Length + 3;

			var playerIdBytes = bytes.Skip(startByte).Take(36).ToArray();

			startByte = str.IndexOf(pwKey, StringComparison.InvariantCulture) + pwKey.Length + 3;

			var playerSecretBytes = bytes.Skip(startByte).Take(32).ToArray();

			return new AuthData(Encoding.ASCII.GetString(playerIdBytes), Encoding.ASCII.GetString(playerSecretBytes));

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
