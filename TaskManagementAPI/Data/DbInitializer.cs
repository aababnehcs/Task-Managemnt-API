using Microsoft.EntityFrameworkCore;
using TaskManagementAPI.Models;
using TaskManagementAPI.Services;

namespace TaskManagementAPI.Data
{
    public static class DbInitializer
    {
        public static void Initialize(AppDbContext context, IPasswordHasher passwordHasher)
        {
        
            context.Database.Migrate();

            // Seed Users
            if (!context.Users.Any())
            {
                var users = new User[]
                {
                    new User 
                    { 
                        Username = "admin", 
                        Password = passwordHasher.HashPassword("Admin@123"), 
                        Role = Role.Admin 
                    },
                    new User 
                    { 
                        Username = "user", 
                        Password = passwordHasher.HashPassword("User@123"), 
                        Role = Role.User 
                    }
                };

                context.Users.AddRange(users);
                context.SaveChanges();
            }

            
            if (!context.Tasks.Any() && context.Users.Any())
            {
                var admin = context.Users.First(u => u.Username == "admin");
                var regularUser = context.Users.First(u => u.Username == "user");

                var tasks = new TaskItem[]
                {
                    new TaskItem
                    {
                        Title = "Setup Database",
                        Description = "Initialize the database with seed data",
                        Status = TaskItemStatus.Completed,
                        AssignedUserId = admin.Id,
                   
                    },
                    new TaskItem
                    {
                        Title = "Implement Authentication",
                        Description = "Add JWT authentication to the API",
                        Status = TaskItemStatus.InProgress,
                        AssignedUserId = admin.Id,
                     
                    },
                    new TaskItem
                    {
                        Title = "Create Frontend UI",
                        Description = "Build React components for task management",
                        Status = TaskItemStatus.Pending,
                        AssignedUserId = regularUser.Id,
                       
                    }
                };

                context.Tasks.AddRange(tasks);
                context.SaveChanges();
            }
        }
    }
}