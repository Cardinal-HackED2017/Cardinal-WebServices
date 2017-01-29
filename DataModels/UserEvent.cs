using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace cardinal_webservices.DataModels 
{
    [Table("events")]
    public class UserEvent 
    {
        [Key, Column("event_id")]
        public string Id { get; set; }
        [Column("meeting_id")]
        public string meetingId{get; set;}
        [Column("user_id")]
        public string userID {get; set;}
        [Column("name")]
        public string Name { get; set; }
        [Column("start")]
        public DateTime Start {get; set;}
        [Column("length")]
        public TimeSpan Length { get; set; }
        
    }
}