using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace kurs_frontapp
{
    public class Employee
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int employee_id { get; set; }
        public string last_name { get; set; }
        public string login { get; set; }
        public string password_ { get; set; }
        public string position_ { get; set; }

    }
}