using IMS.BusinessLayer.Interfaces;
using IMSRepository.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;

namespace IMS.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    //[ApiExplorerSettings(IgnoreApi = true)]
    
    public class PurchaseOrdersController : ControllerBase
    {
        private readonly IBLPurchaseOrder  _bLPurchaseOrder;
        private readonly ILogger _logger;

        public PurchaseOrdersController(IBLPurchaseOrder bLPurchaseOrder, ILogger<PurchaseOrdersController> logger)
        {
            _bLPurchaseOrder = bLPurchaseOrder;
            _logger = logger;
        }

        
        [HttpGet]
        public  ActionResult GetPurchaseOrders()
        {
            try
            {
                _logger.LogInformation("GetPurchaseOrders Started ");
                return Ok(_bLPurchaseOrder.GetPurchaseOrders());
            }
            catch (Exception ex)
            {
                _logger.LogError("GetPurchaseOrders Error", ex);
                return BadRequest(ex.Message);
            }
            
        }


        [HttpGet("{id:int}")]
        public ActionResult<PurchaseOrder> GetPurchaseOrder(int id)
        {

            try
            {
                _logger.LogInformation("GetPurchaseOrders Started " +id.ToString());
                var purchaseOrder = _bLPurchaseOrder.FindById(id);

                if (purchaseOrder == null)
                {
                    return NotFound();
                }

                return purchaseOrder;
            }
            catch (Exception ex)
            {
                _logger.LogError("GetPurchaseOrders Error" + id.ToString(), ex);
                return BadRequest(ex.Message);
            }
        }


        [HttpPut("{id}")]
        public  IActionResult PutPurchaseOrder(PurchaseOrder purchaseOrder)
        {
            try
            {
                _logger.LogInformation("PutPurchaseOrder Started " + purchaseOrder.Id.ToString());

                if(!ModelState.IsValid)
                {
                    return BadRequest();
                }

                _bLPurchaseOrder.Edit(purchaseOrder);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                _logger.LogError("PutPurchaseOrder Error" + purchaseOrder.Id.ToString(), ex);

                if (_bLPurchaseOrder.FindById(purchaseOrder.Id) ==null)
                {
                    return NotFound();
                }
                else
                {
                    return BadRequest();
                }
            }catch(Exception ex)
            {
                _logger.LogError("PutPurchaseOrder Error" + purchaseOrder.Id.ToString(), ex);
                return BadRequest(ex.Message);
            }

            return NoContent();
        }

        [HttpPost]
        public  ActionResult<PurchaseOrder> PostPurchaseOrder(PurchaseOrder purchaseOrder)
        {
            try {
                    _logger.LogInformation("PostPurchaseOrder Started " + purchaseOrder.Id.ToString());

                    if (!ModelState.IsValid)
                    {
                        return BadRequest();
                    }
                    
                   _bLPurchaseOrder.Add(purchaseOrder);

                return CreatedAtAction("PostPurchaseOrder", new { id = purchaseOrder.Id }, purchaseOrder);
            }
            catch (Exception ex)
            {
                _logger.LogError("PostPurchaseOrder Error" + purchaseOrder.Id.ToString(), ex);
                return BadRequest(ex.Message);
            }

        }

        // DELETE: api/PurchaseOrders/5
        [HttpDelete("{id}")]
        public ActionResult<PurchaseOrder> DeletePurchaseOrder(int id)
        {
            try
            {
                _logger.LogInformation("DeletePurchaseOrder Started " + id.ToString());

                var purchaseOrder = _bLPurchaseOrder.FindById(id);
                if (purchaseOrder == null)
                {
                    return NotFound();
                }

                _bLPurchaseOrder.Remove(id);

                return purchaseOrder;
            }catch(Exception ex)
            {
                _logger.LogError("DeletePurchaseOrder Error" + id.ToString(), ex);
                return BadRequest(ex.Message);
            }
        }


    }
}
