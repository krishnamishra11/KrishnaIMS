using System;


namespace IMS.IMSExceptions
{
    [Serializable]
    public class VenoderIdNotFoundException : Exception
    {
        public override string Message => "Venoder id not found";
    }
}
