using TaskManagementAPI.Models;

namespace TaskManagementAPI.Dtos
{
    public class TaskUpdateDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public TaskItemStatus Status { get; set; }
        public int? AssignedUserId { get; set; }
    }
}