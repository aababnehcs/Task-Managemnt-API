using System.ComponentModel.DataAnnotations;
using TaskManagementAPI.Models;

namespace TaskManagementAPI.Dtos
{
    public class UserCreateDto
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        public Role Role { get; set; } = Role.User;
    }
}