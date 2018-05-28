using System;
using SharpyJson.Scripts.Core;
using SharpyJson.Scripts.Modules.Auth;
using SharpyJson.Scripts.Modules.Response;

namespace SharpyJson.Scripts.Controllers
{
    public class AuthController
    {
        public static RequestResponse process(Request request) {
            Console.WriteLine("processing auth request");

            switch (request.RequestType) {
                case RequestTypes.Login:
                    return AuthModule.login("", "");
                    break;
            }

            return new RequestResponse(request.RequestType, ReturnCodes.FailedEmptyResponse);
        }
    }
}