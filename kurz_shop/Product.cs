using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace kurz_shop
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int product_id { get; set; }

        public string name_ { get; set; }
        public string description_ { get; set; }
        public int stock_quantity { get; set; }
        public decimal price { get; set; }

        [ForeignKey(nameof(Manufacturer))]
        public int Manufacturer_id { get; set; }
        public virtual Manufacturer Manufacturer { get; set; }

        [ForeignKey(nameof(discount))]
        public int? discount_id { get; set; }
        public virtual Discount discount { get; set; }
    }
}