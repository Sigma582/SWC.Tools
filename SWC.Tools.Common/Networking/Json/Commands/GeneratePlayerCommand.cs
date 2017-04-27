using System.Runtime.Serialization;
using SWC.Tools.Common.Networking.Json.CommandArgs;

namespace SWC.Tools.Common.Networking.Json.Commands
{
    [DataContract]
    internal class GeneratePlayerCommand : Command
    {
        public GeneratePlayerCommand()
        {
            Args = new GeneratePlayerCommandArguments();
        }

        protected override string GetAction()
        {
            return "auth.preauth.generatePlayer";
        }
    }
}