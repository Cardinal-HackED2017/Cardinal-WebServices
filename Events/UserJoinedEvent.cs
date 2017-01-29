using cardinal_webservices.DataModels;
using Newtonsoft.Json;

namespace cardinal_webservices.Events 
{
    public class UserJoinedEvent
    {
        [JsonProperty("type")]
        public string Type {get; set;}
        [JsonProperty("payload")]
        public object Payload {get; set;}

        public static UserJoinedEvent FromUser(User user)
        {
            return new UserJoinedEvent
            {
                Payload = user
            };
        }
    }
}