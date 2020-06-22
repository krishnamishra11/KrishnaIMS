using IMS.BusinessLayer.Interfaces;
using IMS.Controllers;
using IMSRepository.Models;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace MSI.UnitTest
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
            //Added
            //added
            
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
    }
}
