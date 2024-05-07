using System.ComponentModel.DataAnnotations;

namespace Api.Models;

public class CommandeProduct
{
    
    public Guid Id { get; set; }
    public Commande Commande { get; set; }
    public Guid CommandeId { get; set; }
    public PharmacyProduct PharmacyProduct { get; set; }
    public Guid PharmacyProductId { get; set; }

    public int Quantity { get; set; }
}
