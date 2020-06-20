using IMS.BusinessLayer;
using IMS.IMSExceptions;
using IMS.Models;
using IMS.Repository.Interfaces;
using Moq;
using NUnit.Framework;

namespace MSI.UnitTest
{
    class TestBLVendor
    {
        private BLVendor _bLVendor;
        private Mock<IVendorRepository> _VendorRepository;
        [SetUp]
        public void Setup()
        {
            _VendorRepository = new Mock<IVendorRepository>();
            _bLVendor = new BLVendor(_VendorRepository.Object);
        }

        [Test]
        public void AddVerified()
        {
            var v = new Vendor() { Email = "a@b.com", Mobile = "12334566", Fax = "1234456" };

            _bLVendor.Add(v);
            _VendorRepository.Verify(q => q.Add(v), Times.Once());
        }
        //
        [Test]
        [TestCase(null,null,null)]
        [TestCase("" ,"", "")]
        [TestCase(" ", " ", " ")]
        public void Add_No_Communication_Medium_ThrowsException(string email, string mobile, string fax)
        {
            var v = new Vendor() { Email=email,Mobile = mobile, Fax= fax };

            var ex = Assert.Throws<NoMediumExists>(() => _bLVendor.Add(v));

            Assert.AreEqual(ex.GetType(), typeof(NoMediumExists));
        }

        [Test]
        public void EditVerified()
        {
            var v = new Vendor() { Email = "a@b.com", Mobile = "12334566", Fax = "1234456" };

            _bLVendor.Edit(v);
            _VendorRepository.Verify(q => q.Edit(v), Times.Once());
        }

        [Test]
        [TestCase(null, null, null)]
        [TestCase("", "", "")]
        [TestCase(" ", " ", " ")]
        public void Edit_No_Communication_Medium_ThrowsException(string email, string mobile, string fax)
        {
            var v = new Vendor() { Email = email, Mobile = mobile, Fax = fax };

            var ex = Assert.Throws<NoMediumExists>(() => _bLVendor.Edit(v));

            Assert.AreEqual(ex.GetType(), typeof(NoMediumExists));
        }
        [Test]
        public void DeleteVerified()
        {
            int id = 1;
            _bLVendor.Remove(id);
            _VendorRepository.Verify(q => q.Remove(id), Times.Once());
        }
        [Test]
        public void FindByIdVerified()
        {   
            int Id = 1;
            _bLVendor.FindById(Id);
            _VendorRepository.Verify(q => q.FindById(Id), Times.Once());
        }
        [Test]
        public void GetVendorsVerified()
        {
            _bLVendor.GetVendors();
            _VendorRepository.Verify(q => q.GetVendors(), Times.Once());
        }
        [Test]
        public void FindByNameVerified()
        {
            string name = "";
            _bLVendor.FindByName(name);
            _VendorRepository.Verify(q => q.FindByName(name), Times.Once());
        }
    }
}
