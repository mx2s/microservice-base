using System;
using System.Collections;
using System.Linq;
using Dapper;
using SharpyJson.Scripts.Modules.DB;

namespace SharpyJson.Scripts.Models
{
    public class User
    {
        public int id;
        public string login;
        public string password;
        public string email;
        public DateTime register_date;

        public static IEnumerable All() {
            var dbConnection = DBConnector.get().GetDbConnection();
            return dbConnection.Query<User>("select * from users");
        }

        public static int Count() {
            return DBConnector.get().GetDbConnection().ExecuteScalar<int>("SELECT COUNT(*) FROM users");
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
            string sql = $"UPDATE users SET login = '{this.login}', password = '{this.password}' WHERE id = @id";
            DBConnector.get().GetDbConnection().Execute(sql, new {Id = this.id});
        }

        public static void Create(string login, string password) {
            DBConnector.get().GetDbConnection()
                .Execute(
                    $"INSERT INTO public.users(login, password) VALUES (@login, @password)"
                    , new {login, password}
                );
        }
    }
}