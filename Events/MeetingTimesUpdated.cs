using Newtonsoft.Json;

namespace cardinal_webservices 
{
    public class MeetingTimesUpdated 
    {
        [JsonProperty("type")]
        public string Type { get; set; } = "MEETING_TIMES_UPDATED";
        [JsonProperty("payload")]
        public object Payload { get; set; }

        public static MeetingTimesUpdated FromMeetingTimes()
        {
            return new MeetingTimesUpdated(); 
        }
    }
}