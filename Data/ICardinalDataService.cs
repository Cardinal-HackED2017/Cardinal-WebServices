using System.Collections.Generic;
using System.Threading.Tasks;
using cardinal_webservices.DataModels;

namespace cardinal_webservices.Data 
{
    public interface ICardinalDataService 
    {
        IEnumerable<Meeting> GetMeetings();

        IEnumerable<Message> GetMessages();

        //IEnumerable<MeetingParticipation> GetMeetingParticipations();

        //IEnumerable<MeetingTime> GetMeetingTimes();

        IEnumerable<User> GetUsers();

        Task UpsertMeetingAsync(Meeting meeting);

        Task UpsertMessageAsync(Message message);

        Task UpsertUserAsync(User user);
    }
}