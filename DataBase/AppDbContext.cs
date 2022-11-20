using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using LawRoomApi.Entities;
using Task = LawRoomApi.Entities.Task;

namespace LawRoomApi.DataBase;

public class AppDbContext : IdentityDbContext<User,Role,Guid>
{
    public AppDbContext(DbContextOptions options) : base (options) { }
   
    public DbSet<Task> Tasks             { get; set; }
    public DbSet<Course> Courses         { get; set; }
    public DbSet<UserTask> UserTasks     { get; set; }
    public DbSet<UserCourse> UserCourses { get; set; }
}
