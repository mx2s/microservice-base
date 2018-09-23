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
            => DBConnector.Get().GetDbConnection().Query<User>("select * from users");

        public static int Count()
            => DBConnector.Get().GetDbConnection().ExecuteScalar<int>(
                "SELECT COUNT(*) FROM users"
            );

        public static User Find(int id)
            => DBConnector.Get().GetDbConnection().Query<User>(
                "SELECT * FROM users WHERE id = @id LIMIT 1",
                new {id}
            ).FirstOrDefault();

        public static User FindByLogin(string login)
            => DBConnector.Get().GetDbConnection().Query<User>(
                "SELECT * FROM users WHERE login = @login LIMIT 1", new {login}
            ).FirstOrDefault();

        public static void Create(string login, string password)
            => DBConnector.Get().GetDbConnection()
                .Execute(
                    "INSERT INTO public.users(login, password) VALUES (@login, @password)"
                    , new {login, password}
                );

        public void Create() => Create(login, password);

        public User CreateAndGet() {
            Create();
            return FindByLogin(login);
        }

        public void Refresh() => Find(id);

        public void Save()
            => DBConnector.Get().GetDbConnection()
                .Execute(
                    "UPDATE users SET login = @login, password = @password WHERE id = @id",
                    new {login, password, id}
                );

        public void Delete() => DBConnector.Get().GetDbConnection()
            .Execute("DELETE FROM users WHERE id = @id", new {id});
    }
}