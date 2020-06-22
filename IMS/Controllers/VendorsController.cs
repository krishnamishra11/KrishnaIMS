using IMS.BusinessLayer.Interfaces;
using IMSRepository.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;

namespace IMS.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class VendorsController : ControllerBase
    {
        private readonly IBLVendor _blVendor;
        private ILogger _logger;


        public VendorsController(IBLVendor blVendor,ILogger<VendorsController> logger)
        {
            _blVendor = blVendor;
            _logger = logger;
        }
        /// <summary>
        /// GetVeondors has Redis Cache implemented
        /// </summary>
        /// <returns></returns>
     [HttpGet]
        public IActionResult GetVendors()
        {
            try
            {
                _logger.LogInformation("GetVendor Started " );
                return Ok(_blVendor.GetVendors());
            }
            catch (Exception ex)
            {
                _logger.LogError("GetVendor Error",ex);
                return BadRequest(ex.Message);
            }

        }


        [HttpGet("{id:int}")]
        public ActionResult<Vendor> GetVendor(int id)
        {
            try
            {
                _logger.LogInformation("GetVendor Started " + id.ToString());
                var vendor = _blVendor.FindById(id);

                if (vendor == null)
                {
                    return NotFound();
                }
                return vendor;
            }catch(Exception ex)
            {
                _logger.LogError("GetVendor Error"+id.ToString(),ex);
                return BadRequest(ex.Message);
            }

        }

        //[HttpGet("{id:alpha}")]
        ////[Route("VendorByName")]
        //public ActionResult<IEnumerable<Vendor>> GetVendorByName(string id)
        //{
        //    var vendors = _blVendor.FindByName(id);

        //    if (vendors == null)
        //    {
        //        return NotFound();
        //    }
        //    return Ok(vendors);
        //}
        [HttpPut("{id}")]
        public IActionResult PutVendor(Vendor vendor)
        {

            try
            {
                _logger.LogInformation("PutVendor Started " + vendor.Id.ToString());

                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }

                _blVendor.Edit(vendor);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                _logger.LogError("PutVendor Error" + vendor.Id.ToString(), ex);

                if (_blVendor.FindById (vendor.Id)==null)
                {
                    _logger.LogError("PutVendor Error :Id not found " + vendor.Id.ToString());

                    return NotFound();
                }
                else
                {
                    return  BadRequest();
                }
            }catch(Exception ex)
            {
                _logger.LogError("PutVendor Error" + vendor.Id.ToString(), ex);
                return BadRequest(ex.Message);
            }

            return Ok();
        }

        
        [HttpPost]
        public  ActionResult<Vendor> PostVendor(Vendor vendor)
        {
            try
            {
                _logger.LogInformation("PostVendor Started " + vendor.Id.ToString());

                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }

                _blVendor.Add(vendor);

                return CreatedAtAction("PostVendor", new { id = vendor.Id }, vendor);
            }catch(Exception ex)
            {
                _logger.LogError("PostVendor Error" + vendor.Id.ToString(), ex);

                return BadRequest(ex.Message);
            }
        }

        
        [HttpDelete("{id}")]
        public ActionResult<Vendor> DeleteVendor(int id)
        {
            try
            {
                _logger.LogInformation("DeleteVendor Started " + id.ToString());
                var vendor = _blVendor.FindById(id);
                if (vendor == null)
                {
                    _logger.LogError("DeleteVendor Error :Id not found " + id.ToString());

                    return NotFound();
                }


                _blVendor.Remove(id);
                
                return vendor;
            }catch(Exception ex)
            {
                _logger.LogError("DeleteVendor Error" + id.ToString(), ex);

                return BadRequest(ex.Message);
            }
        }

        
    }
}
