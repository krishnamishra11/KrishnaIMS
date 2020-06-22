using IMS.BusinessLayer.Interfaces;
using IMS.IMSExceptions;
using IMSRepository.Models;
using IMSRepository.Models.Interfaces;
using System.Collections;

namespace IMS.BusinessLayer
{
    public class BLPurchaseOrder : IBLPurchaseOrder
    {
        private readonly IPurchaseOrderRepository _repository;

        public BLPurchaseOrder(IPurchaseOrderRepository purchaseOrderRepository)
        {
            _repository = purchaseOrderRepository;
        }
        public void Add(PurchaseOrder purchaseOrder)
        {
            
            if (InvalidDeliveryDate(purchaseOrder))
                throw new InvalidDeliveryDate();

            _repository.Add(purchaseOrder);
        }

        public void Edit(PurchaseOrder purchaseOrder)
        {
            if (InvalidDeliveryDate(purchaseOrder))
                throw new InvalidDeliveryDate();

            _repository.Edit(purchaseOrder);
        }

        public PurchaseOrder FindById(int Id)
        {
            return _repository.FindById(Id);
        }

        public IEnumerable FindByVendorName(string Name)
        {
            return _repository.FindByVendorName(Name);
        }

        public IEnumerable GetPurchaseOrders()
        {
            return _repository.GetPurchaseOrders();
        }

        public void Remove(int Id)
        {
            var purchaseOrder = _repository.FindById(Id);

            if (purchaseOrder.OrderStatus == OrderStatus.Received)
                throw new DeliveredOrderCanNotDeleted();

            _repository.Remove(Id);
        }

        private bool InvalidDeliveryDate(PurchaseOrder  purchase)
        {
            return purchase.DeliveryDate < purchase.OrderDate;
        }

        

    }
}
