using LawRoomApi.Entities.Models.TaskModel;
using Task = LawRoomApi.Entities.Task;

namespace LawRoomApi.Helpers;

public static class UpdateTaskMapper
{
    public static void ConvertToTask (this Task task, UpdateTaskDto updateTaskDto)
    {
        task.Title       = updateTaskDto.Title;
        task.TaskDescription = updateTaskDto.Description;
        task.EndDate     = updateTaskDto.EndDate;
        task.StartDate   = updateTaskDto.StartDate;
        task.Status      = updateTaskDto.Status;
        task.MaxScore    = updateTaskDto.MaxScore;
    }
}