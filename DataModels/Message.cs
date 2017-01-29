using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace cardinal_webservices.DataModels 
{
    [Table("messages")]
    public class Message 
    {
        [Column("message_id")]
        public string MessageId { get; set; }
        [Column("meeting_id")]
        public string MeetingId { get; set; }
        [Column("user_id")]
        public string UserId { get; set; }
        [Column("content")]
        public string content {get; set; }
        [Column("created_time")]
        public DateTime CreatedTime{get; set;}
    }
}