using System;
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
	public class UserRepository : IUserRepository
	{
		private readonly AppDbContext _context;

		public UserRepository(AppDbContext context)
		{
			_context = context;
		}

		public async Task<User> GetById(int id)
		{
			return await _context.Users.FindAsync(id);
		}

		public async Task<User> GetByUsername(string username)
		{
			return await _context.Users
				.FirstOrDefaultAsync(u => u.Username == username);
		}

		public async Task<IEnumerable<User>> GetAll()
		{
			return await _context.Users.ToListAsync();
		}

		public async Task<User> Add(User user)
		{
			if (user == null) throw new ArgumentNullException(nameof(user));
            
			await _context.Users.AddAsync(user);
			await _context.SaveChangesAsync();
			return user;
		}

		public async Task<bool> Update(User user)
		{
			if (user == null) throw new ArgumentNullException(nameof(user));
            
			var existingUser = await _context.Users.FindAsync(user.Id);
			if (existingUser == null) return false;
            
			_context.Entry(existingUser).CurrentValues.SetValues(user);
			return await _context.SaveChangesAsync() > 0;
		}

		public async Task<bool> Delete(int id)
		{
			var user = await _context.Users.FindAsync(id);
			if (user == null) return false;
            
			_context.Users.Remove(user);
			return await _context.SaveChangesAsync() > 0;
		}
	}
}