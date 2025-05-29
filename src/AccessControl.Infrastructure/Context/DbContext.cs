using Microsoft.EntityFrameworkCore;
using AccessControl.Domain.Models;

namespace AccessControl.Infrastructure
{
    public class DBContext : DbContext
    {
        public DbSet<Door> Doors { get; set; }

        public DbSet<Card> Cards { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase("AccessControl");
        }
    }
}
