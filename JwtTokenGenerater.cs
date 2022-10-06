using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace UdemyWEBAPI
{
    public class JwtTokenGenerater
    {
        public string GenerateToken()
        {
            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Abdullahabdullah"));

            SigningCredentials credentials = new SigningCredentials(key,SecurityAlgorithms.HmacSha256);

            List<Claim> claims = new List<Claim>();

            claims.Add(new Claim(ClaimTypes.Role, "Member"));

            JwtSecurityToken token = new JwtSecurityToken(issuer:"http://localhost",audience:"http://localhost",claims : null,notBefore:DateTime.Now,
                                                            expires:DateTime.Now.AddMinutes(2),signingCredentials:credentials);  

            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();

            return handler.WriteToken(token);
        }
    }
}
