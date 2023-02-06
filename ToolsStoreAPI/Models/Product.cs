using System.ComponentModel.DataAnnotations;

namespace ToolsStoreAPI.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }

		[Required, StringLength(30)]
        public string ProductName { get; set; } = string.Empty;

        [Required]
        [RegularExpression("Basic|Electrical|Industrial", ErrorMessage = "Category must be one of: Basic, Electrical, Industrial")]
        public string Category { get; set; }  = string.Empty;

        [Required, Range(0, 10000)]
        public double Price { get; set; }

        [Required, Range(0, 10000)]
        public int UnitsInStock { get; set; }
    }
}
