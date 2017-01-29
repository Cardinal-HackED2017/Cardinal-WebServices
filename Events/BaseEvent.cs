using Newtonsoft.Json;

namespace cardinal_webservices.Events 
{
    public class BaseEvent 
    {
        [JsonProperty("type")]
        public string Type { get; set; }
        [JsonProperty("payload")]
        public object Payload { get; set; }
    }
}