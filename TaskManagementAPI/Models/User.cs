using System;
using System.Data;

namespace TaskManagementAPI.Models
{
	public class User
	{
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public Role Role { get; set; }
        public ICollection<TaskItem> Tasks { get; set; }

    }
    public enum Role
    {
        Admin,
        User
    }
}

