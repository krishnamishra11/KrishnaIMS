using System;

namespace IMS.IMSExceptions
{
    public class DeliveredOrderCanNotDeleted:Exception
    {
        public override string Message => "Deleivered Order cannot be deleted";
    }
}
