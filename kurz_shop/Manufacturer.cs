using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace kurz_shop
{
    public class Manufacturer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Manufacturer_id { get; set; }
        public string name_ { get; set; }
        public string country { get; set; }
    }
}