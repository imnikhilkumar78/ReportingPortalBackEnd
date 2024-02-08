using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Auth.Interface
{
    public interface IJWTInterface
    {
        public string GenerateUserToken(string UserEmail, string UserName);
        public JwtSecurityToken ValidateToken(string token);
    }
}
