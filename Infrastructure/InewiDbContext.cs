using Microsoft.EntityFrameworkCore;
using Inewi_Console.Entities;

namespace Inewi_Console.Infrastructure
{
    internal class InewiDbContext : DbContext
    {
        public DbSet<Entities.Employee> Employee { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=Inewi_ConsoleDb;Trusted_Connection=True;");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>();
        }
    }
}
