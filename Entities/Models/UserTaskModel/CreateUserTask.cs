using System.ComponentModel.DataAnnotations;

namespace LawRoomApi.Entities.Models.UserTaskModel;

public class CreateUserTask
{
    [Required]
    public string? Description        { get; set; }
    [Required]
    public EUserTaskStatus TaskStatus { get; set; }
}