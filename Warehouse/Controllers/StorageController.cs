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
    [Route("api/storage")]
    [ApiController]
    public class StorageController : ControllerBase
    {
        private readonly WarehouseContext _context;
        private readonly IItemService itemService;

        public StorageController(WarehouseContext context, IItemService itemService)
        {
            _context = context;
            this.itemService = itemService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<ListItem>> GetItems([FromQuery(Name = "search")]string searchTerm)
        {
            if (searchTerm == null)            
                return Ok(itemService.GetAllItems().Select(i => ToListItem(i)));               
            else
                return Ok(itemService.SearchItems(searchTerm).Select(ToListItem));
        }

        [HttpPost]
        public ActionResult<AddedItem> AddItem([FromBody]AddedItem addedItem)
        {
            return itemService.AddItem(addedItem);
        }
        /*
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
        }
        */






        private ListItem ToListItem(Item item)
        {
            return new ListItem
            {
                Name = item.Name,
                Description = item.Description,
                Count = item.Count,
                ContainerName = item.Container.Name
            };
        }

    }
}
