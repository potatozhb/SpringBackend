using System.ComponentModel.DataAnnotations;

namespace SpringBackend.Models
{
    public class Product
    {
        [Key]
        [Required]
        public Guid Id { get; set; } = Guid.NewGuid();//uuid

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        [Required]
        [StringLength(100)]
        public string Category { get; set; }

        [Required]
        [StringLength(100)]
        public string Brand { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public double StockQuantity { get; set; }

        [Required]
        [StringLength(10)]
        public string SKU { get; set; }

    }
}