using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
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
            int tokensLimit = 5;
            
            login = ValidationManager.OnlyStringsLettersDigitsSpaces(login);
            password = ValidationManager.OnlyStringsLettersDigitsSpaces(password);
            
            string hashedPasword = HashManager.Encrypt(password);

            User user = User.FindByLogin(login);
            if (user == null) {
                return new RequestResponse(RequestTypes.Login, ReturnCodes.FailedUserNotExist);
            }

            if (user.password != hashedPasword) {
                return new RequestResponse(RequestTypes.Login, ReturnCodes.FailedInvalidLoginData);
            }
           
            int tokensAmount = AccessToken.UserTokensCount(user.id);
            string resultToken = HashManager.GenerateToken();

            if (tokensAmount >= tokensLimit) {
                var userTokens = AccessToken.GetListByUserId(user.id);
                var updatedToken = userTokens[new Random().Next(userTokens.Count)];
                updatedToken.token = resultToken;
                updatedToken.Save();
                userTokens = null;
            }
            else {
                var newToken = new AccessToken();
                newToken.user_id = user.id;
                newToken.token = resultToken;
                AccessToken.Create(newToken);
            }

            var data = new Dictionary<string, string>();
            data.Add("token", resultToken);
            
            return new RequestResponse(RequestTypes.Login, ReturnCodes.Success, data);
        }

        public static RequestResponse logout(string token) {
            var dbToken = AccessToken.FindByToken(
                ValidationManager.OnlyStringsLettersDigitsSpaces(token)
            );
            if (dbToken == null) {
                return new RequestResponse(RequestTypes.LogOut, ReturnCodes.FailedNotFound);
            }

            dbToken.token = HashManager.GenerateToken();
            dbToken.Save();
            
            return new RequestResponse(RequestTypes.LogOut, ReturnCodes.Success);
        }
    }
}