using System;
using Newtonsoft.Json;
using SharpyJson.Scripts.Modules.Response;

namespace SharpyJson.Scripts.Core
{
    public class RequestBuilder
    {
        public static Request Build(string data) {
            var clientRequest = JsonConvert.DeserializeObject<RawRequest>(data);

            if (!Enum.IsDefined(typeof(RequestTypes), clientRequest.RequestType)) {
                return new Request(RequestTypes.None);
            }

            var newRequest = new Request(
                (RequestTypes) clientRequest.RequestType, clientRequest.Token
            );
           
            return newRequest;
        }
    }
}