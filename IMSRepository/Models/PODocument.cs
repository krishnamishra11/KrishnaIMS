using Microsoft.AspNetCore.Http;

namespace IMSRepository.Models
{
    public class PODocument
    {
        public int PurchaseOrderId { get; set; }
        public string FilePath { get; set; }
        public IFormFile FileImage { get; set; }

    }
}
