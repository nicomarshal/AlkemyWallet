using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using AlkemyWallet.Entities;

namespace AlkemyWallet.Utilities
{
    public class JsonWebToken
    {
        private readonly IConfiguration _configuration;
        
        public JsonWebToken(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        
        public JwtSecurityToken CreateToken(User user)
        {
            if (user != null)
            {
                var authClaims = new List<Claim>
                    {
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Role, user.Role.Name),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    };

                //security key
                var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
                var credentials = new SigningCredentials(authSigningKey,SecurityAlgorithms.HmacSha256);
            
                var token = new JwtSecurityToken(
                    issuer: _configuration["JWT:ValidIssuer"],
                    audience: _configuration["JWT:ValidAudience"],
                    expires: DateTime.Now.AddHours(3),
                    claims: authClaims,
                    signingCredentials: credentials
                );

                return token;
            }
            return null;
        }
    }
}

/*
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using AlkemyWallet.Entities;
using TokenAPI.Models;

namespace AlkemyWallet.Utilities
{
    public class JsonWebToken
    {
        public static JwtSecurityToken CreateToken(JWT jwt, User user)
        {
            if (user != null)
            {
                var authClaims = new List<Claim>
                    {
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Role, user.Role.Name),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    };

                //security key
                var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.Key));
                var credentials = new SigningCredentials(authSigningKey,SecurityAlgorithms.HmacSha256);
            
                var token = new JwtSecurityToken(
                    issuer: jwt.Issuer,
                    audience: jwt.Audience,
                    expires: DateTime.Now.AddHours(3),
                    claims: authClaims,
                    signingCredentials: credentials
                );

                return token;
            }
            return null;
        }
    }
}*/