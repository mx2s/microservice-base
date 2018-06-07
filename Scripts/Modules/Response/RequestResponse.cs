using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SharpyJson.Scripts.Core;

namespace SharpyJson.Scripts.Modules.Response
{
    public class RequestResponse
    {
        public RequestResponse(RequestTypes requestType, ReturnCodes returnCode, JObject data = null) {
            RequestType = requestType;
            ReturnCode = returnCode;
            Data = data ?? new JObject();
        }
        
        public RequestTypes RequestType;
        public ReturnCodes ReturnCode;
        public JObject Data;

        public string transform(RequestResponse response) {
            return JsonConvert.SerializeObject(response);
        }
    }
}