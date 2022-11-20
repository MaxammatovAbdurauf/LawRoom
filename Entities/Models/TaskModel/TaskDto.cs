namespace LawRoomApi.Entities.Models.TaskModel;

public class TaskDto
{
    public Guid Id { get; set; }
    public string? Title { get; set; }
    public string? TaskDescription { get; set; }
    public int? MaxScore { get; set; }
    public TaskStatus Status { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
}