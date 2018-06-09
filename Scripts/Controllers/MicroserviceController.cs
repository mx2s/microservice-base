using Newtonsoft.Json.Linq;
using SharpyJson.Scripts.Core;
using SharpyJson.Scripts.Modules.Response;

namespace SharpyJson.Scripts.Controllers
{
    public class MicroserviceController
    {
        public static RequestResponse Process(RawRequest rawRequest) {
            var requestType = RequestBuilder.GetRequestTypeFromRaw(rawRequest);

            switch (requestType) {
                case RequestTypes.GetServiceStatus:
                    var responseData = new JObject();
                    return new RequestResponse(RequestTypes.GetServiceStatus, ReturnCodes.Success, responseData);
            }

            return new RequestResponse(requestType, ReturnCodes.FailedEmptyResponse);
        }
    }
}