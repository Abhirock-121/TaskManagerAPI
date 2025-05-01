using Microsoft.EntityFrameworkCore;
using TaskManagerAPI.Models;

namespace TaskManagerAPI.Data {
    public class AppDbContext : DbContext {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public virtual DbSet<User> Users => Set<User>();
        public virtual DbSet<TaskItem> Tasks => Set<TaskItem>();
        public virtual DbSet<TaskComment> TaskComments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<TaskItem>(x => {
                x.HasOne(t => t.User)
                .WithMany(u => u.Tasks)
                .HasForeignKey(t => t.UserId)
                .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<TaskComment>(x => {
                x.HasOne(c => c.TaskItem)
               .WithMany(t => t.Comments)
               .HasForeignKey(c => c.TaskItemId)
               .OnDelete(DeleteBehavior.Cascade);

                x.HasOne(c => c.User)
                .WithMany(u => u.Comments)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Restrict);

                x.Property(c => c.UserId).HasColumnName("UserId");
            });

            base.OnModelCreating(modelBuilder);

            }
        }
    }
