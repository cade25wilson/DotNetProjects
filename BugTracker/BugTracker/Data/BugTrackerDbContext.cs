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
                entity.HasOne(d => d.IssueClosedByNavigation)
                    .WithMany(p => p.IssueIssueClosedByNavigations)
                    .HasPrincipalKey(p => p.DisplayName)
                    .HasForeignKey(d => d.IssueClosedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Issues_UserProfile2");

                entity.HasOne(d => d.IssueCreatedByNavigation)
                    .WithMany(p => p.IssueIssueCreatedByNavigations)
                    .HasPrincipalKey(p => p.DisplayName)
                    .HasForeignKey(d => d.IssueCreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Issues_UserProfile");

                entity.HasOne(d => d.IssuePriorityNavigation)
                    .WithMany(p => p.Issues)
                    .HasPrincipalKey(p => p.IssuePriorityType)
                    .HasForeignKey(d => d.IssuePriority)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Issues_IssuePriority");

                entity.HasOne(d => d.IssueTypeNavigation)
                    .WithMany(p => p.Issues)
                    .HasPrincipalKey(p => p.IssueStatusType)
                    .HasForeignKey(d => d.IssueType)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Issues_IssueStatus");

                entity.HasOne(d => d.ProjectNavigation)
                    .WithMany(p => p.Issues)
                    .HasPrincipalKey(p => p.Name)
                    .HasForeignKey(d => d.Project)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Issues_Projects");
            });

            modelBuilder.Entity<ProjectAccess>(entity =>
            {
                entity.HasOne(d => d.Project)
                    .WithMany(p => p.ProjectAccesses)
                    .HasForeignKey(d => d.ProjectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProjectAccess_Projects");

                entity.HasOne(d => d.UserNavigation)
                    .WithMany(p => p.ProjectAccesses)
                    .HasPrincipalKey(p => p.DisplayName)
                    .HasForeignKey(d => d.User)
                    .HasConstraintName("FK_ProjectAccess_UserProfile");
            });

            modelBuilder.Entity<UserProfile>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("PK__tmp_ms_x__1788CC4C7C9531A8");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
