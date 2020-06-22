using System;

namespace IMS.IMSExceptions
{
    [Serializable]
    public class VenoderIdNotFound:Exception
    {
        public override string Message => "Venoder id not found";
    }
}
