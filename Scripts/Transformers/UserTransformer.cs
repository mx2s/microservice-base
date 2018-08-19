using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using SharpyJson.Scripts.Models;

namespace SharpyJson.Scripts.Transformers
{
    public class UserTransformer
    {
        public static JObject Transform(User item)
            => new JObject {
                ["id"] = item.id,
                ["login"] = item.login,
                ["email"] = item.email,
                ["registerDate"] = item.register_date
            };

        public static JArray TransformMultiple(IEnumerable<User> items) {
            JArray result = new JArray();
            foreach (var item in items) {
                result.Add(Transform(item));
            }

            return result;
        }
    }
}