using System.ComponentModel.DataAnnotations;

namespace Api.Models;

public class Commande
{
    
    public Guid Id { get; set; }

    public Guid ClientId { get; set; }
    public Guid PharmacyId { get; set; }

    public DateTime Date { get; set; }
    public bool WithDelivery { get; set; }
    public ICollection<CommandeProduct> CommandeProducts { get; set; } = null!;   
}
