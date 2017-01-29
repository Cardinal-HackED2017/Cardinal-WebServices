using System;
using cardinal_webservices.DataModels;

namespace cardinal_webservices.Models 
{
    public class MeetingTimeModel
    {
        public DateTime startTime {get; set;}
        public DateTime endTime {get; set;}

        public TimeSpan gapWidth {get;set;}

        public MeetingTimeModel(MeetingTime meetingTime, Meeting meeting)
        {
            startTime = meetingTime.StartTime;
            gapWidth = meetingTime.length;
            endTime = meetingTime.StartTime.Add(meeting.Length);
        }
    }
}