using Api.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace Api.Dtos
{
   
        [Index(nameof(Name), IsUnique = true)]
    public class DPharmacy
    {
        [Required]
        [MaxLength(80)]
        [MinLength(10)]
        public string Name { get; set; }
        [Required]
        public string Address { get; set; } = null!;
        [Required]
        [StringLength(10)]
        public string Phone { get; set; } = null!;
        [Required]
        [StringLength(20)]
        public string Email { get; set; } = null!;
        [Required]

        public double Latitude { get; set; }
        [Required]
        public double Longitude { get; set; }

        public Guid pharmacyProduct { get; set; } 
    }
}
