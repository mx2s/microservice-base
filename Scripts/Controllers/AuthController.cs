using Newtonsoft.Json.Linq;
using SharpyJson.Scripts.Core;
using SharpyJson.Scripts.Core.Middleware;
using SharpyJson.Scripts.Modules.Auth;
using SharpyJson.Scripts.Modules.Request;
using SharpyJson.Scripts.Modules.Response;

namespace SharpyJson.Scripts.Controllers
{
    public class AuthController
    {
        public static RequestResponse Process(RawRequest rawRequest) {
            var requestType = RequestBuilder.GetRequestTypeFromRaw(rawRequest);
            
            switch (requestType) {
                case RequestTypes.Login:
                    return AuthModule.Login(
                        rawRequest.Data.SelectToken("login").Value<string>() ?? "",
                        rawRequest.Data.SelectToken("pass").Value<string>() ?? ""
                    );
                case RequestTypes.LogOut:
                    return AuthModule.Logout(
                        rawRequest.Data.SelectToken("token").Value<string>() ?? ""
                    );
                case RequestTypes.Register:
                    return AuthModule.Register(
                        rawRequest.Data.SelectToken("login").Value<string>() ?? "",
                        rawRequest.Data.SelectToken("pass").Value<string>() ?? "",
                        rawRequest.Data.SelectToken("email").Value<string>() ?? ""
                    );
            }

            var token = (string) rawRequest.Data["token"] ?? "";
            int userId = rawRequest.Data.Value<int?>("userId") ?? 0;
            
            var authMiddleware = AuthMiddleware.IsUserLoggedIn(
                token, userId
            );

            if (authMiddleware.Code != ReturnCodes.Success) {            
               return new RequestResponse(requestType, authMiddleware.Code);
            }

            authMiddleware = null;
            
            // Next request requires auth

            return null;
        }
    }
}