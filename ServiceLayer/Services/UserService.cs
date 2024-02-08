using DataLayer.DbContexts;
using DataLayer.Entities;
using Microsoft.AspNetCore.Identity;
using ServiceLayer.Auth.Interface;
using ServiceLayer.DTO;
using ServiceLayer.Interfaces;
using ServiceLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services
{
    public class UserService : IUserService
    {
        private readonly ReportingPortalDbContext _context;
        private readonly IJWTInterface _tokenService;
        private readonly IPasswordHasher<UserDTO> _passwordHasher;

        public UserService(ReportingPortalDbContext context,
            IJWTInterface tokenService,
            IPasswordHasher<UserDTO> passwordHasher)
        {
            _context = context;
            _tokenService = tokenService;
            _passwordHasher = passwordHasher;
        }
        public async Task<int> AddUser(UserDTO user)
        {
            try
            {
                var PasswordHashing = _passwordHasher.HashPassword(user, user.Password);
                byte[] HashedPassword = Encoding.ASCII.GetBytes(PasswordHashing);
                var User = new Employee
                {
                    EmpId = user.EmpId,
                    PasswordHash = HashedPassword,
                    UserEmail = user.UserEmail,
                    UserName = user.UserName,
                };
                await _context.Employees.AddAsync(User);
                await _context.SaveChangesAsync();

                //Removing Password as this object will get returned
                user.Password = "";

                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }
            
        }

        public Task<UserModel> Login(string username, string password)
        {
            throw new NotImplementedException();
        }

        public List<Employee> GetAll()
        {
            List<Employee> customers = _context.Employees.ToList();
            return customers;
        }
    }
}
