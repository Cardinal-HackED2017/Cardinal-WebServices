using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace cardinal_webservices.DataModels 
{
    [Table("MeetingTimes")]
    public class MeetingTime
    {
        [Key, Column("meeting_time_id")]
        public Guid Id { get; set; }
        [Key, Column("meeting_id")]
        public Guid MeetingId { get; set; }
        [Column("start_time")]
        public DateTime StartTime { get; set; }
    }
}