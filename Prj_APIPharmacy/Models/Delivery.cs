using System.ComponentModel.DataAnnotations;

namespace Api.Models;

public class Delivery
{
    
    public Guid Id { get; set; }

    public Guid DelivererId { get; set; }
    public DateTime? DeliveryDate { get; set; }

    public string? Address { get; set; }
    public string Contact { get; set; } = null!;

    public double Latitude { get; set; }
    public double Longitude { get; set; }
}
