using System.ComponentModel.DataAnnotations;

namespace LawRoomApi.Entities.Models.CourceModel;

public class CreateCource
{
    [Required]
    [StringLength(20, MinimumLength = 4)]
    public string? CourceName { get; set; }
}