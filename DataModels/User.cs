using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace cardinal_webservices.DataModels 
{
    [Table("users")]
    public class User 
    {
        [Key, Column("user_id")]
        public string Id { get; set; }
        [Column("display_name")]
        public string DisplayName { get; set; }
        [Column("email")]
        public string Email { get; set; }
    }
}