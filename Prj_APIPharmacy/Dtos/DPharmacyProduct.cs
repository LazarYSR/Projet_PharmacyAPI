using Api.Enums;
using Api.Models;
using System.ComponentModel.DataAnnotations;

namespace Api.Dtos
{
    public class DPharmacyProduct
    {

        [Required]
        [MaxLength(80)]
        [MinLength(10)]
        public Guid PharmacyId { get; set; }

        [Required]
        [MaxLength(80)]
        [MinLength(10)]
        public Guid ProductId { get; set; }

        [Required]
        [MaxLength(80)]
        [MinLength(10)]
        public Guid CommandeProduct { get; set; }

        [Required]
        [MaxLength(80)]
        [MinLength(10)]
        public float Price { get; set; }
        [Required]
        public Etat Available { get; set; }
    }
}
