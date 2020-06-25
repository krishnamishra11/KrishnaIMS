using IMSRepository.Models;
using IMSRepository.Repository.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;


namespace IMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PODocumentsController : ControllerBase
    {
        private readonly IPODocumentsRepository _pODocumentsRepository;  
        public PODocumentsController(IPODocumentsRepository  pODocumentsRepository)
        {
            _pODocumentsRepository = pODocumentsRepository;

        }
        
        [HttpGet]
        public IEnumerable<PODocument> Get()
        {
            return _pODocumentsRepository.GetPODocument();
        }

        [HttpGet("{PurchaseOrderId}/{FilePath}")]
        public IActionResult Get([FromQuery]int PurchaseOrderId,[FromQuery]  string FilePath)
        {
            _pODocumentsRepository.Download(PurchaseOrderId, FilePath );
            return Ok();
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
