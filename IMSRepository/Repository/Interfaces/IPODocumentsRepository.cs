using IMSRepository.Models;
using System.Collections.Generic;
using System.IO;

namespace IMSRepository.Repository.Interfaces
{
    public interface IPODocumentsRepository
    {
        public void Add(PODocument podocuments);
        public void Edit(PODocument podocuments);
        public void Remove(int Id);
        public IEnumerable<PODocument> GetPODocument();
        public Stream Download(int PurchaseOrderId, string FilePath);
        public  IEnumerable<PODocument> FindByName(string podocuments);
    }
}
