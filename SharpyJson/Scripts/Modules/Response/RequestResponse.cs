using System.Collections.Generic;
using Newtonsoft.Json;
using SharpyJson.Scripts.Core;

namespace SharpyJson.Scripts.Modules.Response
{
    public class RequestResponse
    {
        public RequestResponse(RequestTypes requestType, ReturnCodes returnCode, Dictionary<string, int> data = null) {
            RequestType = requestType;
            ReturnCode = returnCode;
            Data = data;
            if (data == null) {
                Data = new Dictionary<string, int>();
            }
        }
        public RequestTypes RequestType;
        public ReturnCodes ReturnCode;
        public Dictionary<string, int> Data;

        public string transform(RequestResponse response) {
            return JsonConvert.SerializeObject(response);
        }
    }
}