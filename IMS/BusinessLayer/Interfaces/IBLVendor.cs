using IMS.Models;
using System.Collections;
using System.Collections.Generic;

namespace IMS.BusinessLayer.Interfaces
{
    public interface IBLVendor
    {
        public void Add(Vendor vendor);

        public void Edit(Vendor vendor);

        public Vendor FindById(int Id);

        public IEnumerable FindByName(string Name);

        public IEnumerable<Vendor> GetVendors();

        public void Remove(int Id);


}
}
