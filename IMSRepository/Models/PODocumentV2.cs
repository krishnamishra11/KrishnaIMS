using Microsoft.AspNetCore.Http;

namespace IMSRepository.Models
{
    public class PODocumentV2
    {
        public string FilePath { get; set; }
        public IFormFile FileImage { get; set; }
    }
}
