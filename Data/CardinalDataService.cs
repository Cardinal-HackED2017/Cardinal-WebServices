using System;
using System.Linq;
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

        public IEnumerable<MeetingParticipation> GetMeetingParticipations()
        {
            return _cardinalDbContext.MeetingParticipations;
        }

        public IEnumerable<MeetingTime> GetMeetingTimes()
        {
             return _cardinalDbContext.MeetingTimes;
        }

        public IEnumerable<User> GetUsers()
        {
            return _cardinalDbContext.Users;
        }

        public IEnumerable<Invitation> GetInvitations() 
        {
            return _cardinalDbContext.Invitations;
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

        public async Task UpsertMeetingParticipationAsync(MeetingParticipation meetingParticipation)
        {
            _cardinalDbContext.MeetingParticipations.Add(meetingParticipation);
            await _cardinalDbContext.SaveChangesAsync();
        }

        public async Task UpsertMeetingTimeAsync(MeetingTime meetingTime)
        {
            _cardinalDbContext.MeetingTimes.Add(meetingTime);
            await _cardinalDbContext.SaveChangesAsync();
        }

        public async Task UpsertInvitationAsync(Invitation invitation) 
        {
            _cardinalDbContext.Invitations.Add(invitation);
            await _cardinalDbContext.SaveChangesAsync();
        }

        public async Task DeleteInvitationAsync(string invitationId) 
        {
            var invitation = _cardinalDbContext.Invitations.Where(i => i.InvitationId == invitationId).FirstOrDefault();

            if (invitation != null) 
            {
                _cardinalDbContext.Invitations.Remove(invitation);
                await _cardinalDbContext.SaveChangesAsync();
            }
        }

        public IEnumerable<User> GetUsersForMeeting(string meetingId) 
        {
            var userIdsInMeeting = GetMeetingParticipations().Where(p => p.MeetingId == meetingId)
                                                             .Select(p => p.UserId)
                                                             .ToList();
            
            return GetUsers().Where(u => userIdsInMeeting.Contains(u.Id)).ToList();
        }

        public IEnumerable<Meeting> GetMeetingsForUser(string userId) 
        {
            var meetingIdsForuser = GetMeetingParticipations().Where(p => p.UserId == userId)
                                                              .Select(p => p.MeetingId)
                                                              .ToList();

            return GetMeetings().Where(m =>meetingIdsForuser.Contains(m.Id)).ToList();
        }

        public IEnumerable<MeetingTime> GetMeetingTimesForMeeting(string meetingId) 
        {
            return GetMeetingTimes().Where(t => t.MeetingId == meetingId).ToList();
        }

        public IEnumerable<Invitation> GetInvitationsForUser(string userId) 
        {
            return GetInvitations().Where(i => i.UserId == userId).ToList();
        }
    }
}