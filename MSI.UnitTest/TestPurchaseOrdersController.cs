using IMS.BusinessLayer.Interfaces;
using IMS.Controllers;
using IMSRepository.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace UnitTestMSI
{
    [TestFixture]
    public class TestPurchaseOrdersController
    {
        Mock<IBLPurchaseOrder> _blPurchaseOrder;
        Mock<ILogger<PurchaseOrdersController>> _logger;
        PurchaseOrdersController PurchaseOrdersController;
        [SetUp]
        public void SetUp()
        {
            _blPurchaseOrder = new Mock<IBLPurchaseOrder>();
            _logger = new Mock<ILogger<PurchaseOrdersController>>();
            PurchaseOrdersController = new PurchaseOrdersController(_blPurchaseOrder.Object, _logger.Object);

        }
        [Test]
        public void TestGetPurchaseOrdersById()
        {
            int id = 1;

            PurchaseOrdersController.GetPurchaseOrder(id);
            _blPurchaseOrder.Verify(q => q.FindById(id), Times.Once);

        }

        [Test]
        public void TestGetPurchaseOrders_Post()
        {
         
            PurchaseOrder PurchaseOrder = new PurchaseOrder();

            PurchaseOrdersController.PostPurchaseOrder(PurchaseOrder);
            _blPurchaseOrder.Verify(q => q.Add(PurchaseOrder), Times.Once);

        }

        [Test]
        public void TestGetPurchaseOrders_Put()
        {
            PurchaseOrder PurchaseOrder = new PurchaseOrder();
            PurchaseOrdersController.PutPurchaseOrder(PurchaseOrder);
            _blPurchaseOrder.Verify(q => q.Edit(PurchaseOrder), Times.Once);
        }

        [Test]
        public void TestGetPurchaseOrders_Put_WithException()
        {
            int id = 1;
            PurchaseOrder PurchaseOrder = new PurchaseOrder() { Id = id };

            _blPurchaseOrder.Setup(q => q.Edit(PurchaseOrder)).Throws(new DbUpdateConcurrencyException());
            PurchaseOrdersController.PutPurchaseOrder(PurchaseOrder);

            _blPurchaseOrder.Verify(q => q.FindById(id), Times.Once);
        }

        [Test]
        public void TestGetPurchaseOrders_Delete()
        {
            int id = 1;
            PurchaseOrder PurchaseOrder = new PurchaseOrder();
            _blPurchaseOrder.Setup(q => q.FindById(id)).Returns(PurchaseOrder);
            PurchaseOrdersController.DeletePurchaseOrder(id);
            _blPurchaseOrder.Verify(q => q.Remove(id), Times.Once);
        }



    }
}
