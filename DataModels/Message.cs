using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace cardinal_webservices.DataModels 
{
    public class Message 
    {
        [Key, Column("message_id")]
        public string MessageId { get; set; }
        [Key, Column("meeting_id")]
        public string MeetingId { get; set; }
        [Key, Column("user_id")]
        public string UserId { get; set; }
        [Column("content")]
        public string content {get; set; }
        [Column("created_time")]
        public DateTime CreatedTime;
    }
}