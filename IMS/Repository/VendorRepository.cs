using IMS.Models;
using IMS.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace IMS.Repository
{
    public class VendorRepository : IVendorRepository
    {

        private readonly IMSContext _context;

        public VendorRepository(IMSContext context)
        {
            _context = context;
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
            return  _context.Vendors.ToList();
        }

        public void Remove(int Id)
        {
            var vendor = _context.Vendors.Find(Id);
            _context.Vendors.Remove(vendor);
            _context.SaveChangesAsync();

       }

    }
}
