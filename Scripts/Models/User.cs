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

        public static IEnumerable All() {
            var dbConnection = DBConnector.get().GetDbConnection();
            return dbConnection.Query<User>("select * from users");
        }
        
        public static User Find(int id) {
            var dbConnection = DBConnector.get().GetDbConnection();
            return dbConnection.QueryFirst<User>("select * from users where id = @id", new { id });
        }
        
        public static User FindByLogin(string login) {
            var dbConnection = DBConnector.get().GetDbConnection();
            return dbConnection.QueryFirst<User>("select * from users where login = @login", new { login });
        }
        
        public static User Save(User user) {
            var dbConnection = DBConnector.get().GetDbConnection();
            return dbConnection.QueryFirst<User>("UPDATE users SET login = @login WHERE id = @id", new {
                login = user.Login, id = user.Id
            });
        }

        public static void Create(User newUser) {
            string sql = $"INSERT INTO public.users(login, password) VALUES ('{newUser.Login}', '{newUser.Password}')"; 
            DBConnector.get().GetDbConnection().Execute(sql, newUser);
        }
    }
}