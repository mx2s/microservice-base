using System.Linq;
using NUnit.Framework;
using SharpyJson.Scripts.Controllers;
using SharpyJson.Scripts.Core;
using SharpyJson.Scripts.Models;
using SharpyJson.Scripts.Modules.Processor;
using SharpyJson.Scripts.Modules.Response;

namespace UnitTests.Test.Controllers
{
    [TestFixture]
    public class AuthControllerTests
    {
        [Test]
        public void Test_EmptyRequest() {
            var response = AuthController.Process(new RawRequest());
            Assert.True(response.ReturnCode == ReturnCodes.FailedEmptyResponse);
        }

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
            
            var updatedToken = AccessToken.ListAll().First();
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
    }
}