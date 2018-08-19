using Newtonsoft.Json.Linq;
using SharpyJson.Scripts.Core;

namespace SharpyJson.Scripts.Modules.Middleware
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