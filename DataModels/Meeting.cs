using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace cardinal_webservices.DataModels 
{
    public class Meeting 
    {
        [Key, Column("meeting_id")]
        public Guid Id { get; set; }
        [Column("name")]
        public string Name { get; set; }
        [Column("created_time")]
        public DateTime CreatedTime { get; set; }
        [Column("start_fence")]
        public DateTime StartFence { get; set; }
        [Column("end_fence")]
        public DateTime EndFence { get; set; }
        [Column("length")]
        public DateTime Length { get; set; }
        [Column("longitude")]
        public double Longitude { get; set; }
        [Column("latitude")]
        public double Latitude { get; set; }
        [Column("location_description")]
        public string LocationDescription { get; set; }
    }
}