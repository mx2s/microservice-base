using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Runtime.Remoting.Channels;
using System.Security.Cryptography;
using System.Xml.Xsl.Runtime;
using Newtonsoft.Json.Linq;
using SharpyJson.Scripts.Models.Microservice;
using SharpyJson.Scripts.Modules.Response;

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
            var sendToAll = idsToSend == null;
            idsToSend = idsToSend ?? new int[0];

            var servicesList = MicroserviceManager.get().GetServices(type);
            var servicesToSend = sendToAll ? servicesList : new Dictionary<int, MicroServiceNode>();
            
            if (!sendToAll) {
                foreach (int index in idsToSend) {
                    if (servicesList.ContainsKey(index)) {
                        servicesToSend.Add(index, servicesToSend[index]);
                    }
                }
            }
            
            servicesList = null;

            foreach (var service in servicesToSend) {
                // TODO: Process response
                service.Value.Client.SendMessage(data);
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