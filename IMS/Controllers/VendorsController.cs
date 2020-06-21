using IMS.BusinessLayer.Interfaces;
using IMS.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;

namespace IMS.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class VendorsController : ControllerBase
    {
        private readonly IBLVendor _blVendor;
        private readonly IDistributedCache _distributedCache;
        
        public VendorsController(IBLVendor blVendor, IDistributedCache distributedCache)
        {
            _blVendor = blVendor;
            _distributedCache = distributedCache;
        }
        /// <summary>
        /// GetVeondors has Redis Cache implemented
        /// </summary>
        /// <returns></returns>
     [HttpGet]
        public  ActionResult GetVendors()
        {
            List<Vendor> v;
            string key = "GetVendors";
            try
            {
                if (String.IsNullOrEmpty(_distributedCache.GetString(key)))
                {
                    v = (List<Vendor>)_blVendor.GetVendors();

                    var options = new DistributedCacheEntryOptions();
                    options.SetSlidingExpiration(TimeSpan.FromMinutes(1));
                    _distributedCache.SetString(key, System.Text.Json.JsonSerializer.Serialize<List<Vendor>>(v), options);
                }
                else
                {
                    v = System.Text.Json.JsonSerializer.Deserialize<List<Vendor>>(_distributedCache.GetString(key));
                }
            }
            catch (StackExchange.Redis.RedisConnectionException)
            {
                v = (List<Vendor>)_blVendor.GetVendors();
            }
            return Ok(v);
            
        }


        [HttpGet("{id:int}")]
        public ActionResult<Vendor> GetVendor(int id)
        {
            var vendor = _blVendor.FindById(id);

            if (vendor == null)
            {
                return NotFound();
            }
            return vendor;
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
        public IActionResult PutVendor(int id, Vendor vendor)
        {
            if (id != vendor.Id)
            {
                return BadRequest();
            }

            try
            {
                _blVendor.Edit(vendor);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (_blVendor.FindById (id)==null)
                {
                    return NotFound();
                }
                else
                {
                    return  BadRequest();
                }
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok();
        }

        
        [HttpPost]
        public  ActionResult<Vendor> PostVendor(Vendor vendor)
        {
            try
            {
                _blVendor.Add(vendor);

                return CreatedAtAction("GetVendor", new { id = vendor.Id }, vendor);
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        
        [HttpDelete("{id}")]
        public ActionResult<Vendor> DeleteVendor(int id)
        {
            try
            {
                var vendor = _blVendor.FindById(id);
                if (vendor == null)
                {
                    return NotFound();
                }


                _blVendor.Remove(id);

                return vendor;
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        
    }
}
