
using System.ComponentModel.DataAnnotations;

namespace SpringBackend.Dtos
{
    public class ProductReadResponse
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        [Required]
        [StringLength(100)]
        public string Category { get; set; }

    }
}