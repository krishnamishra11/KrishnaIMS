using IMS.Models;
using System.Collections;

namespace IMS.BusinessLayer.Interfaces
{
    public interface IBLPurchaseOrder
    {
        public void Add(PurchaseOrder purchaseOrder);

        public void Edit(PurchaseOrder purchaseOrder);

        public PurchaseOrder FindById(int Id);

        public IEnumerable FindByVendorName(string Name);

        public IEnumerable GetPurchaseOrders();

        public void Remove(int Id);

    }
}
