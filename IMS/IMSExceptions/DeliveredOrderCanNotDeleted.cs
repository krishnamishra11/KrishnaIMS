using System;
using System.Runtime.Serialization;

namespace IMS.IMSExceptions
{
    public class DeliveredOrderCanNotDeleted:Exception, ISerializable
    {
        public override string Message => "Deleivered Order cannot be deleted";
    }
}
