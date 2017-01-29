using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace cardinal_webservices.DataModels 
{
    public class MeetingParticipation
    {
        [Key, Column("meeting_id")]
        public string MeetingId { get; set; }
        [Key, Column("user_id")]
        public string UserId { get; set; }
    }
}