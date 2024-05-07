using System.ComponentModel.DataAnnotations;

namespace Api.Models;

public class Pharmacy
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;
    public string Address { get; set; } = null!;
    public string Phone { get; set; } = null!;
    public string Email { get; set; } = null!;

    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public ICollection <PharmacyProduct> pharmacyProduct { get; set; }  
}
