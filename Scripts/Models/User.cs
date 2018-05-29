using System.Collections;
using Dapper;
using SharpyJson.Scripts.Modules.DB;

namespace SharpyJson.Scripts.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }

        public IEnumerable All() {
            var dbConnection = DBConnector.get().GetDbConnection();
            return dbConnection.Query<User>("select * from users");
        }

        public static void Create(User newUser) {
            string sql = $"INSERT INTO public.users(login, password) VALUES ('{newUser.Login}', '{newUser.Password}')";
      
            DBConnector.get().GetDbConnection().Execute(sql, newUser);
        }
    }
}