using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace cardinal_webservices.DataModels 
{
    [Table("meeting_times")]
    public class MeetingTime
    {
        [Key, Column("meeting_time_id")]
        public string Id { get; set; }
        [ForeignKey("meetings"), Column("meeting_id")]
        public string MeetingId { get; set; }
        [Column("start_time")]
        public DateTime StartTime { get; set; }
    }
}