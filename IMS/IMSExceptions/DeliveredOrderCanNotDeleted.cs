using System;


namespace IMS.IMSExceptions
{
    [Serializable]
    public class DeliveredOrderCanNotDeletedException : Exception
    {
        public override string Message => "Deleivered Order cannot be deleted";
    }
}
