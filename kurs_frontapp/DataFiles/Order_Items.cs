using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace kurs_frontapp
{
    public class Order_Items
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int list_item_id { get; set; }

        [ForeignKey("Order")]
        public int order_id { get; set; }

        [ForeignKey("Product")]
        public int product_id { get; set; }

        [Required]
        public int quantity { get; set; }

        [Required]
        [Column(TypeName = "decimal(10, 2)")]
        public decimal price { get; set; }

        // Навігаційні властивості
        public virtual Order Order { get; set; }
        public virtual Product Product { get; set; }
    }
}