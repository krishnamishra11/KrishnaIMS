using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace IMSRepository.Models
{
    public class PODocumentV2
    {
        public string FilePath { get; set; }
        public IFormFile FileImage { get; set; }
    }
}
