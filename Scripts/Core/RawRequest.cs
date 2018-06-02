﻿using System;
using Newtonsoft.Json.Linq;

namespace SharpyJson.Scripts.Core
{
    public class RawRequest
    {
        public string Token = "";
        public int RequestType = 0;
        public JToken Data = "";

        public static RawRequest BuildFromString(string data) {
            var newRequest = new RawRequest();
            JObject schema = JObject.Parse(data);
            newRequest.Token = schema.SelectToken("token").Value<string>();
            newRequest.RequestType = schema.SelectToken("type").Value<int>();
            newRequest.Data = schema.SelectToken("data");
            return newRequest;
        }
    }
}