using IMS.BusinessLayer;
using IMSRepository.Models;
using IMSRepository.Models.Interfaces;
using Moq;
using NUnit.Framework;

namespace IMSUnitTests
{
    public class Tests
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
        public void Test1()
        {
            Assert.Pass();
        }
    }
}