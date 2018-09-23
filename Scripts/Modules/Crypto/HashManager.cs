using System;
using System.Security.Cryptography;
using System.Text;

namespace SharpyJson.Scripts.Modules.Crypto
{
    public static class HashManager
    {
        public static string Encrypt(string input) {
            byte[] bytes = Encoding.Unicode.GetBytes(input);
            byte[] inArray = HashAlgorithm.Create("SHA1")?.ComputeHash(bytes);
            return Convert.ToBase64String(inArray ?? throw new Exception());
        }

        public static string GenerateToken(ushort min = 8, ushort max = 16) {
            return Guid.NewGuid().ToString("n").Substring(0, new Random().Next(min, max));
        }        
    }
}