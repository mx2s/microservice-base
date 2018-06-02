using System;

namespace SharpyJson.Scripts.Core
{
    public class RequestBuilder
    {
        public static Request Build(RawRequest rawRequest) {           
            if (!Enum.IsDefined(typeof(RequestTypes), rawRequest.RequestType)) {
                return new Request(RequestTypes.None);
            }

            return new Request(
                (RequestTypes) rawRequest.RequestType, rawRequest.Token
            );          
        }       
    }
}