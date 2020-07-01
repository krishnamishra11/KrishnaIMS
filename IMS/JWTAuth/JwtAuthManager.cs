using IMS.JWTAuth.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace IMS.JWTAuth
{
    public class JwtAuthManager : IJwtAuthManager
    {
        
        private readonly string _key;
        public JwtAuthManager( string key)
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
