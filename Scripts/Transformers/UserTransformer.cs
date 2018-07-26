using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using SharpyJson.Scripts.Models;

namespace SharpyJson.Scripts.Transformers
{
    public class UserTransformer
    {
        public static JObject Transform(User item) {
            return new JObject {
                ["id"] = item.id,
                ["login"] = item.login,
                ["email"] = item.email,
                ["registerDate"] = item.register_date
            };
        }
        
        public static JArray TransformList(List<User> items) {
            JArray result = new JArray();
            for (int i = 0; i < items.Count; i++) {
                result.Add(Transform(items[i]));
            }
            return result;
        }
    }
}