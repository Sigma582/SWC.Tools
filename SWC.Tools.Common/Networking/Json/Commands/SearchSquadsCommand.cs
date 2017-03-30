using System.Runtime.Serialization;
using SWC.Tools.Common.Networking.Json.CommandArgs;

namespace SWC.Tools.Common.Networking.Json.Commands
{
    [DataContract]
    internal class SearchSquadsCommand : Command
    {
        public SearchSquadsCommand(string playerId, string searchString)
        {
            Args = new SearchSquadsCommandArguments(playerId, searchString);
        }

        //public override bool NeedsTime {get { return true; }}

        protected override string GetAction()
        {
            return "guild.search.byName";
        }
    }
}