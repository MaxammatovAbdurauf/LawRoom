using LawRoomApi.Entities.Models.TaskModel;
using LawRoomApi.Helpers;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LawRoomApi.Controllers;

public partial class CourseController : ControllerBase
{
    [HttpPost("/{courseId}/Task")]
    public async Task<IActionResult> CreateTask(Guid courseId, [FromBody] CreateTaskDto createTaskDto)
    {
        if (!ModelState.IsValid)
            return BadRequest();

        var course = await context.Courses.FirstOrDefaultAsync(c => c.Id == courseId);
        if (course == null) 
            return NotFound("Course not exist");

        var user = await userManager.GetUserAsync(User);
       
        //if (course.UserCourseList?.Any(uc => uc.Id == user.Id && uc.IsAdmin) is null) bu mutlaqo xato
       if (course.UserCourseList!.Any(uc => uc.UserId == user.Id && uc.IsAdmin == true) != true)
         return BadRequest("you are not admin in this group");

        var task = createTaskDto.Adapt<Entities.Task>();
        task.Id = Guid.NewGuid();
        task.CourseId = courseId;
        task.CreateDate = DateTime.Now;

        await context.Tasks.AddAsync(task);
        await context.SaveChangesAsync();
       
        return Ok(task.Adapt<TaskDto>());
    }


    [HttpGet("/{courseId}/Tasks")]
    public async Task<IActionResult> GetTasks(Guid courseId)
    {
        var course = await context.Courses.FirstOrDefaultAsync (c => c.Id == courseId);
        if (course is null)
            return NotFound("courseId is incorrect or course do not exist");

        var tasks = course.Tasklist?.Select(c => c.Adapt<TaskDto>()).ToList();
        if (tasks is null)
            return NotFound("tasks do not exist in this course");

        return Ok(tasks);
    }
    
    [HttpGet("/{courseId}/Task/{taskId}")]
    public async Task<IActionResult> GetTaskById(Guid courseId, Guid taskId)
    {
        var task = await context.Tasks.FirstOrDefaultAsync(t => t.Id == taskId && t.CourseId == courseId);
        if (task is null)
            return NotFound("taskId is incorrect or task does not exist in this course");

        return Ok(task.Adapt<TaskDto>());
    }


    [HttpPut("/{courseId}/Task/{taskId}")]
    public async Task<IActionResult> UpdateTask(Guid courseId, Guid taskId,[FromBody] UpdateTaskDto updateTaskDto)
    {
        var task = await context.Tasks.FirstOrDefaultAsync(uc => uc.Id == taskId && uc.CourseId == courseId);
        if (task is null)
            return NotFound("taskId is incorrect or task does not exist in this course");

        task.ConvertToTask(updateTaskDto);
        await context.SaveChangesAsync();

        return Ok("task is updated");
    }


    [HttpDelete("/{courseId}/Task/{taskId}")]
    public async Task<IActionResult> DeleteTask(Guid courseId, Guid taskId)
    {
        var task = await context.Tasks.FirstOrDefaultAsync(t => t.Id == taskId);
        if (task is null) return NotFound();

        context.Tasks.Remove(task);
        await context.SaveChangesAsync();
        return Ok("task is deleted");
    }
}
