using System.Collections.Generic;
using System.Net.WebSockets;
using System.Linq;
using System.Threading.Tasks;
using cardinal_webservices.Data;
using cardinal_webservices.DataModels;
using cardinal_webservices.Events;
using cardinal_webservices.Models;

namespace cardinal_webservices.WebSockets 
{
    public class CardinalEventManager 
    {
        private readonly IEnumerable<CardinalWebSocket> _sockets;
        private readonly ICardinalDataService _cardinalDataService;

        public CardinalEventManager(ICardinalDataService cardinalDataService) 
        {
            _sockets = new List<CardinalWebSocket>();
            _cardinalDataService = cardinalDataService;   
        }
        
        private bool IsUserInMeeting(string userId, string meetingId) 
        {
            return _cardinalDataService.GetMeetingParticipations()
                                       .Where(p => p.UserId == userId && p.MeetingId == meetingId)
                                       .Any();
        }

        public async void OnMessageCreated(Message message) 
        {
            var sendTasks = _sockets.Where(s => IsUserInMeeting(s.UserId, message.MeetingId))
                                    .Select(s => s.SendObjectAsync(MessageCreatedEvent.FromMessage(message)));

            await Task.WhenAll(sendTasks);
        }

        public async void OnUserJoined(User user, Meeting meeting)
        {
            var sendTasks = _sockets.Where(s => IsUserInMeeting(s.UserId, meeting.Id))
                                    .Select(s => s.SendObjectAsync(UserJoinedEvent.FromUser(user)));

            await Task.WhenAll(sendTasks);
        }

        public MeetingTimesUpdated GetMeetingTimesUpdatedEvent(string meetingId) 
        {
            var meeting = _cardinalDataService.GetMeetings().Where(m => m.Id == meetingId).First();
            var meetingTimesForMeeting = _cardinalDataService.GetMeetingTimesForMeeting(meetingId);
            var meetingTimeModels = meetingTimesForMeeting.Select(m => new MeetingTimeModel(m, meeting));

            return MeetingTimesUpdated.FromMeetingTimes(meetingTimeModels);
        }

        public async void OnMeetingTimesUpdated(string meetingId) 
        {
            var sendTasks = _sockets.Where(s => IsUserInMeeting(s.UserId, meetingId))
                                    .Select(s => s.SendObjectAsync(GetMeetingTimesUpdatedEvent(meetingId));

            await Task.WhenAll(sendTasks);
        }
    }
}