using Api.Models;
using System.ComponentModel.DataAnnotations;

namespace Api.Dtos
{
    public class DCommandeProduct
    {
        [Required] 
        public Guid CommandeId { get; set; }
        [Required]
        public Guid PharmacyProductId { get; set; }
        [Required]
        [MaxLength(100)]
        [MinLength(1)]
        public int Quantity { get; set; }
    }
}
