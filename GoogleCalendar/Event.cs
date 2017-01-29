using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace cardinal_webservices.GoogleCalendar 
{
    public class EventsResponseWrapper 
    {
        [JsonProperty("items")]
        public IEnumerable<Event> Items { get; set;}
    }

    public class Event 
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("start")]        
        public TimeSlot Start { get; set; }
        [JsonProperty("end")]        
        public TimeSlot End { get; set; }

    }

    public class TimeSlot
    {
        [JsonProperty("date")]
        public DateTime Date { get; set; }
        [JsonProperty("dateTime")]
        public DateTime DateTime { get; set; }
        [JsonProperty("timeZone")]
        public string TimeZone { get; set; }
    }
}