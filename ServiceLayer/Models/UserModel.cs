using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Models
{
    public class UserModel
    {
        public int UserId;
        public string? UserName;
        public string? UserEmail;
        public byte[] PasswordHash;
        public byte[] PasswordSalt;
        public int EmpId;
    }
}
