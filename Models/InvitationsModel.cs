using cardinal_webservices.DataModels;

namespace cardinal_webservices.Models 
{
    public class InvitationModel 
    {
        public string invitationId { get; set ;}
        public string meetingName { get; set; }
        public string meetingId { get; set; }
        public string userId { get; set; }

        public InvitationModel(Invitation invitation, Meeting meeting) 
        {
            invitationId = invitation.InvitationId;
            meetingName = meeting.Name;
            meetingId = invitation.MeetingId;
            userId = invitation.UserId;
        }
    }
}