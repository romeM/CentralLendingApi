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

        public virtual DbSet<Person> Person { get; set; }
        public virtual DbSet<PersonMonthlyStatistics> PersonMonthlyStatistics { get; set; }
        public virtual DbSet<PersonProject> PersonProject { get; set; }
        public virtual DbSet<Project> Project { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>(entity =>
            {
                entity.Property(e => e.Address).HasMaxLength(255);

                entity.Property(e => e.City).HasMaxLength(255);

                entity.Property(e => e.Country).HasMaxLength(255);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.PasswordHash).IsRequired();

                entity.Property(e => e.PasswordSalt).IsRequired();

                entity.Property(e => e.PostalCode).HasMaxLength(10);

                entity.Property(e => e.UpdatedOn).HasColumnType("datetime");

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<PersonMonthlyStatistics>(entity =>
            {
                entity.ToTable("Person_MonthlyStatistics");

                entity.Property(e => e.Date).HasColumnType("date");

                entity.Property(e => e.Ipmt)
                    .HasColumnName("IPMT")
                    .HasColumnType("decimal(10, 2)");

                entity.Property(e => e.PersonId).HasColumnName("Person_Id");

                entity.Property(e => e.Pmt)
                    .HasColumnName("PMT")
                    .HasColumnType("decimal(10, 2)");

                entity.Property(e => e.Ppmt)
                    .HasColumnName("PPMT")
                    .HasColumnType("decimal(10, 2)");

                entity.HasOne(d => d.Person)
                    .WithMany(p => p.PersonMonthlyStatistics)
                    .HasForeignKey(d => d.PersonId)
                    .HasConstraintName("FK_Person_MonthlyStatistics_Person");
            });

            modelBuilder.Entity<PersonProject>(entity =>
            {
                entity.ToTable("Person_Project");

                entity.Property(e => e.PersonId).HasColumnName("Person_Id");

                entity.Property(e => e.ProjectId).HasColumnName("Project_Id");

                entity.Property(e => e.StartDate).HasColumnType("date");

                entity.HasOne(d => d.Person)
                    .WithMany(p => p.PersonProject)
                    .HasForeignKey(d => d.PersonId)
                    .HasConstraintName("FK_Person_Project_Person");

                entity.HasOne(d => d.Project)
                    .WithMany(p => p.PersonProject)
                    .HasForeignKey(d => d.ProjectId)
                    .HasConstraintName("FK_Person_Project_Project");
            });

            modelBuilder.Entity<Project>(entity =>
            {
                entity.Property(e => e.PollDate).HasColumnType("datetime");

                entity.Property(e => e.StartDate).HasColumnType("datetime");
            });
        }
    }
}
