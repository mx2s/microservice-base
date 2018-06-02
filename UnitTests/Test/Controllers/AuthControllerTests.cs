using System;
using NUnit.Framework;
using SharpyJson.Scripts.Controllers;
using SharpyJson.Scripts.Core;

namespace UnitTests.Test.Controllers
{
    [TestFixture]
    public class AuthControllerTests
    {
        [Test]
        public void Test_EmptyRequest() {
            var response = AuthController.Process(new RawRequest());

            Assert.True(response.ReturnCode == ReturnCodes.FailedWrongRequestType);
        }
    }
}