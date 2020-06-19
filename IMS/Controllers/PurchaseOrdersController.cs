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
    [ApiExplorerSettings(IgnoreApi = true)]
    //hi
    public class PurchaseOrdersController : ControllerBase
    {
        private readonly IBLPurchaseOrder  _bLPurchaseOrder;
        private readonly IDistributedCache _distributedCache;

        public PurchaseOrdersController(IBLPurchaseOrder bLPurchaseOrder, IDistributedCache distributedCache )
        {
            _bLPurchaseOrder = bLPurchaseOrder;
            _distributedCache = distributedCache;
        }

        
        [HttpGet]
        public  ActionResult GetPurchaseOrders()
        {
            List<PurchaseOrder> po;
            string key = "GetPurchaseOrders";
            try
            {
                if (String.IsNullOrEmpty(_distributedCache.GetString(key)))
                {
                    po =(List<PurchaseOrder>)_bLPurchaseOrder.GetPurchaseOrders();

                    var options = new DistributedCacheEntryOptions();
                    options.SetSlidingExpiration(TimeSpan.FromMinutes(1));
                    _distributedCache.SetString(key, System.Text.Json.JsonSerializer.Serialize<List<PurchaseOrder>>(po), options);
                }
                else
                {
                    po = System.Text.Json.JsonSerializer.Deserialize<List<PurchaseOrder>>(_distributedCache.GetString(key));
                }
            }
            catch(StackExchange.Redis.RedisConnectionException )
            {
                po = (List<PurchaseOrder>)_bLPurchaseOrder.GetPurchaseOrders();
            }
            return Ok(po);
        }

        //[HttpGet("{name:alpha}")]
        //public ActionResult GetPurchaseOrdersByVendorName(string name)
        //{
        //    var po = _bLPurchaseOrder.FindByVendorName(name);
        //    return Ok(po);
        //}


        [HttpGet("{id:int}")]
        public ActionResult<PurchaseOrder> GetPurchaseOrder(int id)
        {
            var purchaseOrder = _bLPurchaseOrder.FindById(id);

            if (purchaseOrder == null)
            {
                return NotFound();
            }

            return purchaseOrder;
        }


        [HttpPut("{id}")]
        public  IActionResult PutPurchaseOrder(int id, PurchaseOrder purchaseOrder)
        {
            if (id != purchaseOrder.Id)
            {
                return BadRequest();
            }

            try
            {
                
                _bLPurchaseOrder.Edit(purchaseOrder);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (_bLPurchaseOrder.FindById(id)==null)
                {
                    return NotFound();
                }
                else
                {
                    return BadRequest();
                }
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return NoContent();
        }

        [HttpPost]
        public  ActionResult<PurchaseOrder> PostPurchaseOrder(PurchaseOrder purchaseOrder)
        {
            try { 
                _bLPurchaseOrder.Add(purchaseOrder);

                return CreatedAtAction("GetPurchaseOrder", new { id = purchaseOrder.Id }, purchaseOrder);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        // DELETE: api/PurchaseOrders/5
        [HttpDelete("{id}")]
        public ActionResult<PurchaseOrder> DeletePurchaseOrder(int id)
        {
            try
            {
                var purchaseOrder = _bLPurchaseOrder.FindById(id);
                if (purchaseOrder == null)
                {
                    return NotFound();
                }

                _bLPurchaseOrder.Remove(id);

                return purchaseOrder;
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }
}
