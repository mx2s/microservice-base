using System;
using SharpyJson.Scripts.Core;

namespace SharpyJson.Scripts.Modules.Request
{
    public class RequestBuilder
    {
        public static Core.Request Build(RawRequest rawRequest) {
            return new Core.Request(
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