using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace ProductCatalog.Responses
{
    public static class JsonSerializer
    {
        private static JsonSerializerSettings settings = new JsonSerializerSettings
        {
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            NullValueHandling = NullValueHandling.Ignore,
            DateTimeZoneHandling = DateTimeZoneHandling.Utc,
            ContractResolver = new DefaultContractResolver
            {
                NamingStrategy = new SnakeCaseNamingStrategy()
            }
        };

        public static string SerializeObject(object data)
        {
            return JsonConvert.SerializeObject(data, Formatting.Indented, settings);
        }
    }
}