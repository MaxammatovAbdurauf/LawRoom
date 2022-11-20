using LawRoomApi.DataBase;
using LawRoomApi.Entities;
using LawRoomApi.Entities.Models;
using LawRoomApi.Entities.Models.UserTaskModel;
using LawRoomApi.Helpers;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LawRoomApi.Controllers;

[Route("Api/[controller]")]
[ApiController]
[Authorize]
public class ProfileController : Controller
{
    private readonly AppDbContext context;
    private readonly UserManager<User> userManager;

    public ProfileController (AppDbContext _context, UserManager<User> _userManager)
    {
        context = _context;
        userManager = _userManager;
    }


    [HttpGet("{UserName}")]
    public async Task<IActionResult> Profile(string UserName)
    {
        var user = await userManager.GetUserAsync(User);
        if (user.UserName != UserName) return BadRequest();
        return Ok(user.Adapt<SendUser>());
    }


    [HttpGet("Courses")]
    public async Task <IActionResult> GetUserCourses ()
    {
        var user =await userManager.GetUserAsync(User);

        var courses = user.UserCourseList?.Select(c => c.Course?.ConvertToSendUserDto()).ToList();
        if (courses == null) return Ok("you have no course");
        return Ok(courses);
    }


    [HttpGet("{courseId}/tasks/user")]
    public async Task<IActionResult> GetUserTasksInCourse (Guid courseId)
    {
        var course = await context.Courses.FirstOrDefaultAsync(c => c.Id == courseId);
        if (course is null)
            return NotFound("courseId is incorrect");

        var user = await userManager.GetUserAsync(User);
        if (course.UserCourseList?.Any(uc => uc.UserId == user.Id) is not true)
            return BadRequest("you do not join in this course");

        if (course.Tasklist is null)
            return NotFound("till no tasks in this course");

        var sendUserTaskList = new List<SendUserTask>();

        foreach (var task in course.Tasklist)
        {
            var sendUserTask = task.Adapt<SendUserTask>();

            var oldSendUserUser = task.UserTasks.FirstOrDefault(u => u.UserId == user.Id);
            if (oldSendUserUser is not null)
            {
                sendUserTask.TaskDescription = oldSendUserUser.Description;
                sendUserTask.TaskStatus = oldSendUserUser.TaskStatus;
            }
            sendUserTaskList.Add(sendUserTask);
        }
        return Ok(sendUserTaskList);
    }


    [HttpGet("{courseId}/tasks/all")]
    public async Task<IActionResult> GetUserTasks()
    {
       
        var user = await userManager.GetUserAsync(User);
        var userTaskList = context.UserTasks.Where(uc => uc.UserId == user.Id).ToList();
        if (userTaskList is null) return BadRequest("you have no task");

        var sendUserTaskList = new List<SendUserTask>();

        foreach (var userTask in userTaskList)
        {
            var task = await context.Tasks.FirstOrDefaultAsync(t => t.Id == userTask.TaskId);
            if (task is not null)
            {
                var sendUserTask = task.Adapt<SendUserTask>();
                sendUserTask.TaskDescription = userTask.Description;
                sendUserTask.TaskStatus = userTask.TaskStatus;

                sendUserTaskList.Add(sendUserTask);
            }
        }
        return Ok(sendUserTaskList);
    }

   /* [HttpGet("{courseId}/tasks/{userTaskId}")]
    public async Task<IActionResult> GetUserTask(Guid userTaskId)
    {
        var user = await userManager.GetUserAsync(User);
        var userTaskList = context.UserTasks.Where(uc => uc.UserId == user.Id).ToList();
        if (userTaskList is null) return BadRequest("you have no task");

        var sendUserTaskList = new List<SendUserTask>();

        foreach (var userTask in userTaskList)
        {
            var task = await context.Tasks.FirstOrDefaultAsync(t => t.Id == userTask.TaskId);
            if (task is not null)
            {
                var sendUserTask = task.Adapt<SendUserTask>();
                sendUserTask.TaskDescription = userTask.Description;
                sendUserTask.TaskStatus = userTask.TaskStatus;

                sendUserTaskList.Add(sendUserTask);
            }
        }
        return Ok(sendUserTaskList);
    }*/ // not ready
    
    [HttpPost("{courseId}/tasks/{taskId}/add")]
    public async Task <IActionResult> AddUserTaskResult(Guid courseId, Guid taskId, [FromBody] CreateUserTask createUserTask)
    {
        if (!ModelState.IsValid) return BadRequest("invalid values");
        
        var course = await context.Courses.FirstOrDefaultAsync(c =>c.Id == courseId);
        if (course is null)      return NotFound("courseId is incorrect");

        var user   = await userManager.GetUserAsync(User);
        if (course.UserCourseList?.Any(uc => uc.UserId == user.Id) != true)
            return BadRequest ("you do not join in this course");

        var currentTask = course.Tasklist?.FirstOrDefault(ut => ut.Id == taskId);
        if (currentTask is null)  return NotFound("not found this task in this course");

        var task = createUserTask.Adapt<UserTask> ();
        var id = Guid.NewGuid();
        task.Id = id;
        task.TaskId = currentTask.Id;
        task.UserId = user.Id;

        await context.UserTasks.AddAsync(task);
        await context.SaveChangesAsync();

        return Ok ("successfully added:\n" + id);
    }
}