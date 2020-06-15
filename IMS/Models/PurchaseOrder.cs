using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IMS.Models
{
    public class PurchaseOrder
    {
        
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Order Date Required")]
        public DateTime OrderDate { get; set; }

        [Required(ErrorMessage = "Vendor Required")]
        public int VendorId { get; set; }
        
        public  Vendor Vendor { get; set; }

        [Required(ErrorMessage = "Order Details Required")]
        
        public  ICollection<Item> OrderDetails { get; set; }
        
        [StringLength(200)]
        [Required(ErrorMessage = "Shipping Required")]
        public string ShipmentAddress { get; set; }
        
        [StringLength(30, ErrorMessage = "Shipment Mode By can not be more then 30")]
        [Required(ErrorMessage = "Shipping Mode Required")]
        public string ShipmentMode { get; set; }
        [Required(ErrorMessage = "Delivery Date")]
        public DateTime DeliveryDate { get; set; }
        [Required(ErrorMessage = "Created by Required")]
        [StringLength(50, ErrorMessage = "Created By can not be more then 50")]
        public string CreatedBy { get; set; }
        [StringLength(50,ErrorMessage = "Approved By can not be more then 50")]
        public string ApprovedBy { get; set; }
        [Required]
        public OrderStatus OrderStatus { get; set; }
        public DateTime OrderStatusDate { get; set; }
        [StringLength(500, ErrorMessage = "Shipment Mode By can not be more then 30")]
        public string Note { get; set; }
    }
}
