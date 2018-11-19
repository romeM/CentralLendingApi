using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CentralLendingApi.Data.Models
{
    public partial class CentralLendingContext : DbContext
    {
        public CentralLendingContext()
        {
        }

        public CentralLendingContext(DbContextOptions<CentralLendingContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Project> Project { get; set; }
        public virtual DbSet<User> User { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
    }
}
