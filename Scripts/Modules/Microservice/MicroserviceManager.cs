using Newtonsoft.Json.Linq;
using SharpyJson.Scripts.Modules.Response;

namespace SharpyJson.Scripts.Modules.Microservice
{
    public class MicroserviceManager
    {
        public RequestResponse SendRequest(MicroserviceTypes microserviceTypes = MicroserviceTypes.None, JObject data = null, bool sendToAll = true, int[] idsToSend = null) {
            idsToSend = idsToSend ?? new int[0];
            data = data ?? new JObject();

            return null;
        }
    }
}