using System;
using System.Runtime.Serialization;

namespace IMS.IMSExceptions
{
    public class VenoderIdNotFound:Exception, ISerializable
    {
        public override string Message => "Venoder id not found";
    }
}
