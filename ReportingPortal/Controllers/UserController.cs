using DataLayer.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.DTO;
using ServiceLayer.Interfaces;
using ServiceLayer.Models;

namespace ReportingPortal.Controllers
{
    [Route("api/[Controller]")]
    public class UserController : Controller
    {
        private readonly IUserService _service;
        public UserController(IUserService service)
        {
            _service = service;
        }

        [HttpGet]
        public IEnumerable<Employee> Get()
        {
            if(_service.GetAll() != null)
            {
                return _service.GetAll();
            }
            return Enumerable.Empty<Employee>();
        }

        [HttpPost]
        [Route("Register")]
        public async Task<ActionResult<UserDTO>> Post([FromBody] UserDTO user)
        {
            var UserDTO = await _service.AddUser(user);
            if (UserDTO != 0)
                return user;
            return BadRequest("Not able to Register");
        }


    }
}
