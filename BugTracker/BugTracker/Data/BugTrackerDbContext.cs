using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using BugTracker.Models;

namespace BugTracker.Data
{
    public partial class BugTrackerDbContext : DbContext
    {
        public BugTrackerDbContext()
        {
        }

        public BugTrackerDbContext(DbContextOptions<BugTrackerDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Issue> Issues { get; set; } = null!;
        public virtual DbSet<IssuePriority> IssuePriorities { get; set; } = null!;
        public virtual DbSet<IssueStatus> IssueStatuses { get; set; } = null!;
        public virtual DbSet<Project> Projects { get; set; } = null!;
        public virtual DbSet<ProjectAccess> ProjectAccesses { get; set; } = null!;
        public virtual DbSet<UserProfile> UserProfiles { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=BugTrackerDb;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Issue>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.HasOne(d => d.IssueClosedByNavigation)
                    .WithMany(p => p.IssueIssueClosedByNavigations)
                    .HasForeignKey(d => d.IssueClosedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Issues_UserProfile2");

                entity.HasOne(d => d.IssueCreatedByNavigation)
                    .WithMany(p => p.IssueIssueCreatedByNavigations)
                    .HasForeignKey(d => d.IssueCreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Issues_UserProfile");

                entity.HasOne(d => d.IssueResolutionSummaryNavigation)
                    .WithMany(p => p.Issues)
                    .HasForeignKey(d => d.IssueResolutionSummary)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Issues_IssueStatus");

                entity.HasOne(d => d.IssueTypeNavigation)
                    .WithMany(p => p.Issues)
                    .HasForeignKey(d => d.IssueType)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Issues_IssuePriority");
            });

            modelBuilder.Entity<IssuePriority>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();
            });

            modelBuilder.Entity<IssueStatus>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();
            });

            modelBuilder.Entity<Project>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Description).IsFixedLength();
            });

            modelBuilder.Entity<ProjectAccess>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.HasOne(d => d.Project)
                    .WithMany(p => p.ProjectAccesses)
                    .HasForeignKey(d => d.ProjectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProjectAccess_Projects");

                entity.HasOne(d => d.UserNavigation)
                    .WithMany(p => p.ProjectAccesses)
                    .HasForeignKey(d => d.User)
                    .HasConstraintName("FK_ProjectAccess_UserProfile");
            });

            modelBuilder.Entity<UserProfile>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("PK__tmp_ms_x__1788CC4C5F4B7976");

                entity.Property(e => e.UserId).ValueGeneratedNever();
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
