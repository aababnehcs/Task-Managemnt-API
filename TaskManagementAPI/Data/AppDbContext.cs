
using Microsoft.EntityFrameworkCore;
using TaskManagementAPI.Models;

namespace TaskManagementAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<TaskItem> Tasks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
          
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(u => u.Id);
                entity.Property(u => u.Username)
                    .IsRequired()
                    .HasMaxLength(50);
                
                entity.Property(u => u.Password)
                    .IsRequired()
                    .HasMaxLength(255);
                
                entity.Property(u => u.Role)
                    .IsRequired()
                    .HasConversion<string>();
                
             
                entity.HasIndex(u => u.Username)
                    .IsUnique();
            });

            modelBuilder.Entity<TaskItem>(entity =>
            {
                entity.HasKey(t => t.Id);
                entity.Property(t => t.Title)
                    .IsRequired()
                    .HasMaxLength(100);
                
                entity.Property(t => t.Description)
                    .HasMaxLength(500);
                
                entity.Property(t => t.Status)
                    .IsRequired()
                    .HasConversion<string>();
                
               
                entity.HasOne(t => t.AssignedUser)
                    .WithMany(u => u.Tasks)
                    .HasForeignKey(t => t.AssignedUserId)
                    .OnDelete(DeleteBehavior.Restrict); 
            });
        }
    }
}