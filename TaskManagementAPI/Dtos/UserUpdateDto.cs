using TaskManagementAPI.Models;

namespace TaskManagementAPI.Dtos
{
    public class UserUpdateDto
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public Role? Role { get; set; }
    }
}