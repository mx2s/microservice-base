using NUnit.Framework;
using SharpyJson.Scripts.Modules.Validation;

namespace UnitTests.Test.Modules.Validation
{
    [TestFixture]
    public class ValidationManagerTests
    {
        [Test]
        public void Test_RemovingSpecialCharacters() {
            string strToValidate = ValidationManager.OnlyStringsLettersDigitsSpaces("text'{/");
            Assert.True(strToValidate == "text");
        }
    }
}