using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using IMSRepository.Models;
using IMSRepository.Repository.Interfaces;
using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace IMSRepository.Repository
{
    public class PODocumentsRepository : IPODocumentsRepository
    {
        BlobContainerClient _blobContainter;
        string _connectionstring;
        string _container;

        public PODocumentsRepository(string connectionString,string ContainerName)
        {
            BlobServiceClient _blobServiceClient = new BlobServiceClient(connectionString);

            _blobContainter = _blobServiceClient.GetBlobContainerClient(ContainerName);
            _connectionstring = connectionString;
            _container = ContainerName;

        }
        public void Add(PODocument podocuments)
        {

            if (podocuments.FileImage!=null)
            {
                BlobClient blobClient = _blobContainter.GetBlobClient(podocuments.PurchaseOrderId.ToString());
                blobClient.Upload(podocuments.FileImage.OpenReadStream(), false);
            }
            else
            {
                throw new FileNotFoundException("File Not found");
            }
        }

        public void Edit(PODocument podocuments)
        {
            if (podocuments.FileImage != null)
            {
                BlobClient blobClient = _blobContainter.GetBlobClient(podocuments.PurchaseOrderId.ToString());
                blobClient.Upload(podocuments.FileImage.OpenReadStream(), true);
            }
            else
            {
                throw new FileNotFoundException("File Not found");
            }
        }

        public async Task<Stream> Download(int PurchaseOrderId,string FileName)
        {
            try
            {

                MemoryStream ms = new MemoryStream();
                if (CloudStorageAccount.TryParse(_connectionstring, out CloudStorageAccount storageAccount))
                {
                    CloudBlobClient BlobClient = storageAccount.CreateCloudBlobClient();
                    CloudBlobContainer container = BlobClient.GetContainerReference(_container);

                    if (await container.ExistsAsync())
                    {
                        CloudBlob file = container.GetBlobReference(PurchaseOrderId.ToString());

                        if (await file.ExistsAsync())
                        {
                            await file.DownloadToStreamAsync(ms);
                            Stream blobStream = file.OpenReadAsync().Result;
                            return blobStream;
                        }
                        else
                        {
                            throw new Exception( "File does not exist");
                        }
                    }
                    else
                    {
                        throw new Exception("Container does not exist");
                    }
                }
                else
                {
                    throw new Exception("Error opening storage");
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<PODocument> FindByName(string podocuments)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<PODocument> GetPODocument()
        {
            List<PODocument> pODocuments = new List<PODocument>();

            foreach (BlobItem blobItem in _blobContainter.GetBlobs())
                {
                    pODocuments.Add(new PODocument { PurchaseOrderId = int.Parse(blobItem.Name) });
                }

            return pODocuments;
        }

        public void Remove(int Id)
        {
            try
            {
                _blobContainter.DeleteBlob(Id.ToString(), Azure.Storage.Blobs.Models.DeleteSnapshotsOption.IncludeSnapshots);
            }
            catch(Exception)
            {
                throw new FileNotFoundException("File for Order ID " + Id.ToString() +"not exists");
            }
        }
    }
}
