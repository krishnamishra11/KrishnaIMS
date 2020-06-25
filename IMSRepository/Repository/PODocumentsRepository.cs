using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Azure.Storage.Blobs.Specialized;
using IMSRepository.Models;
using IMSRepository.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;

namespace IMSRepository.Repository
{
    public class PODocumentsRepository : IPODocumentsRepository
    {
        BlobContainerClient _blobContainter;

         public PODocumentsRepository(string connectionString,string ContainerName)
        {
            BlobServiceClient _blobServiceClient = new BlobServiceClient(connectionString);

            _blobContainter = _blobServiceClient.GetBlobContainerClient(ContainerName);
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

        public void Download(int PurchaseOrderId,string FilePath)
        {
            try
            {
                BlobDownloadInfo download = _blobContainter.GetBlobClient(PurchaseOrderId.ToString()).Download();

                using (FileStream downloadFileStream = File.OpenWrite(FilePath))
                {
                    download.Content.CopyTo(downloadFileStream);
                    downloadFileStream.Close();
                }
            }catch(Exception ex)
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
                _blobContainter.DeleteBlob(Id.ToString(), DeleteSnapshotsOption.IncludeSnapshots);
            }
            catch(Exception)
            {
                throw new FileNotFoundException("File for Order ID " + Id.ToString() +"not exists");
            }
        }
    }
}
