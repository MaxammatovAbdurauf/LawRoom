using System.ComponentModel.DataAnnotations;

namespace LawRoomApi.Entities.Models.CourceModel;

public class UpdateCource
{
    [Required]
    [StringLength(20, MinimumLength = 4)]
    public string? CourseName { get; set; }
}
