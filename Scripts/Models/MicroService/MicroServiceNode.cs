using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using SharpyJson.Scripts.Modules.DB;
using SharpyJson.Scripts.Modules.Microservice;
using SharpyJson.Scripts.Modules.Websocket;

namespace SharpyJson.Scripts.Models.Microservice
{
    public class MicroServiceNode {
        public int id;
        public int service_id;
        public int service_type;
        public string host;
        public int port;
        public string token;

        public WebsocketClient Client;

        public static IEnumerable All()
            => DBConnector.Get().GetDbConnection().Query<MicroServiceNode>("SELECT * FROM microservices");

        public static List<MicroServiceNode> ListAll()
            => All().Cast<MicroServiceNode>().ToList();

        public static IEnumerable GetByServiceId(int serviceId)
            => DBConnector.Get().GetDbConnection()
                .Query<MicroServiceNode>("SELECT * FROM microservices WHERE service_id = @serviceId",
                    new {serviceId}
                );

        public static List<MicroServiceNode> GetListByServiceId(int serviceId)
            => GetByServiceId(serviceId).Cast<MicroServiceNode>().ToList();
        
        public static MicroServiceNode FindByServiceId(int serviceId)
            => DBConnector.Get().GetDbConnection()
                .Query<MicroServiceNode>("SELECT * FROM microservices WHERE id = @id LIMIT 1", new { serviceId })
                .FirstOrDefault();

        public static int Count()
            => DBConnector.Get().GetDbConnection().ExecuteScalar<int>("SELECT COUNT(*) FROM microservices");
        
        public static MicroServiceNode Find(int id)
            => DBConnector.Get().GetDbConnection()
                .Query<MicroServiceNode>("SELECT * FROM microservices WHERE id = @id LIMIT 1", new { id })
                .FirstOrDefault();

        public static MicroServiceNode FindByToken(string token)
            => DBConnector.Get().GetDbConnection()
                .Query<MicroServiceNode>("SELECT * FROM microservices WHERE token = @token LIMIT 1", new {token})
                .FirstOrDefault();

        public MicroserviceTypes GetMicroserviceType() {
            if (!Enum.IsDefined(typeof(MicroserviceTypes), service_type)) {
                return MicroserviceTypes.None;
            }

            return (MicroserviceTypes) service_type;
        }
    }
}