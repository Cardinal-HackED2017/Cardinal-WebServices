using cardinal_webservices.DataModels;
using Microsoft.EntityFrameworkCore;

namespace cardinal_webservices.Data 
{
    public class CardinalDbContext : DbContext 
    {
        public DbSet<Meeting> Meetings { get; set; }

        public CardinalDbContext(DbContextOptions<CardinalDbContext> options) : base(options) {}
    }
}