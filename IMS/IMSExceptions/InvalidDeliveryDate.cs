﻿using System;
using System.Runtime.Serialization;

namespace IMS.IMSExceptions
{
    public class InvalidDeliveryDate:Exception
    {
        public override string Message => "Delivery Date should be greater then Order Date";

    }
}
