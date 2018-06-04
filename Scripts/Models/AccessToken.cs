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

        public static IEnumerable All() {
            var dbConnection = DBConnector.get().GetDbConnection();
            return dbConnection.Query<AccessToken>("SELECT * FROM access_tokens");
        }

        public static List<AccessToken> ListAll() {
            return AccessToken.All().Cast<AccessToken>().ToList();
        }

        public static IEnumerable GetByUserId(int userId) {
            var dbConnection = DBConnector.get().GetDbConnection();
            return dbConnection.Query<AccessToken>("SELECT * FROM access_tokens WHERE user_id = @user_id", new { userId });
        }

        public static List<AccessToken> GetListByUserId(int userId) {
            return AccessToken.GetByUserId(1).Cast<AccessToken>().ToList();
        }

        public static int Count() {
            return DBConnector.get().GetDbConnection().ExecuteScalar<int>("SELECT COUNT(*) FROM access_tokens");
        }
        
        public static int UserTokensCount(int userId) {
            return DBConnector.get().GetDbConnection()
                .ExecuteScalar<int>("SELECT COUNT(*) FROM access_tokens WHERE user_id = @user_id", new { userId });
        }
        
        public static AccessToken Find(int id) {
            var dbConnection = DBConnector.get().GetDbConnection();
            return dbConnection.Query<AccessToken>("SELECT * FROM access_tokens WHERE id = @id LIMIT 1", new {id}).FirstOrDefault();
        }
        
        public static AccessToken FindByToken(string token) {
            var dbConnection = DBConnector.get().GetDbConnection();
            return dbConnection.Query<AccessToken>($"SELECT * FROM access_tokens WHERE token = '{token}' LIMIT 1").FirstOrDefault();
        }

        public void Save() {
            var dbConnection = DBConnector.get().GetDbConnection();
            string sql = $"UPDATE access_tokens SET token = '{this.token}', user_id = @user_id WHERE id = @id";
            DBConnector.get().GetDbConnection().Execute(sql, new {id = this.id, user_id = this.user_id});
        }

        public static void Create(AccessToken newToken) {
            string sql = $"INSERT INTO public.access_tokens(user_id, token) VALUES ('{newToken.user_id}', '{newToken.token}')"; 
            DBConnector.get().GetDbConnection().Execute(sql);
        }
    }
}