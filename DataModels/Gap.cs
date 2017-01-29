using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace cardinal_webservices.DataModels 
{
    [Table("gaps")]
    public class Gap
    {
        [Key, Column("gap_id")]
        public string GapId { get; set; }
        [ForeignKey("meetings"), Column("meeting_id")]
        public string MeetingId { get; set; }
        [Column("start")]
        public DateTime start { get; set; }
        [Column("length")]
        public TimeSpan length {get; set;}
    }
}