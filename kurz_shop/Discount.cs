using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace kurz_shop
{
    public class Discount
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int discount_id { get; set; }
        public string discount_code { get; set; }
        public int percentage { get; set; }
    }
}