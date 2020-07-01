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
    [Route("api/v2/PODocuments")]
    [ApiController]
    [Authorize]
    public class PODocumentsV2Controller : ControllerBase
    {
        private readonly IPODocumentsRepository _pODocumentsRepository;
        private readonly ILogger<PODocumentsController> _logger;
        public PODocumentsV2Controller(IPODocumentsRepository pODocumentsRepository, ILogger<PODocumentsController> logger)
        {
            _pODocumentsRepository = pODocumentsRepository;
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<PODocument> Get()
        {
            return _pODocumentsRepository.GetPODocument();
        }

        [HttpGet("{FileName:alpha}")]
        [AllowAnonymous]
        public async Task<IActionResult> Get([FromQuery] string FileName)
        {
            try
            {
                var stream = await _pODocumentsRepository.Download( FileName);
                return File(stream, "application/octet-stream", FileName);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }


        [HttpPost]
        public void Post([FromForm] PODocumentV2 pODocument)
        {
            _pODocumentsRepository.Add(pODocument);
        }

       
    }
}
