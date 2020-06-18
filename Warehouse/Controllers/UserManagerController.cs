using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Warehouse;
using Warehouse.Controllers.DTO;
using Warehouse.Entities;
using Warehouse.Services;

namespace Warehouse.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserManagerController : ControllerBase
    {
        private readonly WarehouseContext _context;
        private readonly IUserService userService;

        public UserManagerController(WarehouseContext context, IUserService userService)
        {
            _context = context;
            this.userService = userService;
        }

        [HttpGet]
        [Authorize]
        public ActionResult<IEnumerable<ModelUser>> ListAllUsers()
        {
            var users = userService.GetAllUsers().Select(ToModelUser);
            return Ok(users);
        }

        [HttpGet("roles")]
        [Authorize]
        public ActionResult<IEnumerable<string>> ListAllRoles()
        {
            var roles = userService.GetAllRoles();
            return Ok(roles);
        }

        [HttpPost]
        [Authorize]
        public ActionResult AddNewUser([FromBody]NewUser newuser)
        {
            var status = userService.AddUser(newuser.Email, newuser.Password, newuser.FullName, newuser.Role);
            if (status.Succeeded)               
                return Ok();
            return Conflict();
        }

        private ModelUser ToModelUser(Employee employee)
        {
            return new ModelUser
            {
                Id = employee.Id,
                Email = employee.Email,
                FullName = employee.FullName,
                Role = userService.GetUserRole(employee.Id)
            };
        }
    }
}

