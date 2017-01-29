using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace cardinal_webservices.DataModels 
{
    [Table("meetings")]
    public class Meeting 
    {
        [Key, Column("meeting_id")]
        public string Id { get; set; }
        [Column("name")]
        public string Name { get; set; }
        [ColumnAttribute("description")]
        public string Description { get; set; }
        [Column("create_time")]
        public DateTime CreatedTime { get; set; }
        [Column("start_fence")]
        public DateTime StartFence { get; set; }
        [Column("end_fence")]
        public DateTime EndFence { get; set; }
        [Column("day_start")]
        public TimeSpan dayStart {get; set;}
        [Column("day_end")]
        public TimeSpan dayEnd {get; set;}
        [Column("length")]
        public TimeSpan Length { get; set; }
        [Column("longitude")]
        public double Longitude { get; set; }
        [Column("latitude")]
        public double Latitude { get; set; }
        [Column("location_description")]
        public string LocationDescription { get; set; }
    }
}