using cardinal_webservices.DataModels;
using Microsoft.EntityFrameworkCore;

namespace cardinal_webservices.Data 
{
    public class CardinalDbContext : DbContext 
    {
        public DbSet<Meeting> Meetings { get; set; }

        public DbSet<Message> Messages { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<MeetingParticipation> MeetingParticipations { get; set; }

        public DbSet<MeetingTime> MeetingTimes { get; set; }

        public CardinalDbContext(DbContextOptions<CardinalDbContext> options) : base(options) {}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<MeetingParticipation>().HasKey(u => new 
        { 
            u.UserId, 
            u.MeetingId
        });
    }
    }
}