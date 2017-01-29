using Newtonsoft.Json;

namespace cardinal_webservices.GoogleCalendar 
{
    public class Calendar 
    {
        [JsonProperty("id")]
        public string Id { get; set; }
    }
}