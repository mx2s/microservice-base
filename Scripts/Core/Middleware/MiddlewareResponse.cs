using Newtonsoft.Json.Linq;

namespace SharpyJson.Scripts.Core.Middleware
{
    public class MiddlewareResponse
    {
        public ReturnCodes Code;
        public JObject Data;

        public MiddlewareResponse(ReturnCodes setCode, JObject setData = null) {
            Code = setCode;
            Data = setData ?? new JObject();
        }
    }
}