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
    public class TestVenorControler
    {
        Mock<IBLVendor> _blVendor;
        Mock<ILogger<VendorsController>> _logger;
        VendorsController vendorsController; 
        [SetUp]
        public void SetUp()
        {
             _blVendor = new Mock<IBLVendor>();
            _logger = new Mock<ILogger<VendorsController>>();
           vendorsController = new VendorsController(_blVendor.Object, _logger.Object);

        }
        [Test]
        public void TestGetVendorsById()
        {
            int id = 1;
            
            vendorsController.GetVendor(id);
            _blVendor.Verify(q => q.FindById(id), Times.Once);

        }

        [Test]
        public void TestGetVendors_Post()
        {
            int id = 1;
            Vendor vendor = new Vendor();

            vendorsController.PostVendor(vendor);
            _blVendor.Verify(q => q.Add(vendor), Times.Once);

        }

        [Test]
        public void TestGetVendors_Put()
        {
            Vendor vendor = new Vendor();
            vendorsController.PutVendor(vendor);
            _blVendor.Verify(q => q.Edit(vendor), Times.Once);
        }

        [Test]
        public void TestGetVendors_Put_WithException()
        {
            int id = 1;
            Vendor vendor = new Vendor() {Id=id };
            
            _blVendor.Setup(q => q.Edit(vendor)).Throws(new DbUpdateConcurrencyException());
            vendorsController.PutVendor(vendor);
            
            _blVendor.Verify(q => q.FindById(id), Times.Once);
        }

        [Test]
        public void TestGetVendors_Delete()
        {
            int id = 1;
            Vendor vendor = new Vendor();
            _blVendor.Setup(q => q.FindById(id)).Returns(vendor);
            vendorsController.DeleteVendor(id);
            _blVendor.Verify(q => q.Remove(id), Times.Once);
        }

        

    }
}
