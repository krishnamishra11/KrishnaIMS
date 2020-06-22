using System;
using System.Runtime.Serialization;

namespace IMS.IMSExceptions
{
    public class NoMediumExists:Exception, ISerializable
    {
        public override string Message => "Vendor should have at least one Medium of Communication";
    }
}
