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
        public virtual DbSet<UserMonthlyStatistics> UserMonthlyStatistics { get; set; }
        public virtual DbSet<UserProject> UserProject { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source=APILAPRMA01;Initial Catalog=CentralLending;Integrated Security=True;Persist Security Info=False;Pooling=False;MultipleActiveResultSets=False;Connect Timeout=60;Encrypt=False;TrustServerCertificate=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
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

                entity.Property(e => e.UpdatedOn).HasColumnType("datetime");

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<UserMonthlyStatistics>(entity =>
            {
                entity.ToTable("User_MonthlyStatistics");

                entity.Property(e => e.Date).HasColumnType("date");

                entity.Property(e => e.Ipmt)
                    .HasColumnName("IPMT")
                    .HasColumnType("decimal(10, 2)");

                entity.Property(e => e.Pmt)
                    .HasColumnName("PMT")
                    .HasColumnType("decimal(10, 2)");

                entity.Property(e => e.Ppmt)
                    .HasColumnName("PPMT")
                    .HasColumnType("decimal(10, 2)");

                entity.Property(e => e.UserId).HasColumnName("User_Id");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserMonthlyStatistics)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_User_MonthlyStatistics_User");
            });

            modelBuilder.Entity<UserProject>(entity =>
            {
                entity.ToTable("User_Project");

                entity.Property(e => e.ProjectId).HasColumnName("Project_Id");

                entity.Property(e => e.ProjectStartDate).HasColumnType("date");

                entity.Property(e => e.UserId).HasColumnName("User_Id");

                entity.HasOne(d => d.Project)
                    .WithMany(p => p.UserProject)
                    .HasForeignKey(d => d.ProjectId)
                    .HasConstraintName("FK_User_Project_Project");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserProject)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_User_Project_User");
            });
        }
    }
}
