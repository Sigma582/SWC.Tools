using System.Runtime.Serialization;

namespace SWC.Tools.Common.Networking.Json.CommandArgs
{
    [DataContract]
    internal class GeneratePlayerCommandArguments : CommandArguments
    {
        public GeneratePlayerCommandArguments()
        {
            Locale = "en_US";
        }

        [DataMember(Name = "locale")]
        public string Locale { get; set; }
    }
}