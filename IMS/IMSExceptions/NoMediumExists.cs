using System;

namespace IMS.IMSExceptions
{
    public class NoMediumExists:Exception
    {
        public override string Message => "Vendor should have at least one Medium of Communication";
    }
}
