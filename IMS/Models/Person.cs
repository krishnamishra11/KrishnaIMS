using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace IMS.Models
{
    [Table("Person")]
    public class Person
    {
        [Key]
        public int Id {get;set;}
        [Required]
        public string Name { get; set; }
        [Required]
        [JsonIgnore]
        public string Password { get; set; }

    }
}
