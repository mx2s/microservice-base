using SharpyJson.Scripts.Controllers;
using SharpyJson.Scripts.Modules.Request;
using SharpyJson.Scripts.Modules.Response;

namespace SharpyJson.Scripts.Core
{
    public class RequestProcessor
    {
        public static RequestResponse Process(RawRequest rawRequest) {   
            var requestType = RequestBuilder.GetRequestTypeFromRaw(rawRequest);
            
            if (requestType == RequestTypes.None) {
                return new RequestResponse(RequestTypes.None, ReturnCodes.FailedWrongRequestType);
            }

            int intRequestType = (int) requestType;
            
            // Auth (1 - 99)
            if (intRequestType >= 1 && intRequestType < 100) {
                return AuthController.Process(rawRequest);
            }
            
            return new RequestResponse(requestType, ReturnCodes.FailedEmptyResponse);
        }
    }
}