
using IMS.BusinessLayer.Interfaces;
using IMS.Controllers;
using IMS.JWTAuth.Interfaces;
using IMSRepository.Models;
using IMSRepository.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace UnitTestMSI
{
    [TestFixture]
    public class Test_authController
    {
        
        Mock<IPersonRepository>   _personRepository;
        Mock<ILogger<AuthController>> _logger;
        Mock<IJWTAuthManager> _jWTAuthManager;
        AuthController _authController;
        

        [SetUp]
        public void SetUp()
        {
            _personRepository = new Mock<IPersonRepository>();
            _logger = new Mock<ILogger<AuthController>>();
            _jWTAuthManager = new Mock<IJWTAuthManager>();
            _authController = new AuthController(_personRepository.Object, _jWTAuthManager.Object, _logger.Object);
        }
        [Test]
        public void TestGetPersonById()
        {
            int id = 1;

            _authController.GetPerson(id);
            _personRepository.Verify(q => q.FindById(id), Times.Once);

        }

        [Test]
        public void TestGetPerson_Post()
        {
            int id = 1;
            Person person = new Person();

            _authController.PostPerson(person);
            _personRepository.Verify(q => q.Add(person), Times.Once);

        }

        [Test]
        public void TestGetPerson_Put()
        {
            Person person = new Person();
            _authController.PutPerson(person);
            _personRepository.Verify(q => q.Edit(person), Times.Once);
        }

        [Test]
        public void TestGetPerson_Authenticate_Fail()
        {
            Person person = new Person();

            _personRepository.Setup(q => q.VerifyPerson(person)).Returns(false);
            _authController.Authenticate(person);
            _personRepository.Verify(q => q.Edit(person), Times.Never);
        }
        public void TestGetPerson_Authenticate_Pass()
        {
            Person person = new Person();

            _personRepository.Setup(q => q.VerifyPerson(person)).Returns(true);
            _authController.Authenticate(person);
            _personRepository.Verify(q => q.Edit(person), Times.Once);
        }

        [Test]
        public void TestGetPerson_Put_WithException()
        {
            int id = 1;
            Person person = new Person() { Id = id };

            _personRepository.Setup(q => q.Edit(person)).Throws(new DbUpdateConcurrencyException());
            _authController.PutPerson(person);

            _personRepository.Verify(q => q.FindById(id), Times.Once);
        }

        [Test]
        public void TestGetPerson_Delete()
        {
            int id = 1;
            Person person = new Person();
            _personRepository.Setup(q => q.FindById(id)).Returns(person);
            _authController.DeletePerson(id);
            _personRepository.Verify(q => q.Remove(id), Times.Once);
        }
        [Test]
        public void TestGetPerson_Delete_Admin()
        {
            int id = 1;
            Person person = new Person() { Name="admin"};
            _personRepository.Setup(q => q.FindById(id)).Returns(person);
            _authController.DeletePerson(id);
            _personRepository.Verify(q => q.Remove(id), Times.Never);
        }



    }
}
