using System.Collections.Generic;
using System.Threading.Tasks;
using cardinal_webservices.DataModels;

namespace cardinal_webservices.Data 
{
    public interface ICardinalDataService 
    {
        IEnumerable<Meeting> GetMeetings();

        Task UpsertMeetingAsync(Meeting meeting);
    }
}