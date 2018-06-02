using System;
using System.Collections;
using System.Linq;
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
            return dbConnection.Query<User>("SELECT * FROM users WHERE id = @id LIMIT 1", new {id}).FirstOrDefault();
        }
        
        public static User FindByLogin(string login) {
            var dbConnection = DBConnector.get().GetDbConnection();
            return dbConnection.Query<User>($"SELECT * FROM users WHERE login = '{login}' LIMIT 1").FirstOrDefault();
        }

        public void Save() {
            var dbConnection = DBConnector.get().GetDbConnection();
            string sql = $"UPDATE users SET login = '{this.Login}', password = '{this.Password}' WHERE id = @id";
            DBConnector.get().GetDbConnection().Execute(sql, new {this.Id});
        }

        public static void Create(User newUser) {
            string sql = $"INSERT INTO public.users(login, password) VALUES ('{newUser.Login}', '{newUser.Password}')"; 
            DBConnector.get().GetDbConnection().Execute(sql);
        }
    }
}