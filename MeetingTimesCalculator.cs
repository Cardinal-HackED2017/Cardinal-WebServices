using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using cardinal_webservices.Data;
using cardinal_webservices.GoogleCalendar;
using cardinal_webservices.Models;
using cardinal_webservices.DataModels;
using System;
using cardinal_webservices.WebSockets;

namespace cardinal_webservices 
{
    public class MeetingTimesCalculator 
    {
        private readonly ICardinalDataService _cardinalDataService;
        private readonly CardinalEventManager _eventManager;

        public MeetingTimesCalculator(ICardinalDataService cardinalDataService, CardinalEventManager eventManager) 
        {
            _cardinalDataService = cardinalDataService;
            _eventManager = eventManager;
        }

        public async void ProcessUserJoinMeeting(string meetingId, string userId, string userCalendarToken) 
        {
            var meeting = _cardinalDataService.GetMeetings().Where(m => m.Id == meetingId).First();
            var userEvents = await GetUserEvents(userId, userCalendarToken);
            var events = _cardinalDataService.GetUserEvents().Where(e => e.meetingId == meetingId);

            foreach (UserEvent userEvent in events)
            {
                await _cardinalDataService.UpsertUserEvent(userEvent);
            }
            var calendar = new CalendarModel
            {
                    startDate = meeting.StartFence,
                    endDate = meeting.EndFence,
                    dayStart = meeting.dayStart,
                    dayEnd = meeting.dayEnd,
                    lengthOfMeeting = meeting.Length,
                    events = events.Select(e => UserEventModel.UserEventModelFromUserEvent(e)).ToList()
            };

            calendar.consolidateEvents();
            
            var Gaps = calendar.gaps;
            DateTime startTime;
            foreach(TimeSlotModel gap in Gaps)
            {  
                int numMeetings =  Convert.ToInt32(Math.Floor(gap.length.TotalMinutes/meeting.Length.TotalMinutes));
                while(numMeetings > 0)
                {
                    startTime = Gaps[numMeetings - 1].start;
                    await _cardinalDataService.UpsertMeetingTimeAsync(new MeetingTime
                    {
                        Id = Guid.NewGuid().ToString(),
                        MeetingId = meetingId,
                        StartTime = startTime,
                        length = meeting.Length
                    });
                    numMeetings--;
                    startTime.Add(meeting.Length);
                }
            }

            _eventManager.OnMeetingTimesUpdated(meetingId);

        }   

        private async Task<IEnumerable<UserEventModel>> GetUserEvents(string userId, string userCalendarToken) 
        {
            var googleEvents = await GetAllEvents(userCalendarToken);
            return googleEvents.Select(e => EventToUserEvent(e, userId));
        }

        private UserEventModel EventToUserEvent(Event ev, string userId)
        {
            return new UserEventModel 
            {
                userId = userId,
                name = ev.Id,
                timeSlot = new TimeSlotModel
                {
                    start = ev.Start.DateTime,
                    length = ev.End.DateTime.Subtract(ev.Start.DateTime)
                }
            };
        }

        private async Task<IEnumerable<Event>> GetAllEvents(string userCalendarToken)
        {  
            var calendarService = GetCalendarService(userCalendarToken);
            var calendars = await calendarService.GetCalendarsAsync();
            var events = new List<Event>();

            foreach (var cal in calendars) 
            {
                events.AddRange(await calendarService.GetEventsAsync(cal.Id));
            }

            return events;
        } 

        private GoogleCalendarService GetCalendarService(string token)
        {
            return new GoogleCalendarService(token);
        }
    }
}