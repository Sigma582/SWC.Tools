using System.Runtime.Serialization;

namespace SWC.Tools.Common.Networking.Json.CommandArgs
{
    [DataContract]
    internal class SearchSquadsCommandArguments : CommandArguments
    {
        public SearchSquadsCommandArguments(string playerId, string searchString)
        {
            PlayerId = playerId;
            SearchString = searchString;
        }

        [DataMember(Name = "playerId")]
        public string PlayerId { get; set; }

        [DataMember(Name = "searchTerm")]
        public string SearchString { get; set; }
    }
}