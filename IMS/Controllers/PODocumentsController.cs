using Castle.Core.Logging;
using IMSRepository.Models;
using IMSRepository.Repository.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;


namespace IMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PODocumentsController : ControllerBase
    {
        private readonly IPODocumentsRepository _pODocumentsRepository;
        private readonly ILogger<PODocumentsController> _logger;
        public PODocumentsController(IPODocumentsRepository  pODocumentsRepository, ILogger<PODocumentsController> logger )
        {
            _pODocumentsRepository = pODocumentsRepository;
            _logger = logger;
        }
        
        [HttpGet]
        public IEnumerable<PODocument> Get()
        {
            return _pODocumentsRepository.GetPODocument();
        }

        [HttpGet("{PurchaseOrderId}/{FilePath}")]
        public IActionResult Get([FromQuery]int PurchaseOrderId,[FromQuery]  string FilePath)
        { 
            return File(_pODocumentsRepository.Download(PurchaseOrderId, FilePath), "application/octet-stream");
        }

        
        [HttpPost]
        public void Post([FromForm] PODocument pODocument)
        {
            _pODocumentsRepository.Add(pODocument);
        }
        
        [HttpPut]
        public void Put( [FromBody] PODocument pODocument)
        {
            _pODocumentsRepository.Edit(pODocument);

        }
        
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _pODocumentsRepository.Remove(id);
        }
    }
}
