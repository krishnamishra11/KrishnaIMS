using IMSRepository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IMS.JWTAuth.Interfaces
{
    public interface IJWTAuthManager
    {
        string Authenticate(string name, string password );
    }
}
