using System;
using System.Runtime.Serialization;

namespace IMS.IMSExceptions
{
    [Serializable]
    public class DeliveredOrderCanNotDeletedException : Exception, ISerializable
    {
        public override string Message => "Deleivered Order cannot be deleted";
    }
}
