using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ideaport.Models
{
    public partial class SystemContext : DbContext
    {
        public SystemContext()
        {
        }

        public SystemContext(DbContextOptions<SystemContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Client> Client { get; set; }
        public virtual DbSet<Task> Task { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<UserTask> UserTasks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Client>();

            modelBuilder.Entity<Task>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<UserTask>(entity =>
            {
                entity.HasKey(e => new { e.TaskId, e.UserId });
            });

            modelBuilder.Entity<UserTask>()
                .HasOne(pt => pt.User)
                .WithMany(p => p.UserTasks)
                .HasForeignKey(pt => pt.UserId);

            modelBuilder.Entity<UserTask>()
                .HasOne(pt => pt.Task)
                .WithMany(t => t.UserTasks)
                .HasForeignKey(pt => pt.TaskId);
        }
    }
}
