using Api.Enums;
using System.ComponentModel.DataAnnotations;

namespace Api.Models;

public class User
{
  
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;
    public string Phone { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Address { get; set; } = null!;

    public string? Username { get; set; }
    public string Password { get; set; } = null!;

    public UserRole Role { get; set; }

    public Guid? PharmacyId { get; set; }
    public int CostPerKM { get; set; }
    public bool valider { get; set; }
    public  DateTime? verifiedAt { get; set; }

    public bool IsActive { get; set; }

    public bool IsUserActive()
    {
        return IsActive;
    }
}


