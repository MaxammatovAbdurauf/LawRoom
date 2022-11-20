using LawRoomApi.DataBase;
using LawRoomApi.Entities.Models.CourceModel;
using LawRoomApi.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LawRoomApi.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using LawRoomApi.Entities.Models.TaskModel;

namespace LawRoomApi.Controllers;

[Route("Api/[controller]")]
[ApiController]
[Authorize]
public partial class CourseController : ControllerBase
{
    private readonly AppDbContext context;
    private readonly UserManager  <User> userManager;

    public CourseController(AppDbContext _context, UserManager<User> _userManager)
    {
        context = _context;
        userManager = _userManager;
    }

    [HttpPost]
    public async Task<IActionResult> CreateCourse([FromBody]CreateCource createCource)
    {
        if (!ModelState.IsValid)
            return BadRequest();

        var key = GenerateRandomString.RandomString();
        var id = Guid.NewGuid();
        var user = await userManager.GetUserAsync(User);

        var newCourse = new Course
        {
            Id = id,
            CourceName = createCource.CourceName,
            Key = key,

            UserCourseList = new List<UserCourse>
            {
                new UserCourse
                {
                    CourseId = id,
                    UserId = user.Id,
                    IsAdmin = true
                }
            }
        };

        await context.Courses.AddAsync(newCourse);
        await context.SaveChangesAsync();
        var course = await context.Courses.FirstAsync(c => c.Key == key);
        var currentCourse = course.ConvertToSendUserDto();

        return Ok(currentCourse);
    }

    [HttpGet("{Id}")]
    public async Task<IActionResult> GetCourceById(Guid Id)
    {
        var course = await context.Courses.FirstOrDefaultAsync(x => x.Id == Id);
        if (course is null) return NotFound();
        var currentCourse = course.ConvertToSendUserDto();
        return Ok(currentCourse);
    }

    [HttpGet]
    public async Task<IActionResult> GetCource()
    {
        var courses = await context.Courses.Select(c => c.ConvertToSendUserDto()).ToListAsync();
        return Ok(courses);
    }

    [HttpPut("{Id}")]
    public async Task<IActionResult> UpdateCource(Guid Id,[FromBody] UpdateCource updateCource)
    {
        var course = await context.Courses.FirstOrDefaultAsync(x => x.Id == Id);
        if (course is null) return NotFound();
        course.CourceName = updateCource.CourseName;
        await context.SaveChangesAsync();
        return Ok();
    }

    [HttpDelete("{Id}")]
    public async Task<IActionResult> DeleteCource(Guid Id)
    {
        if (!await context.Courses.AnyAsync(x => x.Id == Id)) return NotFound();

        var course = await context.Courses.FirstAsync(x => x.Id == Id);
        context.Courses.Remove(course);
        await context.SaveChangesAsync();
        return Ok();
    }
    
    [HttpGet("{Id}/join")]
    public async Task<IActionResult> JointoCourse(Guid CourseId)
    {
        var cource = await context.Courses.FirstOrDefaultAsync(c => c.Id == CourseId);
        if (cource is null) return NotFound();

        var user = await userManager.GetUserAsync(User);
        if (cource.UserCourseList!.Any(u => u.UserId == user.Id)) return BadRequest("you have already join");

        cource.UserCourseList ??= new List<UserCourse>();
        var userCourse = new UserCourse
        {
            UserId = user.Id,
            CourseId = cource.Id,
            IsAdmin = false
        };
        await context.UserCourses.AddAsync(userCourse);
        await context.SaveChangesAsync();
        return Ok("you have joined");
    }
}