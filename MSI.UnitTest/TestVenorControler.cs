using IMS.BusinessLayer.Interfaces;
using IMS.Controllers;
using IMS.Models;
using Microsoft.Extensions.Caching.Distributed;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace MSI.UnitTest
{
    [TestFixture]
    public class TestVenorControler
    {
        Mock<IBLVendor> _blVendor;
        Mock<IDistributedCache> _distributedCache;
        VendorsController vendorsController; 
        [SetUp]
        public void SetUp()
        {
             _blVendor = new Mock<IBLVendor>();
             _distributedCache = new Mock<IDistributedCache>();
            vendorsController = new VendorsController(_blVendor.Object, _distributedCache.Object);

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
    }
}
