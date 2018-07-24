using System.Collections.Generic;
using System.Threading;
using SharpyJson.Scripts.Models.Microservice;
using SharpyJson.Scripts.Modules.Response;
using SharpyJson.Scripts.Modules.Websocket;

namespace SharpyJson.Scripts.Modules.Microservice
{
    public class MicroserviceManager
    {       
        private static MicroserviceManager instance;
        private Dictionary<MicroserviceTypes, Dictionary<int, MicroServiceNode>> servicesList;

        public MicroserviceManager() {
            servicesList = new Dictionary<MicroserviceTypes, Dictionary<int, MicroServiceNode>>();
        }
        
        public static MicroserviceManager get() {
            instance = instance ?? new MicroserviceManager();
            return instance;
        }

        public static RequestResponse SendRequest(
            MicroserviceTypes type, string data, int[] idsToSend = null
        ) {
            int servicesLimit = 5;
            
            var sendToAll = idsToSend == null;
            idsToSend = idsToSend ?? new int[servicesLimit]; // TODO: user idsToSend

            var servicesToSend = new List<MicroServiceNode>();
            
            if (sendToAll) {
                foreach (var service in get().GetServices(type)) {
                    if (service.Value != null) {
                        servicesToSend.Add(service.Value);
                    }
                }
            }           
            var requestsKeys = new int[servicesLimit];
            
            for (int i = 0; i < servicesToSend.Count; i++) {
                if (servicesToSend[i].Client != null) {
                    requestsKeys[i] = servicesToSend[i].Client.SendMessage(data);
                }
            }

            Thread.Sleep(50);
            
            int waitIterations = 20;
            
            for (int i = 0; i < waitIterations; i++) {
                foreach (int key in requestsKeys) {
                    if (key <= 0 || WebsocketRequests.Responses[key] == null) {
                        continue;
                    }

                    var response = RequestResponse.BuildFromString(WebsocketRequests.Responses[key]);

                    WebsocketRequests.Responses[key] = null;

                    if (response != null) {
                        return response;
                    }
                }
                Thread.Sleep(50);
            }

            return null;
        }

        public Dictionary<int, MicroServiceNode> GetServices(MicroserviceTypes type) {
            if (!servicesList.ContainsKey(type)) {
                servicesList[type] = new Dictionary<int, MicroServiceNode>();
            }

            return servicesList[type];
        }

        public void AddService(MicroServiceNode service) {
            var type = service.GetMicroserviceType();
            
            if (type == MicroserviceTypes.None) {
                return;
            }
            if (!servicesList.ContainsKey(type)) {
                servicesList.Add(type, new Dictionary<int, MicroServiceNode>());
                servicesList[type].Add(service.service_id, service);
                return;
            }
            if (servicesList[type].ContainsKey(service.service_id)) {
                return;
            }
            
            servicesList[type].Add(service.service_id, service);
        }
    }
}