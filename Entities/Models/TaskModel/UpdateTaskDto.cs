using System.ComponentModel.DataAnnotations;

namespace LawRoomApi.Entities.Models.TaskModel;

public class UpdateTaskDto
{
    [Required]
    public string? Title { get; set; }
    public string? Description { get; set; }
    public int MaxScore { get; set; }
    public TaskStatus Status { get; set; }
    public DateTime StartDate{ get; set; }
    public DateTime EndDate { get; set; }
}