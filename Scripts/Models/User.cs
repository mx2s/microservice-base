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

        public static IEnumerable All()
            => DBConnector.get().GetDbConnection().Query<User>("select * from users");

        public static int Count()
            => DBConnector.get().GetDbConnection().ExecuteScalar<int>(
                "SELECT COUNT(*) FROM users"
            );

        public static User Find(int id)
            => DBConnector.get().GetDbConnection().Query<User>(
                "SELECT * FROM users WHERE id = @id LIMIT 1",
                new {id}
            ).FirstOrDefault();

        public static User FindByLogin(string login)
            => DBConnector.get().GetDbConnection().Query<User>(
                $"SELECT * FROM users WHERE login = @login LIMIT 1", new {login}
            ).FirstOrDefault();

        public void Save()
            => DBConnector.get().GetDbConnection()
                .Execute(
                    "UPDATE users SET login = @login, password = @password WHERE id = @id",
                    new {login, password, id}
                );

        public static void Create(string login, string password)
            => DBConnector.get().GetDbConnection()
                .Execute(
                    $"INSERT INTO public.users(login, password) VALUES (@login, @password)"
                    , new {login, password}
                );
    }
}