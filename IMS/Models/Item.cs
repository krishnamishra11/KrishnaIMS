
using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace IMS.Models
{

    public class Item
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage ="Item Name is required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Item Unit is required")]
        [StringLength(10,ErrorMessage ="Unit can not be more then 10")]
        public string Unit { get; set; }
        [Range(float.Epsilon,1000000,ErrorMessage ="Quantity can not be less then less then equal to zero")]
        [Required(ErrorMessage = "Item Quantity is required")]
        public float Quantity { get; set; }
        [Required(ErrorMessage = "Item Rate is required")]

        [Range(float.Epsilon, 1000000, ErrorMessage = "Rate can not be less then less then equal to zero")]
        public float Rate { get; set; }
        [JsonIgnore]
        [Newtonsoft.Json.JsonIgnore]
        public  PurchaseOrder PurchaseOrder { get; set; }
        
    }
}
