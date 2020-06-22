using System;
using System.Runtime.Serialization;

namespace IMS.IMSExceptions
{
    [Serializable]
    public class VenoderIdNotFoundException : Exception, ISerializable
    {
        public override string Message => "Venoder id not found";
    }
}
