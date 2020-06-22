using System;


namespace IMS.IMSExceptions
{
    [Serializable]
    public class InvalidDeliveryDateException : Exception
    {
        public override string Message => "Delivery Date should be greater then Order Date";

    }
}
