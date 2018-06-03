using SharpyJson.Scripts.Core;
using SharpyJson.Scripts.Models;
using SharpyJson.Scripts.Modules.Crypto;
using SharpyJson.Scripts.Modules.Response;
using SharpyJson.Scripts.Modules.Validation;

namespace SharpyJson.Scripts.Modules.Auth
{
    public class AuthModule
    {
        public static RequestResponse login(string login, string password) {
            login = ValidationManager.OnlyStringsLettersDigitsSpaces(login);
            password = ValidationManager.OnlyStringsLettersDigitsSpaces(password);
            string hashedPasword = HashManager.Encrypt(password);
            
            User user = User.FindByLogin(login);
            if (user == null) {                
                return new RequestResponse(RequestTypes.Login, ReturnCodes.FailedUserNotExist);
            }

            if (user.Password != hashedPasword) {
                return new RequestResponse(RequestTypes.Login, ReturnCodes.FailedInvalidLoginData);
            }
            
            return new RequestResponse(RequestTypes.Login, ReturnCodes.Success);
        }
    }
}