using System.ComponentModel.DataAnnotations;
using Api.Enums;

namespace Api.Dtos;

public class DRegister
{
    [Required]
    public string Name { get; set; } = null!;
    
    [Required]
    public string Phone { get; set; } = null!;
    
    [Required]
    [EmailAddress]
    public string Email { get; set; } = null!;
    
    [Required]
    public string Address { get; set; } = null!;
    
    public string? Username { get; set; } = null!;
    
    [Required]
    [MinLength(3)]
    [MaxLength(50)]
    public string Password { get; set; } = null!;

    [Required]
    [MinLength(3)]
    [MaxLength(50)]
    [Compare("Password")]
    public string ConfirmPassword { get; set; } = null!;

    
    [Required]
    public UserRole Role { get; set; }
    
    public Guid? PharmacyId { get; set; }

    [Required]
    public int CostPerKM { get; set; }
}