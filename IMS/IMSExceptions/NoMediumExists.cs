using System;


namespace IMS.IMSExceptions
{
    [Serializable]
    public class NoMediumExistsException: Exception
    {
        public override string Message => "Vendor should have at least one Medium of Communication";
    }
}
