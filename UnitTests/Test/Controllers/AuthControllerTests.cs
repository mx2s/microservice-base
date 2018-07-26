using System;
using System.Linq;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using SharpyJson.Scripts.Core;
using SharpyJson.Scripts.Models;
using SharpyJson.Scripts.Modules.Request;

namespace UnitTests.Test.Controllers
{
    [TestFixture]
    public class AuthControllerTests
    {
        [Test]
        public void Test_LoginRequest() {
            var rawRequest = RawRequest.BuildFromString(@"{
                'type': 1,
                'data': {
                    'login': 'testuser',
                    'pass': '1234'
                }
            }");
            var response = RequestProcessor.Process(rawRequest);
            Assert.True(response.RequestType == RequestTypes.Login);
            Assert.True(response.ReturnCode == ReturnCodes.Success);
        }
        
        [Test]
        public void Test_LoginRequestWrongPassword() {
            var rawRequest = RawRequest.BuildFromString(@"{
                'type': 1,
                'data': {
                    'login': 'testuser',
                    'pass': '12'
                }
            }");
            var response = RequestProcessor.Process(rawRequest);
            Assert.True(response.RequestType == RequestTypes.Login);
            Assert.True(response.ReturnCode == ReturnCodes.FailedInvalidLoginData);
        }
        
        [Test]
        public void Test_LogoutRequest() {
            var token = AccessToken.ListAll().First();
            string prevToken = token.token;

            string json = @"{
                'type': 2,
                'data': {
                    'token': '{0}',
                }
            }".Replace("{0}", token.token);
            
            var rawRequest = RawRequest.BuildFromString(json);
            var response = RequestProcessor.Process(rawRequest);           
            Assert.True(response.RequestType == RequestTypes.LogOut);
            Assert.True(response.ReturnCode == ReturnCodes.Success);

            var updatedToken = AccessToken.Find(token.id);
            Assert.True(updatedToken.token != prevToken);
        }
        
        [Test]
        public void Test_LogoutRequestEmptyToken() {
            string json = @"{
                'type': 2,
                'data': {
                    'token': 'someInvalidToken',
                }
            }";
            
            var rawRequest = RawRequest.BuildFromString(json);
            var response = RequestProcessor.Process(rawRequest);           
            Assert.True(response.RequestType == RequestTypes.LogOut);
            Assert.True(response.ReturnCode == ReturnCodes.FailedNotFound);
        }
        
        [Test]
        public void Test_Register() {
            // TODO: allow some special symbols like '_'
            
            var login = "tempuser" + Guid.NewGuid().ToString("n").Substring(0, new Random().Next(2, 5));
            var password = "1234";
            
            string json = @"{
                'type': 3,
                'data': {
                    'login': '{login}',
                    'pass': '{password}',
                    'email': 'someemail'
                }
            }"
                .Replace("{login}", login)
                .Replace("{password}", password);
            
            var rawRequest = RawRequest.BuildFromString(json);
            var response = RequestProcessor.Process(rawRequest);
            Assert.True(response.RequestType == RequestTypes.Register);
            Assert.True(response.ReturnCode == ReturnCodes.Success);
            var foundUser = User.FindByLogin(login);
            Assert.True(foundUser != null);
        }
        
        [Test]
        public void Test_RegisterShortLogin() {
            var login = "tmp";
            var password = "1234";
            
            string json = @"{
                'type': 3,
                'data': {
                    'login': '{login}',
                    'pass': '{password}',
                    'email': 'someemail'
                }
            }"
                .Replace("{login}", login)
                .Replace("{password}", password);
            
            var rawRequest = RawRequest.BuildFromString(json);
            var response = RequestProcessor.Process(rawRequest);
            var responseMessage = response.Data["message"].Value<string>();
            
            Assert.True(response.RequestType == RequestTypes.Register);
            Assert.True(response.ReturnCode == ReturnCodes.FailedInvalidRegisterData);
            Assert.True(responseMessage.Contains("shorter than"));
            Assert.True(responseMessage.Contains("login"));
        }
    }
}