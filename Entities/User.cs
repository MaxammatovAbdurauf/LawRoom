using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using LawRoomApi.Filters;

namespace LawRoomApi.Entities;

public class User : IdentityUser<Guid>
{ 
    //public string Email { get; set; }
    public string? FirstName      { get; set; }
    public string? LastName       { get; set; }
    public string? Password       { get; set; }
    public virtual List <UserCourse>? UserCourseList { get; set; }
    public virtual List <UserTask>? UserTaskList     { get; set; }
}