using cardinal_webservices.DataModels;
using cardinal_webservices.Models;
using Newtonsoft.Json;

namespace cardinal_webservices.Events 
{
    public class MessageCreatedEvent 
    {
        [JsonProperty("type")]
        public string Type { get; set; } = "MESSAGE_CREATED";
        [JsonProperty("payload")]
        public object Payload { get; set; }

        public static MessageCreatedEvent FromMessage(MessageModel message)
        {
            return new MessageCreatedEvent 
            {
                Payload = message
            };
        }
    }
}