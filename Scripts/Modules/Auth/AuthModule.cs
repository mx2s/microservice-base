using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using Newtonsoft.Json.Linq;
using SharpyJson.Scripts.Core;
using SharpyJson.Scripts.Models;
using SharpyJson.Scripts.Modules.Crypto;
using SharpyJson.Scripts.Modules.Response;
using SharpyJson.Scripts.Modules.Validation;

namespace SharpyJson.Scripts.Modules.Auth
{
    public class AuthModule
    {
        public static RequestResponse Login(string login, string password) {
            int tokensLimit = 5;
            
            login = ValidationManager.OnlyStringsLettersDigitsSpaces(login);
            password = ValidationManager.OnlyStringsLettersDigitsSpaces(password);

            User user = User.FindByLogin(login);
            if (user == null) {
                return new RequestResponse(RequestTypes.Login, ReturnCodes.FailedUserNotExist);
            }

            if (user.password != HashManager.Encrypt(password)) {
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

            var data = new JObject();
            data["token"] = resultToken;
            
            return new RequestResponse(RequestTypes.Login, ReturnCodes.Success, data);
        }

        public static RequestResponse Logout(string token) {
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

        public static RequestResponse Register(string login, string password, string email) {
            login = ValidationManager.OnlyStringsLettersDigitsSpaces(login);
            password = ValidationManager.OnlyStringsLettersDigitsSpaces(password);
            email = IsValidEmail(email) ? email : null;

            JObject responseData = new JObject();
            if (User.FindByLogin(login) != null) {
                responseData["message"] = "user with this login already exist";
                return new RequestResponse(RequestTypes.Register, ReturnCodes.FailedUserAlreadyExist, responseData);
            }

            if (login.Length < 4) {
                responseData["message"] = "login can't be shorter than 4 symbols";               
                return new RequestResponse(RequestTypes.Register, ReturnCodes.FailedInvalidRegisterData, responseData);
            }
            
            if (password.Length < 4) {
                responseData["message"] = "password can't be shorter than 4 symbols";
                return new RequestResponse(RequestTypes.Register, ReturnCodes.FailedInvalidRegisterData, responseData);
            }
            
            User newUser = new User();
            newUser.login = login;
            newUser.password = HashManager.Encrypt(password);
            User.Create(newUser);
            
            return new RequestResponse(RequestTypes.Register, ReturnCodes.Success);
        }

        private static bool IsValidEmail(string email)
        {
            try {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch {
                return false;
            }
        }
    }
}