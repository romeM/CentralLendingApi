using CentralLendingApi.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace CentralLendingApi.Data
{
    public class CentralLendingApiContext : DbContext
    {
        public CentralLendingApiContext(DbContextOptions<CentralLendingApiContext> options)
            : base(options)
        { }

        public DbSet<Project> Projects { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Project>().ToTable("Project");
        }
    }
}
