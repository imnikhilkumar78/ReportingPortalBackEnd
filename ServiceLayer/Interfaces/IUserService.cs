using DataLayer.Entities;
using ServiceLayer.DTO;
using ServiceLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Interfaces
{
    public interface IUserService
    {
        Task<int> AddUser(UserDTO user);
        Task<UserModel> Login(string username, string password);
        public List<Employee> GetAll();
    }
}
