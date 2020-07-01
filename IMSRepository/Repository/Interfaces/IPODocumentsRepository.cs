using IMSRepository.Models;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace IMSRepository.Repository.Interfaces
{
    public interface IPODocumentsRepository
    {
        public void Add(PODocument podocuments);
        public void Add(PODocumentV2 podocuments);
        public void Edit(PODocument podocuments);
        public void Remove(int Id);
        public IEnumerable<PODocument> GetPODocument();
        public Task<Stream> Download(int PurchaseOrderId, string FileName);
        public Task<Stream> Download(string FileName);
        
    }
}
