using System.Linq;
using NUnit.Framework;
using SharpyJson.Scripts.Models.Microservice;
using SharpyJson.Scripts.Modules.Microservice;

namespace UnitTests.Test.Modules.Microservice
{
    [TestFixture]
    public class MicroservicesManagerTests
    {
        [Test]
        public void Test_AddService() {
            var testNode = MicroServiceNode.ListAll().First();

            var microserviceManager = MicroserviceManager.get();
            microserviceManager.AddService(testNode);

            var servicesList = microserviceManager.GetServices(MicroserviceTypes.Auth);
            
            Assert.True(servicesList.Count > 0);
        }
    }
}