namespace LawRoomApi.Entities;

public class Course
{
    public Guid Id { get; set; }
    public string?  CourceName { get; set; }
    public string? Key { get; set; }
    public virtual List<UserCourse>? UserCourseList { get; set; }
    public virtual List <Task>? Tasklist { get; set; }
}
