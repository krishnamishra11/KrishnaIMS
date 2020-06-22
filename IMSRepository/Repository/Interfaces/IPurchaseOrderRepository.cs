using System.Collections;

namespace IMSRepository.Models.Interfaces
{

    public interface IPurchaseOrderRepository
    {

        void Add(PurchaseOrder purchaseOrder);
        void Edit(PurchaseOrder purchaseOrder);
        void Remove(int Id);
        IEnumerable GetPurchaseOrders();
        PurchaseOrder FindById(int Id);
        IEnumerable FindByVendorName(string Name);
    }

}
