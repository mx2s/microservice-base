using SharpyJson.Scripts.Models;
using SharpyJson.Scripts.Modules.Crypto;

namespace UnitTests.Utils.Data
{
    public class DataCreatorUtils
    {
        private User user;

        public User GetUser() {
            if (user == null) {
                user = new User() {
                    login = HashManager.GenerateToken()
                }.CreateAndGet();
            }

            return user;
        }

        public void CleanUpData() {
            user?.Delete();
        }
    }
}