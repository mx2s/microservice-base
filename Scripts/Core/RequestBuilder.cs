using System;

namespace SharpyJson.Scripts.Core
{
    public class RequestBuilder
    {
        public static Request Build(RawRequest rawRequest) {
            return new Request(
                GetRequestTypeFromRaw(rawRequest), rawRequest.Token
            );
        }

        public static RequestTypes GetRequestTypeFromRaw(RawRequest rawRequest) {
            if (!Enum.IsDefined(typeof(RequestTypes), rawRequest.RequestType)) {
                return RequestTypes.None;
            }

            return (RequestTypes) rawRequest.RequestType;
        } 
    }
}