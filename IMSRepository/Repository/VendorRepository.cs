﻿using IMSRepository.Models;
using IMSRepository.Modules;
using IMSRepository.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;

namespace IMSRepository.Repository
{
    public class VendorRepository : IVendorRepository
    {

        private readonly ImsContext _context;
        private readonly IDistributedCache _distributedCache;
         
        public VendorRepository(ImsContext context,IDistributedCache distributedCache)
        {
            _context = context;
            _distributedCache = distributedCache;
        }
        public void Add(Vendor vendor)
        {
            _context.Vendors.Add(vendor);
            _context.SaveChanges();
        }

        public void Edit(Vendor vendor)
        {
            
            _context.Entry(vendor).State = EntityState.Modified;
            _context.SaveChanges();

        }

        public Vendor FindById(int Id)
        {
            var vendor =  _context.Vendors.AsNoTracking().Where(q=>q.Id==Id).FirstOrDefault();

            return vendor;
        }

        public IEnumerable<Vendor> FindByName(string Name)
        {
            var vendors = _context.Vendors.Where (q=>q.Name.Contains(Name) );

            return vendors;
        }

        public IEnumerable<Vendor> GetVendors()
        {
            List<Vendor> vendors;
            string key = Constants.RedisCasheGetVendors;
            try
            {
                if (string.IsNullOrEmpty(_distributedCache.GetString(key)))
                {
                    vendors = _context.Vendors.ToList();

                    var options = new DistributedCacheEntryOptions();
                    options.SetSlidingExpiration(TimeSpan.FromMinutes(Constants.RedisCasheExpiry));
                    _distributedCache.SetString(key, System.Text.Json.JsonSerializer.Serialize(vendors), options);
                }
                else
                {
                    vendors = System.Text.Json.JsonSerializer.Deserialize<List<Vendor>>(_distributedCache.GetString(key));
                }
            }
            catch (StackExchange.Redis.RedisConnectionException)
            {
                vendors = _context.Vendors.ToList();
            }
            return vendors;
        }

        public void Remove(int Id)
        {
            var vendor = _context.Vendors.Find(Id);
            _context.Vendors.Remove(vendor);
            _context.SaveChangesAsync();

       }

    }
}
