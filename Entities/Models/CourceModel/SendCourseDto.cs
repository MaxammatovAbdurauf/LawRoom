namespace LawRoomApi.Entities.Models.CourceModel;

public class SendCourseDto
{
    public Guid Id { get; set; }
    public string? CourceName { get; set; }
    public string? Key { get; set; }
    public List <SendUser>? Users { get; set; }
}