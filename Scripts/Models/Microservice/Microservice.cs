using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using SharpyJson.Scripts.Modules.DB;

namespace SharpyJson.Scripts.Models.Microservice
{
    public class Microservice
    {
        public int id;
        public int service_id;
        public int service_type;
        public string host;
        public int port;
        public string token;

        public static IEnumerable All() {
            var dbConnection = DBConnector.get().GetDbConnection();
            return dbConnection.Query<Microservice>("SELECT * FROM microservices");
        }

        public static List<Microservice> ListAll() {
            return All().Cast<Microservice>().ToList();
        }

        public static IEnumerable GetByServiceId(int serviceId) {
            return DBConnector.get().GetDbConnection()
                .Query<Microservice>("SELECT * FROM microservices WHERE service_id = @serviceId",
                    new { serviceId }
                );
        }

        public static List<Microservice> GetListByServiceId(int serviceId) {
            return GetByServiceId(serviceId).Cast<Microservice>().ToList();
        }
        
        public static Microservice FindByServiceId(int serviceId) {
            return DBConnector.get().GetDbConnection()
                .Query<Microservice>("SELECT * FROM microservices WHERE id = @id LIMIT 1", new { serviceId })
                .FirstOrDefault();
        }

        public static int Count() {
            return DBConnector.get().GetDbConnection().ExecuteScalar<int>("SELECT COUNT(*) FROM microservices");
        }
        
        public static Microservice Find(int id) {
            return DBConnector.get().GetDbConnection()
                .Query<Microservice>("SELECT * FROM microservices WHERE id = @id LIMIT 1", new { id })
                .FirstOrDefault();
        }

        public static Microservice FindByToken(string token) {
            var dbConnection = DBConnector.get().GetDbConnection();
            return dbConnection.Query<Microservice>($"SELECT * FROM microservices WHERE token = '{token}' LIMIT 1")
                .FirstOrDefault();
        }
    }
}