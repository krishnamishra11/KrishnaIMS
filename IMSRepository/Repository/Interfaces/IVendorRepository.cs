using IMSRepository.Models;
using System.Collections.Generic;

namespace IMSRepository.Repository.Interfaces
{
    public interface IVendorRepository
    {

        void Add(Vendor vendor);
        void Edit(Vendor vendor);
        void Remove(int Id);
        IEnumerable<Vendor> GetVendors(); 
        Vendor FindById(int Id);
        IEnumerable<Vendor> FindByName(string Name);
    }

}
