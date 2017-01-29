using System.Collections.Generic;
using Newtonsoft.Json;

namespace cardinal_webservices.GoogleCalendar 
{
    public class CalendarsResponseWrapper 
    {
        [JsonProperty("items")]
        public IEnumerable<Calendar> Items { get; set; }
    }
    
    public class Calendar 
    {
        [JsonProperty("id")]
        public string Id { get; set; }
    }
}