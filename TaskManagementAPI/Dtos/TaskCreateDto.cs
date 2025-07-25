using System.ComponentModel.DataAnnotations;
using TaskManagementAPI.Models;

public class TaskCreateDto
{
    [Required]
    public string Title { get; set; }
   
    public TaskItemStatus Status { get; set; } 

    public string Description { get; set; }

    [Required]
    public int AssignedUserId { get; set; }
}