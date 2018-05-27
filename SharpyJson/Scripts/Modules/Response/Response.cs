using Newtonsoft.Json;
using SharpyJson.Scripts.Core;

namespace SharpyJson.Scripts.Modules.Response
{
    public class Response
    {
        public Response(RequestTypes requestType, ReturnCodes returnCode, string data) {
            RequestType = requestType;
            ReturnCode = returnCode;
            data = data;
        }
        public RequestTypes RequestType;
        public ReturnCodes ReturnCode;
        public string Data;

        public string transform(Response response) {
            return JsonConvert.SerializeObject(response);
        }
    }
}