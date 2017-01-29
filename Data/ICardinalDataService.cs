using System.Collections.Generic;
using System.Threading.Tasks;
using cardinal_webservices.DataModels;

namespace cardinal_webservices.Data 
{
    public interface ICardinalDataService 
    {
        IEnumerable<Meeting> GetMeetings();

        IEnumerable<Message> GetMessages();

        IEnumerable<MeetingParticipation> GetMeetingParticipations();

        IEnumerable<MeetingTime> GetMeetingTimes();

        IEnumerable<User> GetUsers();

        Task UpsertMeetingAsync(Meeting meeting);

        Task UpsertMeetingParticipationAsync(MeetingParticipation meetingParticipation);

        Task UpsertMessageAsync(Message message);

        Task UpsertUserAsync(User user);

        Task UpsertMeetingTimeAsync(MeetingTime meetingTime);

        IEnumerable<User> GetUsersForMeeting(string meetingId);

        IEnumerable<Meeting> GetMeetingsForUser(string userId);

        IEnumerable<MeetingTime> GetMeetingTimesForMeeting(string meetingId);
    }
}