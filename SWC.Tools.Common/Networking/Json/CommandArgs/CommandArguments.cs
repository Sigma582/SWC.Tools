using System.Runtime.Serialization;

namespace SWC.Tools.Common.Networking.Json.CommandArgs
{
    [DataContract]
    [KnownType(typeof(GetAuthTokenCommandArguments))]
    internal abstract class CommandArguments
    {
    }
}