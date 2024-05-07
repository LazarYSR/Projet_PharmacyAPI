using System.ComponentModel.DataAnnotations;

namespace Api.Models;

public class Product
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string Image { get; set; } = null!;
    public string Barcode { get; set; } = null!;


    public Category Category { get; set; }
    public Guid CategoryId { get; set; }

    public ICollection<PharmacyProduct> PharmacyProduct { get; set; } = null!;  

}
