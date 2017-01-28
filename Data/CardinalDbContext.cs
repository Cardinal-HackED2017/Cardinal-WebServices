using Microsoft.EntityFrameworkCore;

namespace cardinal_webservices.Data 
{
    public class CardinalDbContext : DbContext 
    {
        public DbSet<Test> test { get; set; }

        public CardinalDbContext(DbContextOptions<CardinalDbContext> options) : base(options) {}
    }
}