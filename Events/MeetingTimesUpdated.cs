using Newtonsoft.Json;
using System.Collections.Generic;
using cardinal_webservices.Models;

namespace cardinal_webservices 
{
    public class MeetingTimesUpdated 
    {
        [JsonProperty("type")]
        public string Type { get; set; } = "MEETING_TIMES_UPDATED";
        [JsonProperty("payload")]
        public object Payload { get; set; }

        public static MeetingTimesUpdated FromMeetingTimes(IEnumerable<MeetingTimeModel> meetingTimes)
        {
            return new MeetingTimesUpdated() 
            {
                Payload = meetingTimes
            };
        }
    }
}