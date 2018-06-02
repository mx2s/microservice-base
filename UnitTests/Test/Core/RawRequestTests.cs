using Newtonsoft.Json.Linq;
using NUnit.Framework;
using SharpyJson.Scripts.Core;

namespace UnitTests.Test.Core
{
    [TestFixture]
    public class RawRequestTests
    {
        [Test]
        public void Test_RawRequestBuilder() {
            string json = @"{
                'token': 'daf12f12',
                'type': 2,
                'data': {
                    'login': 'admin',
                    'pass': '1234'
                }
            }";
            var newRequest = RawRequest.BuildFromString(json);
            Assert.True(newRequest.Token == "daf12f12");
            Assert.True(newRequest.RequestType == 2);
            Assert.True(newRequest.Data.SelectToken("login").Value<string>() == "admin");
        }
    }
}