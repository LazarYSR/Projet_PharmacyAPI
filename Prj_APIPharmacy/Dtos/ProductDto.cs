using System.ComponentModel.DataAnnotations;

namespace Api.Dtos
{
    public class ProductDto
    {
        [Required]
        public string Name { get; set; } = null!;
        [Required]
        public string Description { get; set; } = null!;
        [Required]
        public string Image { get; set; } = null!;
        [Required]
        public string Barcode { get; set; } = null!;
        [Required]
        public Guid CategoryId { get; set; }
    }
}
