using Castle.Core.Logging;
using IMSRepository.Models;
using IMSRepository.Repository.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IMS.Controllers
{
    [Route("api/v1/PODocuments")]
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

        [HttpGet("{PurchaseOrderId}")]
        [AllowAnonymous]
        public async Task<IActionResult> Get(int PurchaseOrderId, [FromQuery]string FileName)
        {
            try
            {
                var stream = await _pODocumentsRepository.Download(PurchaseOrderId, FileName);
                 return File(stream, "application/octet-stream", FileName);
            }
            catch(Exception  ex)
            {
                return BadRequest(ex.Message);
            }
             
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
