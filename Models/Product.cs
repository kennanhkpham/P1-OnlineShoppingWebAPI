using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace P1_OnlineShoppingWebAPI.Models
{
    public class Product
    {
        [Key]
        public int pId { get; set; }
        [Required]
        [Column("pName")]
        public string pName { get; set; } = string.Empty;
        [Required]
        [Column("pCategory")]
        public string pCategory { get; set; } = string.Empty;
        [Required]
        [Column("pPrice", TypeName = "decimal(18,2)")]
        public decimal pPrice { get; set; }
        [Required]
        [Column("pQty")]
        public int pStockQuantity { get; set; }
        [Required]
        [Column("pInStock")]
        public bool pInStock { get; set; }
    }
}