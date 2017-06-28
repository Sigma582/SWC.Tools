namespace SWC.Tools.Common.Networking.Exception
{
    public class StatusException : System.Exception
    {
        public int StatusCode { get; private set; }

        public StatusException(int statusCode)
        : base("The server returned Status code " + statusCode)
        {
            StatusCode = statusCode;
        }
    }
}