using EventsApi.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace EventsApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options) 
        {

        }

        public DbSet<Event> Events { get; set; }
    }
}
