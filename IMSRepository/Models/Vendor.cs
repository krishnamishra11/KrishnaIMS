using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IMSRepository.Models
{
    public class Vendor
    {
        public Vendor()
        {
            Prefered = false;
            BlackListed = false;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
        [Required(ErrorMessage ="Vendor Name is Required")]
        [StringLength(100,ErrorMessage ="Venor Name can not be more then 100") ]
        public string Name { get; set; }

        [Required(ErrorMessage = "Address is Required")]
        [StringLength(200, ErrorMessage = "Address can not be more then 200")]
        public string Address { get; set; }

        [StringLength(20, ErrorMessage = "Mobile Number can not be more then 20")]
        [RegularExpression(@"^[789]\d{9}$",ErrorMessage ="Invalid mobile number")]
        public string Mobile { get; set; }

        [StringLength(100, ErrorMessage = "Email can not be more then 100")]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5})$",ErrorMessage ="Invalid email Id")]
        public string Email { get; set; }
        [StringLength(20, ErrorMessage = "Fax Number can not be more then 20")]
        public string Fax { get; set; }
        
        public bool Prefered { get; set; }
        
        public bool BlackListed { get; set; }

    }
}
