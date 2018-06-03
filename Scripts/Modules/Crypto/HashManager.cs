using System;
using System.Security.Cryptography;
using System.Text;

namespace SharpyJson.Scripts.Modules.Crypto
{
    public class HashManager
    {
        public static string Encrypt(string input) {
            byte[] bytes = Encoding.Unicode.GetBytes(input);
            byte[] inArray = HashAlgorithm.Create("SHA1")?.ComputeHash(bytes);
            return Convert.ToBase64String(inArray);
        }}
}