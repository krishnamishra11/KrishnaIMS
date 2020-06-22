using System;
using System.Runtime.Serialization;

namespace IMS.IMSExceptions
{
    [Serializable]
    public class NoMediumExistsException: Exception, ISerializable
    {
        public override string Message => "Vendor should have at least one Medium of Communication";
    }
}
