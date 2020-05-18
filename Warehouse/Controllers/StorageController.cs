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
        private readonly IStorageService storageService;

        public StorageController(WarehouseContext context, IStorageService itemService)
        {
            _context = context;
            this.storageService = itemService;
        }

        [HttpGet("items")]
        public ActionResult<IEnumerable<ListItem>> GetItems([FromQuery(Name = "search")]string searchTerm)
        {
            if (searchTerm == null)            
                return Ok(storageService.GetAllItems().Select(ToListItem));               
            else
                return Ok(storageService.SearchItems(searchTerm).Select(ToListItem));
        }

        [HttpGet("items/{id}")]
        public ActionResult<ModelItem> GetItems(int id)
        {
            return ToModelItem(storageService.GetItemById(id));
        }

        [HttpPost("items/")]
        public ActionResult<ModelItem> AddOrUpdateItem([FromBody]ModelItem addedItem)
        {
            return ToModelItem(storageService.AddOrUpdateItem(addedItem));
        }

        [HttpDelete("items/{id}")]
        public ActionResult<bool> DeleteItem(int id)
        {
            return storageService.DeleteItem(id);
        }



        [HttpGet("containers")]
        public ActionResult<IEnumerable<ListContainer>> GetContainers()
        {
            return Ok(storageService.GetAllContainers().Select(ToListContainer));
        }
        
        [HttpGet("containers/{id}")]
        public ActionResult<ModelContainer> GetContainer(int id)
        {
            return Ok(ToModelContainer(storageService.GetContainerById(id)));
        }

        
        [HttpPost("containers/")]
        public ActionResult<ModelContainer> AddOrUpdateContainer([FromBody]NewContainer addedItem)
        {
            return Ok(ToModelContainer(storageService.AddOrUpdateContainer(addedItem)));
        }
        

        [HttpDelete("containers/{id}")]
        public ActionResult<bool> DeleteContainer(int id)
        {
            return storageService.DeleteContainer(id);
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
            if (item == null) return null;
            return new ListItem
            {
                Id = item.Id,
                Name = item.Name,
                Description = item.Description,
                Count = item.Count,
                ContainerName = item.Container.Name
            };
        }

        private ModelItem ToModelItem(Item item)
        {
            if (item == null) return null;
            return new ModelItem
            {
                Id = item.Id,
                Name = item.Name,
                Description = item.Description,
                Count = item.Count,
                ContainerId = item.ContainerId
            };
        }

        private ListContainer ToListContainer(Container container)
        {
            if (container == null) return null;
            var itemNames = container.Items.Aggregate("", (i, y) => { return i + y.Name + ", "; });
            string shortItemNames = itemNames.Substring(0, Math.Min(itemNames.Length, 60));
            var listContainer = new ListContainer
            {
                Id = container.Id,
                Name = container.Name,
                StoredItemCount = container.Items.Sum(i => i.Count),
                ItemsNames = shortItemNames,
                LastEmployee = container.LastEmployee?.FullName
            };

            return listContainer;
        }

        private ModelContainer ToModelContainer(Container container)
        {
            if (container == null) return null;
            var listContainer = new ModelContainer
            {
                Id = container.Id,
                Name = container.Name,
                Items = container.Items.Select(i=>ToModelItem(i)).ToList(),
                LastEmployee = container.LastEmployee?.FullName
            };

            return listContainer;
        }

        
    }
}
