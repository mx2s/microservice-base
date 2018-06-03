using NUnit.Framework;
using SharpyJson.Scripts.Controllers;
using SharpyJson.Scripts.Core;
using SharpyJson.Scripts.Modules.Processor;

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
    }
}