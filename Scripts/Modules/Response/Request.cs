using Newtonsoft.Json.Linq;

namespace SharpyJson.Scripts.Core
{
    public class Request
    {
        public Request(RequestTypes requestType = RequestTypes.None, string token = "", JToken data = null) {
            RequestType = requestType;
            Token = token;
            Data = data;
        }

        public RequestTypes RequestType;
        public string Token;
        public JToken Data;
    }
}