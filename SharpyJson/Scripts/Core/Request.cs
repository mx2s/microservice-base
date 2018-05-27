using System.Collections.Generic;

namespace SharpyJson.Scripts.Core
{
    public class Request
    {
        public Request(RequestTypes requestType = RequestTypes.None, string token = "", Dictionary<string, int> data = null) {
            RequestType = requestType;
            Token = token;
            Data = data;
            if (data == null) {
                Data = new Dictionary<string, int>();
            } 
        }

        public RequestTypes RequestType;
        public string Token;
        public Dictionary<string, int> Data;
    }
}