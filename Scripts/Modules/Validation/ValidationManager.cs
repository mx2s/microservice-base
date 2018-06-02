using System.Text.RegularExpressions;

namespace SharpyJson.Scripts.Modules.Validation
{
    public class ValidationManager
    {
        public static string OnlyStringsLettersDigitsSpaces(string data) {
            return new Regex(@"[^a-zA-Z0-9\s]").Replace(data, "");
        }
    }
}