using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using SharpyJson.Scripts.Modules.DB;

namespace SharpyJson.Scripts.Models
{
    public class AccessToken
    {
        public int id;
        public int user_id;
        public string token;

        public static IEnumerable All()
            => DBConnector.Get().GetDbConnection().Query<AccessToken>("SELECT * FROM access_tokens");

        public static List<AccessToken> ListAll()
            => All().Cast<AccessToken>().ToList();

        public static IEnumerable GetByUserId(int userId)
            => DBConnector.Get().GetDbConnection()
                .Query<AccessToken>("SELECT * FROM access_tokens WHERE user_id = @user_id", new {userId});

        public static List<AccessToken> GetListByUserId(int userId)
            => GetByUserId(userId).Cast<AccessToken>().ToList();

        public static int UserTokensCount(int userId)
            => DBConnector.Get().GetDbConnection()
                .ExecuteScalar<int>("SELECT COUNT(*) FROM access_tokens WHERE user_id = @user_id", new {userId});

        public static AccessToken Find(int id)
            => DBConnector.Get().GetDbConnection()
                .Query<AccessToken>("SELECT * FROM access_tokens WHERE id = @id LIMIT 1", new {id}).FirstOrDefault();

        public static AccessToken FindByToken(string token)
            => DBConnector.Get().GetDbConnection()
                .Query<AccessToken>($"SELECT * FROM access_tokens WHERE token = '{token}' LIMIT 1")
                .FirstOrDefault();

        public void Save()
            => DBConnector.Get().GetDbConnection()
                .Execute("UPDATE access_tokens SET token = @token, user_id = @user_id WHERE id = @id",
                    new {token, user_id, id});

        public static void Create(AccessToken newToken)
            => DBConnector.Get().GetDbConnection()
                .Execute("INSERT INTO public.access_tokens(user_id, token) VALUES (@userId, @token)");
    }
}