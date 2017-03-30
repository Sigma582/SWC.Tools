namespace SWC.Tools.Common.Networking.Exception
{
    public class StatusException : System.Exception
    {
        public int StatusCode { get; }

        public StatusException(int statusCode)
        : base("The server returned Status code " + statusCode)
        {
            StatusCode = statusCode;
        }
    }
}