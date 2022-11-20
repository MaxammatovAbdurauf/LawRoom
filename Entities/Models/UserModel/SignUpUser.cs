using LawRoomApi.Filters;
using System.ComponentModel.DataAnnotations;

namespace LawRoomApi.Entities.Models.UserModel;

public class SignUpUser
{
    [Required]
    [StringLength(20, MinimumLength = 4)]
    public string? UserName { get; set; }

    [Required]
    [StringLength(20, MinimumLength = 4)]
    public string? FirstName { get; set; }

    [StringLength(20, MinimumLength = 4)]
    public string? LastName { get; set; }

    [Required]
    [StringLength(20, MinimumLength = 6)]
    public string? Password { get; set; }

    [Required]
    [StringLength(20, MinimumLength = 6)]
    public string? ConfirmPassword { get; set; }

    [Required]
    //[EmailFilter]
    public string? Email { get; set; }
}
