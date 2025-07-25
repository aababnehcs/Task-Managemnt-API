using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TaskManagementAPI.Data;
using TaskManagementAPI.Models;
using TaskManagementAPI.Repositories;

namespace TaskManagementAPI.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly AppDbContext _context;

        public TaskRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<TaskItem> GetById(int id)
        {
            return await _context.Tasks
                .Include(t => t.AssignedUser) 
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<IEnumerable<TaskItem>> GetAll()
        {
            return await _context.Tasks
                .Include(t => t.AssignedUser)
                .ToListAsync();
        }

        public async Task<IEnumerable<TaskItem>> GetByAssignedUserId(int userId)
        {
            return await _context.Tasks
                .Include(t => t.AssignedUser) 
                .Where(t => t.AssignedUserId == userId)
                .ToListAsync();
        }

        public async Task<TaskItem> Add(TaskItem task)
        {
            if (task == null) throw new ArgumentNullException(nameof(task));
            
          
            var userExists = await _context.Users.AnyAsync(u => u.Id == task.AssignedUserId);
            if (!userExists) 
                throw new InvalidOperationException("Assigned user does not exist");
            
            await _context.Tasks.AddAsync(task);
            await _context.SaveChangesAsync();
            return task;
        }

        public async Task<bool> Update(TaskItem task)
        {
            if (task == null) throw new ArgumentNullException(nameof(task));
            
            var existingTask = await _context.Tasks.FindAsync(task.Id);
            if (existingTask == null) return false;
            
           
            if (existingTask.AssignedUserId != task.AssignedUserId)
            {
                var userExists = await _context.Users.AnyAsync(u => u.Id == task.AssignedUserId);
                if (!userExists) 
                    throw new InvalidOperationException("Assigned user does not exist");
            }
            
            _context.Entry(existingTask).CurrentValues.SetValues(task);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> Delete(int id)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task == null) return false;
            
            _context.Tasks.Remove(task);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}