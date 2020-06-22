using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IMSRepository.Models
{
    [Table("Person")]
    public class Person
    {
        [Key]
        public int Id {get;set;}
        [Required]
        public string Name { get; set; }
        [Required]
        public string Password { get; set; }

    }
}
