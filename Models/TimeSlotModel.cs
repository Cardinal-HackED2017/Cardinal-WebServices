using System;

namespace cardinal_webservices.Models
{
    public class TimeSlotModel
    {
        public DateTime start {get; set;}
        public TimeSpan length {get; set;}

        public DateTime end()
        {
            return start.Add(length);
        }
    }
}