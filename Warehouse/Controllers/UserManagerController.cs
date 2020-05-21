using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        public ActionResult<IEnumerable<ModelUser>> ListAllUsers()
        {
            var users = userService.GetAllUsers().Select(ToModelUser);
            return Ok(users);
        }

        [HttpGet("roles")]
        public ActionResult<IEnumerable<string>> ListAllRoles()
        {
            var roles = userService.GetAllRoles();
            return Ok(roles);
        }

        [HttpPost]
        public ActionResult AddNewUser([FromBody]NewUser newuser)
        {
            var status = userService.AddUser(newuser.Email, newuser.Password, newuser.FullName, newuser.Role);
            if (status.Succeeded)               
                return Ok();
            return Conflict();
        }

        public ModelUser ToModelUser(Employee employee)
        {
            return new ModelUser
            {
                Id = employee.Id,
                Email = employee.Email,
                FullName = employee.FullName,
                Role = userService.GetUserRole(employee.Id)
            };
        }


        /*
        // GET: api/Items/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Item>> GetItem(int id)
        {
            var item = await _context.Items.FindAsync(id);

            if (item == null)
            {
                return NotFound();
            }

            return item;
        }

        // PUT: api/Items/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutItem(int id, Item item)
        {
            if (id != item.Id)
            {
                return BadRequest();
            }

            _context.Entry(item).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ItemExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Items
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Item>> PostItem(Item item)
        {
            _context.Items.Add(item);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetItem", new { id = item.Id }, item);
        }

        // DELETE: api/Items/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Item>> DeleteItem(int id)
        {
            var item = await _context.Items.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }

            _context.Items.Remove(item);
            await _context.SaveChangesAsync();

            return item;
        }

        private bool ItemExists(int id)
        {
            return _context.Items.Any(e => e.Id == id);
        }*/
    }
}

