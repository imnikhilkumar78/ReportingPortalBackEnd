using Microsoft.IdentityModel.Tokens;
using ServiceLayer.Auth.Interface;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace ServiceLayer.Auth
{
    public class JWTUtilities : IJWTInterface
    {
        private readonly IWebHostEnvironment _environment;
        private readonly byte[] _key;
        public JWTUtilities(IWebHostEnvironment environment, IConfiguration configuration)
        {
            _environment = environment;
            var keyPath = Path.Combine(_environment.ContentRootPath, "App_Data", $"{environment.EnvironmentName}.key");
            _key = File.ReadAllBytes(keyPath);
        }
        public string GenerateUserToken(string UserEmail, string UserName)
        {
            var expirationTime = 8;

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                        new Claim("UserEmail", UserEmail),
                        new Claim("UserName", UserName)
                    }),
                Expires = DateTime.UtcNow.AddHours(expirationTime),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(_key),
                    SecurityAlgorithms.HmacSha512Signature)
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public JwtSecurityToken ValidateToken(string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(_key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                return jwtToken;
            }
            catch(Exception ex)
            {
                return null;
            }
        }
    }


}

