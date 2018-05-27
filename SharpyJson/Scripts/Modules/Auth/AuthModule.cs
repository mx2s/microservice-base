using SharpyJson.Scripts.Core;
using SharpyJson.Scripts.Modules.Response;

namespace SharpyJson.Scripts.Modules.Auth
{
    public class AuthModule
    {
        public static RequestResponse login(string login, string password) {
            return new RequestResponse(RequestTypes.Login, ReturnCodes.FailedInvalidLoginData);
        }
    }
}