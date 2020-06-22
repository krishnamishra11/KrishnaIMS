using System;
using System.Runtime.Serialization;

namespace IMS.IMSExceptions
{
    [Serializable]
    public class InvalidDeliveryDateException : Exception, ISerializable
    {
        public override string Message => "Delivery Date should be greater then Order Date";

    }
}
