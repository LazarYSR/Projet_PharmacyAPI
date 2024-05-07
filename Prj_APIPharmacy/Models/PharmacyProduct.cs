using Api.Enums;
using System.ComponentModel.DataAnnotations;

namespace Api.Models;

public class PharmacyProduct
{
    
    public Guid Id { get; set; }
    public Pharmacy Pharmacy { get; set; }  
    public Guid PharmacyId { get; set; }
   
    public Product Product { get; set; }
    public Guid ProductId { get; set; }

    public ICollection<CommandeProduct> CommandeProduct { get; set; } 
    public float Price { get; set; }

    public Etat Available { get; set; }
}
