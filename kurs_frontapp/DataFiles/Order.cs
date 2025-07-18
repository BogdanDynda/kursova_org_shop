using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace kurs_frontapp
{
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int order_id { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime order_datetime { get; set; }

        public int employee_id { get; set; }

        [ForeignKey("employee_id")]
        public virtual Employee Employee { get; set; }

        public virtual ICollection<Order_Items> Order_Items { get; set; }
    }
}