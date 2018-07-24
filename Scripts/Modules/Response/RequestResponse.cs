using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SharpyJson.Scripts.Core;

namespace SharpyJson.Scripts.Modules.Response
{
    public class RequestResponse
    {
        public RequestTypes RequestType;
        public ReturnCodes ReturnCode;
        public JToken Data;
        
        public RequestResponse(RequestTypes requestType, ReturnCodes returnCode, JToken data = null) {
            RequestType = requestType;
            ReturnCode = returnCode;
            Data = data;
        }
        
        public static RequestResponse BuildFromString(string data) {
            JObject schema = JObject.Parse(data);

            int typeInt = schema["type"].Value<int>();
            int typeCode = schema["code"].Value<int>();
            
            if (!Enum.IsDefined(typeof(RequestTypes), typeInt)) {
                typeInt = 0;
            }
            
            if (!Enum.IsDefined(typeof(ReturnCodes), typeCode)) {
                typeCode = 0;
            }
            
            var result = new RequestResponse(
                (RequestTypes) typeInt,
                (ReturnCodes) typeCode,
                schema["data"]
            );
            return result;
        }

        public string Transform() {
            var response = new JObject();
            response["type"] = (int) RequestType;
            response["code"] = (int) ReturnCode;
            response["data"] = Data;
            return JsonConvert.SerializeObject(response);
        }
    }
}