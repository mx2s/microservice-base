using System.Collections;
using System.Linq;
using Dapper;
using SharpyJson.Scripts.Modules.DB;

namespace SharpyJson.Scripts.Models
{
    public class AccessToken
    {
        public int id { get; set; }
        public int user_id { get; set; }
        public string token { get; set; }

        public static IEnumerable All() {
            var dbConnection = DBConnector.get().GetDbConnection();
            return dbConnection.Query<User>("select * from users");
        }
        
        public static AccessToken Find(int id) {
            var dbConnection = DBConnector.get().GetDbConnection();
            return dbConnection.Query<AccessToken>("SELECT * FROM access_tokens WHERE id = @id LIMIT 1", new {id}).FirstOrDefault();
        }
        
        public static AccessToken FindByToken(string token) {
            var dbConnection = DBConnector.get().GetDbConnection();
            return dbConnection.Query<AccessToken>($"SELECT * FROM access_tokens WHERE login = '{token}' LIMIT 1").FirstOrDefault();
        }

        public void Save() {
            var dbConnection = DBConnector.get().GetDbConnection();
            string sql = $"UPDATE access_tokens SET token = '{this.token}', user_id = @user_id WHERE id = @id";
            DBConnector.get().GetDbConnection().Execute(sql, new {id = this.id, user_id = this.user_id});
        }

        public static void Create(AccessToken newToken) {
            string sql = $"INSERT INTO public.access_tokens(user_id, token) VALUES ('{newToken.id}', '{newToken.token}')"; 
            DBConnector.get().GetDbConnection().Execute(sql);
        }
    }
}