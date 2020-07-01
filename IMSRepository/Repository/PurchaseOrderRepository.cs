using IMSRepository.BusService.Interface;
using IMSRepository.Models;
using IMSRepository.Models.Interfaces;
using IMSRepository.Modules;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace IMSRepository.Repository
{
    public class PurchaseOrderRepository : IPurchaseOrderRepository
    {
        private readonly IMSContext _context;
        private readonly IDistributedCache _distributedCache;
        private readonly IBusMessageService _busMessageService;


        public PurchaseOrderRepository(IMSContext context, IDistributedCache distributedCache, IBusMessageService busMessageService)
        {
            _context = context;
            _distributedCache = distributedCache;
            _busMessageService = busMessageService;
        }
        public void Add(PurchaseOrder purchaseOrder)
        {
            _context.PurchaseOrders.Add(purchaseOrder);
            _context.SaveChanges();
            _busMessageService.SendMessage(purchaseOrder);

        }

        public void Edit(PurchaseOrder purchaseOrder)
        {

            var pods = _context.Items
                               .AsNoTracking()
                               .Where(q => q.PurchaseOrder.Id == purchaseOrder.Id);
            foreach (var o in pods)
            {
                if (!purchaseOrder.OrderDetails.Any(q => q.Id == o.Id))
                {
                    _context.Items.Remove(o);
                }
            }

            _context.Attach(purchaseOrder);

            IEnumerable<EntityEntry> unchangedEntities = _context.ChangeTracker
                                                        .Entries()
                                                        .Where(x => x.State == EntityState.Unchanged);

            foreach (EntityEntry ee in unchangedEntities)
            {
                ee.State = EntityState.Modified;
            }
          
            _context.Entry(purchaseOrder).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public PurchaseOrder FindById(int Id)
        {
            var purchaseOrder = _context.PurchaseOrders
                .AsNoTracking()
                .Include(q => q.OrderDetails)
                .Where(q => q.Id == Id)
                .FirstOrDefault();

            return purchaseOrder;
        }

        public IEnumerable FindByVendorName(string Name)
        {
            var purchaseOrder = _context.PurchaseOrders
               .Include(q => q.OrderDetails)
               .Include(r => r.Vendor)
               .Where(s => s.Vendor.Name.Contains(Name))
               .ToList();

            return purchaseOrder;
        }

        public IEnumerable GetPurchaseOrders()
        {

            List<PurchaseOrder> purchaseOrders;
            string key = Constants.GetPurchaseOrders;
            try
            {
                if (string.IsNullOrEmpty(_distributedCache.GetString(key)))
                {
                    purchaseOrders = _context.PurchaseOrders.Include(q => q.OrderDetails).ToList();

                    var options = new DistributedCacheEntryOptions();
                    options.SetSlidingExpiration(TimeSpan.FromMinutes(Constants.RedisCasheExpiry));
                    _distributedCache.SetString(key, System.Text.Json.JsonSerializer.Serialize(purchaseOrders), options);
                }
                else
                {
                    purchaseOrders = System.Text.Json.JsonSerializer.Deserialize<List<PurchaseOrder>>(_distributedCache.GetString(key));
                }
            }
            catch (StackExchange.Redis.RedisConnectionException)
            {
                purchaseOrders = _context.PurchaseOrders.Include(q => q.OrderDetails).ToList();
            }
            return purchaseOrders;
        }

        public void Remove(int Id)
        {
            var purchaseOrder = _context.PurchaseOrders
                                             .Where(q => q.Id == Id)
                                             .Include(q => q.OrderDetails)
                                             .FirstOrDefault();

            foreach (var item in purchaseOrder.OrderDetails)
            {
                _context.Items.Remove(item);
            }
            _context.PurchaseOrders.Remove(purchaseOrder);

            _context.SaveChanges();

        }
    }
}
