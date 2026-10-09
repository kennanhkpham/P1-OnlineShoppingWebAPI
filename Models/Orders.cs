// Models/Order.cs
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace P1_OnlineShoppingWebAPI.Models
{
    public class Order
    {
        [Key]
        public int oID { get; set; }
        [Required]
        [Column("customerID")]
        public int oCustomerID { get; set; }
        [Required]
        [Column("OrderDate")]
        public DateTime oOrderDate { get; set; } = DateTime.UtcNow;
        [Required]
        [Column("totalAmount", TypeName = "decimal(18,2)")]
        public decimal oTotalAmount { get; set; }
        [Required]
        [Column("status")]
        public string oStatus { get; set; } = "Submitted";
    }
}