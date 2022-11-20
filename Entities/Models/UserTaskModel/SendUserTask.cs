using LawRoomApi.Entities.Models.TaskModel;

namespace LawRoomApi.Entities.Models.UserTaskModel;
/// <summary>
/// bu gibrid model !!!
/// </summary>
public class SendUserTask : TaskDto
{
    public string? TaskDescription { get; set; }
    public EUserTaskStatus? TaskStatus { get; set; }
}