using SharpyJson.Scripts.Controllers;
using SharpyJson.Scripts.Core;
using SharpyJson.Scripts.Modules.Response;

namespace SharpyJson.Scripts.Modules.Processor
{
    public class RequestProcessor
    {
        public static RequestResponse Process(RawRequest rawRequest) {   
            var requestType = RequestBuilder.GetRequestTypeFromRaw(rawRequest);
            
            if (requestType == RequestTypes.None) {
                return new RequestResponse(RequestTypes.None, ReturnCodes.FailedWrongRequestType);
            }

            int intRequestType = (int) requestType;
            
            // AUTH
            if (intRequestType >= 1 && intRequestType < 100) {
                return AuthController.Process(rawRequest);
            }
            
            return new RequestResponse(requestType, ReturnCodes.FailedEmptyResponse);
        }
    }
}