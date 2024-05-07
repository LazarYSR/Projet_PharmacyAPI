using System.ComponentModel.DataAnnotations;

namespace Api.Models;

public class DeliveryOrder
{
    
    public Guid Id { get; set; }

    public Guid OrderId { get; set; }
    public Guid DeliveryId { get; set; }

    public bool Collected { get; set; }
}
