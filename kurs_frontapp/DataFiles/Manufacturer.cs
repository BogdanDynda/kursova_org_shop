using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace kurs_frontapp
{
    public class Manufacturer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int manufacturer_id { get; set; }
        public string name_ { get; set; }
        public string country { get; set; }
    }
}