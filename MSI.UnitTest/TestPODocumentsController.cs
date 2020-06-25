using IMS.BusinessLayer.Interfaces;
using IMS.Controllers;
using IMSRepository.Models;
using IMSRepository.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System.IO;

namespace UnitTestMSI
    {
        [TestFixture]
        public class TestPODocumentsController
        {
        
            Mock<IPODocumentsRepository> _pODocumentRepository;
            Mock<ILogger<PODocumentsController>> _logger;
            PODocumentsController documentsController;
            [SetUp]
            public void SetUp()
            {
                _pODocumentRepository = new Mock<IPODocumentsRepository>();
                _logger = new Mock<ILogger<PODocumentsController>>();
                documentsController = new PODocumentsController(_pODocumentRepository.Object, _logger.Object);

            }
            //[Test]
            //public void TestGetPODocumentsById()
            //{
            //    int id = 1;
            //    string filename = "abc";
            
            //    documentsController.Get(id,filename);

            //    _pODocumentRepository.Verify(q => q.Download(id,filename), Times.Once);
            //}

            [Test]
            public void TestGetPODocuments_Post()
            {
                int id = 1;
                PODocument document = new PODocument();
                
                documentsController.Post(document);
                _pODocumentRepository.Verify(q => q.Add(document), Times.Once);

            }

            [Test]
            public void TestGetPODocuments_Put()
            {
                PODocument document = new PODocument();
                documentsController.Put(document);
                _pODocumentRepository.Verify(q => q.Edit(document), Times.Once);
            }


            [Test]
            public void TestGetPODocuments_Delete()
            {
                int id = 1;
                PODocument document = new PODocument();
                
                documentsController.Delete(id);
                _pODocumentRepository.Verify(q => q.Remove(id), Times.Once);
            }

        }
    }
        