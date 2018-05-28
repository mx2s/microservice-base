using System;
using SharpyJson.Scripts.Core;
using SharpyJson.Scripts.Modules.Response;

namespace SharpyJson.Scripts.Modules.Processor
{
    public class RequestProcessor
    {
        private static RequestProcessor instance;
        
        private RequestProcessor() {}

        public static RequestProcessor get() {
            if (instance == null) {
                instance = new RequestProcessor();
            }

            return instance;
        }

        public RequestResponse process(Request request) {
            if (request.RequestType == RequestTypes.None) {
                return new RequestResponse(RequestTypes.None, ReturnCodes.FailedWrongRequestType);
            }

            Console.WriteLine((int) request.RequestType);
            
            return new RequestResponse(request.RequestType, ReturnCodes.FailedEmptyResponse);
        }
    }
}