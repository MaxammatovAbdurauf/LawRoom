using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace LawRoomApi.Entities;

public class Task
{
    public Guid Id { get; set; }
    public string? Title { get; set; }
    public string? TaskDescription { get; set; }
    public int MaxScore { get; set; }
    public TaskStatus Status { get; set; }
    public DateTime CreateDate { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }

    public Guid CourseId { get; set; }
    [ForeignKey(nameof(CourseId))]
    public virtual Course? course { get; set; }
    public virtual List<UserTask> UserTasks { get; set; }
}

public enum TaskStatus
{
    created,
    delayed,
    finished,
}