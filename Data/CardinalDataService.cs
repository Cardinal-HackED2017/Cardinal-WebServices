using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using cardinal_webservices.DataModels;

namespace cardinal_webservices.Data 
{
    public class CardinalDataService: ICardinalDataService 
    {
        private readonly CardinalDbContext _cardinalDbContext;

        public CardinalDataService(CardinalDbContext cardinalDbContext) 
        {
            _cardinalDbContext = cardinalDbContext;
        }

        public IEnumerable<Meeting> GetMeetings() 
        {
            return _cardinalDbContext.Meetings;
        }

        public IEnumerable<Message> GetMessages()
        {
            return _cardinalDbContext.Messages;
        }

       /* public IEnumerable<MeetingParticipation> GetMeetingParticipations()
        {
            throw new NotImplementedException();
            //return _cardinalDbContext.MeetingParticipations;
        }

        public IEnumerable<MeetingTime> GetMeetingTimes()
        {
             return _cardinalDbContext.MeetingTimes;
        }*/

        public IEnumerable<User> GetUsers()
        {
            return _cardinalDbContext.Users;
        }

        public async Task UpsertMeetingAsync(Meeting meeting)
        {
            _cardinalDbContext.Meetings.Add(meeting);
            await _cardinalDbContext.SaveChangesAsync();
        }

        public async Task UpsertMessageAsync(Message message)
        {
            _cardinalDbContext.Messages.Add(message);
            await _cardinalDbContext.SaveChangesAsync();
        }
        public async Task UpsertUserAsync(User user)
        {
            _cardinalDbContext.Users.Add(user);
            await _cardinalDbContext.SaveChangesAsync();
        }
    }
}