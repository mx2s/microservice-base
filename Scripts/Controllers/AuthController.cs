using Newtonsoft.Json.Linq;
using SharpyJson.Scripts.Core;
using SharpyJson.Scripts.Modules.Auth;
using SharpyJson.Scripts.Modules.Response;

namespace SharpyJson.Scripts.Controllers
{
    public class AuthController
    {
        public static RequestResponse Process(RawRequest rawRequest) {
            RequestTypes requestType = RequestBuilder.GetRequestTypeFromRaw(rawRequest);

            switch (requestType) {
                case RequestTypes.Login:
                    return AuthModule.login(
                        rawRequest.Data.SelectToken("login").Value<string>(),
                        rawRequest.Data.SelectToken("pass").Value<string>()
                    );
                case RequestTypes.LogOut:
                    return AuthModule.logout(
                        rawRequest.Data.SelectToken("token").Value<string>()
                    );
            }

            return new RequestResponse(requestType, ReturnCodes.FailedEmptyResponse);
        }
    }
}