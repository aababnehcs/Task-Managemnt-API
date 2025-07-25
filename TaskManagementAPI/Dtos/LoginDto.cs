using System.ComponentModel.DataAnnotations;
using TaskManagementAPI.Models;

public class LoginDto
{
    public string Username { get; set; }
    public string Password { get; set; }
}