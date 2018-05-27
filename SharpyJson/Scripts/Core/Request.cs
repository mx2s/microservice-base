namespace SharpyJson.Scripts.Core
{
    public class Request
    {
        public Request(RequestTypes requestType = RequestTypes.None, string token = "") {
            RequestType = requestType;
            Token = token;
        }

        public RequestTypes RequestType;
        public string Token;
    }
}