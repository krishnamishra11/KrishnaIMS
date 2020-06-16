using IMS.JWTAuth.Interfaces;
using IMS.Models;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace IMS.JWTAuth
{
    public class JWDAuthManager : IJWTAuthManager
    {
        
        private readonly string _key;
        public JWDAuthManager( string key)
        {
            _key = key;
            
            
        }
        public string Authenticate(string name, string password)
        {

            var tokenHendler = new JwtSecurityTokenHandler();
            var tockenKey = Encoding.ASCII.GetBytes(_key);
            var tockenDiscriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                 {
                     new Claim(ClaimTypes.Name, name)
                 }),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tockenKey), SecurityAlgorithms.HmacSha256Signature)
            };

            var tocken = tokenHendler.CreateToken(tockenDiscriptor);
            return tokenHendler.WriteToken(tocken);
        }
    }
}
