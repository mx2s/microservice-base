using System;
using SharpyJson.Scripts.Core;

namespace SharpyJson.Scripts.Modules.Request
{
    public static class RequestBuilder
    {
        public static Response.Request Build(RawRequest rawRequest) {
            return new Response.Request(
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