using System;
using System.Linq;
using System.Collections.Generic;
using cardinal_webservices.DataModels;

namespace cardinal_webservices.Models 
{
    public class MeetingModel
    {
        public IEnumerable<MeetingTimeModel> suggestedTimes {get; set;}
        public string name {get; set;}
        public string Id {get; set;}
        public DateTime createdTime {get; set;}
        public DateTime startFence { get; set; }
        public DateTime endFence {get; set;}
        public TimeSpan length {get; set;}
        public Location location {get; set;}
        public IEnumerable<string> Participants { get; set; }

        public MeetingModel(Meeting meeting, IEnumerable<User> usersRemote, IEnumerable<MeetingTime> meetingTimes)
        {
            name = meeting.Name;
            Id = meeting.Id;
            createdTime = meeting.CreatedTime;
            startFence = meeting.StartFence;
            endFence = meeting.EndFence;
            length = meeting.Length;

            location = new Location {
                Longitude = meeting.Longitude,
                Latitude = meeting.Latitude,
                Description = meeting.LocationDescription
            };

            Participants = usersRemote.Select(u => u.DisplayName);
            suggestedTimes = meetingTimes.Select(m => new MeetingTimeModel(m, meeting));
        }

    }

}