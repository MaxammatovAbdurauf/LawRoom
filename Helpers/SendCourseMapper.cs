using LawRoomApi.Entities;
using LawRoomApi.Entities.Models;
using LawRoomApi.Entities.Models.CourceModel;
using Mapster;

namespace LawRoomApi.Helpers;

public static class SendCourseMapper
{
    public static SendCourseDto ConvertToSendUserDto (this Course course)
    {
        return new SendCourseDto
        {
            Id = course.Id,
            CourceName = course.CourceName,
            Key = course.Key,
            Users = course.UserCourseList?.Select(userCourse => userCourse.User.Adapt<SendUser>()).ToList(),
        };
    }
}