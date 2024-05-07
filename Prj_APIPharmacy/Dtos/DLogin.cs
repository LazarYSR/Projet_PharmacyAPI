using System.ComponentModel.DataAnnotations;

namespace Api.Dtos;

public class DLogin
{
    [Required]
    [MinLength(3)]
    [MaxLength(50)]
    public string UsernameOrEmail { get; set; } = null!;

    [Required]
    [MinLength(3)]
    [MaxLength(50)]
    public string Password { get; set; } = null!;
}