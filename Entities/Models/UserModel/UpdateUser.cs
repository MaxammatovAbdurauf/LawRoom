using System.ComponentModel.DataAnnotations;

namespace LawRoomApi.Entities.Models.UserModel;

public class UpdateUser
{
    
    [StringLength(20, MinimumLength = 4)]
    public string? UserName { get; set; }
   
    [StringLength(20, MinimumLength = 4)]
    public string? FirstName { get; set; }

    [StringLength(20, MinimumLength = 4)]
    public string? LastName { get; set; }
 
    [StringLength(20, MinimumLength = 6)]
    public string? Password { get; set; }
}