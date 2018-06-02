using Newtonsoft.Json.Linq;
using SharpyJson.Scripts.Core;
using SharpyJson.Scripts.Modules.Auth;
using SharpyJson.Scripts.Modules.Response;

namespace SharpyJson.Scripts.Controllers
{
    public class AuthController
    {
        public static RequestResponse Process(RawRequest rawRequest) {           
            var request = RequestBuilder.Build(rawRequest);

            switch (request.RequestType) {
                case RequestTypes.Login:
                    return AuthModule.login(
                        request.Data.SelectToken("login").Value<string>(), 
                        request.Data.SelectToken("pass").Value<string>()
                    );
                case RequestTypes.LogOut:
                    break;
                default:
                    return new RequestResponse(request.RequestType, ReturnCodes.FailedWrongRequestType);
            }

            return new RequestResponse(request.RequestType, ReturnCodes.FailedEmptyResponse);
        }
    }
}