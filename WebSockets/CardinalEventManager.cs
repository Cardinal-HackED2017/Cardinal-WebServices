using System.Collections.Generic;
using System.Net.WebSockets;
using System.Linq;
using System.Threading.Tasks;
using cardinal_webservices.Data;
using cardinal_webservices.DataModels;
using cardinal_webservices.Events;

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
        
        public async void OnMessageCreated(Message message) 
        {
            var sendTasks = _sockets.Where(s => IsUserInMeeting(s.UserId, message.MeetingId))
                                    .Select(s => s.SendObjectAsync(MessageCreatedEvent.FromMessage(message)));

            await Task.WhenAll(sendTasks);
        }

        public async void OnUserJoined(User user, Meeting meeting)
        {
            var sendTasks = _sockets.Where( s=> IsUserInMeeting(s.UserId, meeting.Id))
                                    .Select(s => s.SendObjectAsync(UserJoinedEvent.FromUser(user)));

            await Task.WhenAll(sendTasks);
        }

        private bool IsUserInMeeting(string userId, string meetingId) 
        {
            return _cardinalDataService.GetMeetingParticipations()
                                       .Where(p => p.UserId == userId && p.MeetingId == meetingId)
                                       .Any();
        }
    }
}