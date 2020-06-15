using IMS.BusinessLayer;
using IMS.IMSExceptions;
using IMS.Models;
using IMS.Models.Interfaces;
using Moq;
using NUnit.Framework;
using System;

namespace MSI.UnitTest
{
    [TestFixture]
    public class TestBLPurchaseOrder
    {
        private BLPurchaseOrder _bLPurchaseOrder;
        private Mock<IPurchaseOrderRepository> _purchaseOrderRepository;
        [SetUp]
        public void Setup()
        {
            _purchaseOrderRepository = new Mock<IPurchaseOrderRepository>();
            _bLPurchaseOrder = new BLPurchaseOrder(_purchaseOrderRepository.Object);
        }

        [Test]
        public void AddVerified()
        {
            var po = new PurchaseOrder();

            _bLPurchaseOrder.Add(po);
            _purchaseOrderRepository.Verify(q => q.Add(po), Times.Once());
        }

        [Test]
        public void Add_Wrong_Delivery_Date_ThrowsException()
        {
            var po = new PurchaseOrder() { OrderDate=DateTime.Now,DeliveryDate=DateTime.Now.AddDays(-1) };

            var ex= Assert.Throws<InvalidDeliveryDate>(() => _bLPurchaseOrder.Add(po));

            Assert.AreEqual(ex.GetType(), typeof(InvalidDeliveryDate));
        }

        [Test]
        public void EditVerified()
        {
            var po = new PurchaseOrder();

            _bLPurchaseOrder.Edit(po);
            _purchaseOrderRepository.Verify(q => q.Edit(po), Times.Once());
        }

        [Test]
        public void Edit_Wrong_Delivery_Date_ThrowsException()
        {
            var po = new PurchaseOrder() { OrderDate = DateTime.Now, DeliveryDate = DateTime.Now.AddDays(-1) };

            var ex = Assert.Throws<InvalidDeliveryDate>(() => _bLPurchaseOrder.Edit(po));

            Assert.AreEqual(ex.GetType(), typeof(InvalidDeliveryDate));
        }
        [Test]
        public void DeleteVerified()
        {
            
            int id = 1;
            var po = new PurchaseOrder() { Id = id, OrderStatus = OrderStatus.Created };

            _purchaseOrderRepository.Setup(q => q.FindById(id)).Returns(po);

            _bLPurchaseOrder.Remove(id);
            _purchaseOrderRepository.Verify(q => q.Remove(id), Times.Once());
        }
        [Test]

        public void Delete_Wrong_Delivery_Status_ThrowsException()
        {
            int id = 1;
            var po = new PurchaseOrder() { Id=id, OrderStatus= OrderStatus.Received };

            _purchaseOrderRepository.Setup(q => q.FindById(id)).Returns(po);
            var ex = Assert.Throws<DeliveredOrderCanNotDeleted>(() => _bLPurchaseOrder.Remove(id));

            Assert.AreEqual(ex.GetType(), typeof(DeliveredOrderCanNotDeleted));
        }
        [Test]
        public void FindByIdVerified()
        {
            int Id = 1;
            _bLPurchaseOrder.FindById(Id);
            _purchaseOrderRepository.Verify(q => q.FindById(Id), Times.Once());
        }
        [Test]
        public void GetPurchaseOrdersVerified()
        {   
            _bLPurchaseOrder.GetPurchaseOrders();
            _purchaseOrderRepository.Verify(q => q.GetPurchaseOrders(), Times.Once());
        }
        [Test]
        public void FindByVendorNameVerified()
        {
            var po = new PurchaseOrder();
            string  name = "";
            _bLPurchaseOrder.FindByVendorName(name);
            _purchaseOrderRepository.Verify(q => q.FindByVendorName(name), Times.Once());
        }
    }
}