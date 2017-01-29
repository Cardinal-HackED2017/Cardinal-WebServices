using cardinal_webservices.DataModels;
namespace cardinal_webservices.Models
{
    public class UserEventModel
    {
        public TimeSlotModel timeSlot {get; set;}
        public string name {get; set;}
        public string userId {get; set;}
        public string eventId {get; set;}
        public string meetingId {get; set;}
        public static UserEventModel UserEventModelFromUserEvent(UserEvent userEvent)
        {
            return new UserEventModel
            {
                name = userEvent.Name,
                userId = userEvent.userID,
                meetingId = userEvent.meetingId,

                timeSlot = new TimeSlotModel
                {
                    start = userEvent.Start,
                    length = userEvent.Length
                }
            };
        }
    }
}