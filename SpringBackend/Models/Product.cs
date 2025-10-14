using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpringBackend.Models
{
    [Index(nameof(Name))]
    [Index(nameof(Category))]
    [Index(nameof(Brand))]
    [Index(nameof(SKU), IsUnique =true)]//SKU must be unique
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
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        [Required]
        public double StockQuantity { get; set; }

        [Required]
        [StringLength(20)]
        public string SKU { get; set; }

        [NotMapped]
        public string NameLower => Name.ToLower();

        [NotMapped]
        public string CategoryLower => Category.ToLower();

        [NotMapped]
        public string BrandLower => Brand.ToLower();
    }
}