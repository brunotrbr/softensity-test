using Microsoft.EntityFrameworkCore;
using AccessControl.Domain.Models;

namespace AccessControl.Infrastructure
{
    public class DBContext : DbContext
    {
        public DbSet<Door> Doors;

        public DbSet<Card> Cards;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase("accessControl");
        }
    }
}
