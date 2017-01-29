using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace cardinal_webservices.DataModels 
{
    [Table("attendees")]
    public class MeetingParticipation
    {
        [ForeignKey("meetings"), Column("meeting_id")]
        public string MeetingId { get; set; }
        [ForeignKey("users"), Column("user_id")]
        public string UserId { get; set; }
    }
}