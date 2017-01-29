using cardinal_webservices.GoogleCalendar;

namespace cardinal_webservices 
{
    public class MeetingTimesCalculator 
    {
        private readonly GoogleCalendarService _googleCalendarService;

        public MeetingTimesCalculator(GoogleCalendarService googleCalendarService)
        {
            _googleCalendarService = googleCalendarService;
        }
        public async void ProcessUserJoinMeeting(string userId, string userCalendarToken) 
        {
            
        }   
    }
}