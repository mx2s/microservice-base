using System.Linq;
using SharpyJson.Scripts.Models;
using SharpyJson.Scripts.Modules.Middleware;

namespace SharpyJson.Scripts.Core.Middleware
{
    public class AuthMiddleware
    {
        public static MiddlewareResponse IsUserLoggedIn(string token, int userId) {
            var accessTokens = AccessToken.GetListByUserId(userId);

            if (accessTokens.All(item => item.token != token)) {
                return new MiddlewareResponse(ReturnCodes.LoginFailed);
            }

            return new MiddlewareResponse(ReturnCodes.Success);
        }
    }
}