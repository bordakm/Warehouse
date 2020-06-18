using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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
    [Route("api/storage")]
    [ApiController]
    public class StorageController : ControllerBase
    {
        private readonly WarehouseContext _context;
        private readonly IStorageService storageService;
        private readonly ITemperatureService temperatureService;
        public StorageController(WarehouseContext context, IStorageService itemService, ITemperatureService temperatureService)
        {
            _context = context;
            this.storageService = itemService;
            this.temperatureService = temperatureService;
        }

        [HttpGet("items")]
        [Authorize]
        public ActionResult<IEnumerable<ListItem>> GetItems([FromQuery(Name = "search")]string searchTerm)
        {
            if (searchTerm == null)            
                return Ok(storageService.GetAllItems().Select(ToListItem));               
            else
                return Ok(storageService.SearchItems(searchTerm).Select(ToListItem));
        }

        [HttpGet("items/{id}")]
        [Authorize]
        public ActionResult<ModelItem> GetItem(int id)
        {
            return ToModelItem(storageService.GetItemById(id));
        }

        [HttpPost("items/")]
        [Authorize]
        public ActionResult<ModelItem> AddOrUpdateItem([FromBody]ModelItem addedItem)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return ToModelItem(storageService.AddOrUpdateItem(addedItem, userId));
        }

        [HttpDelete("items/{id}")]
        [Authorize]
        public ActionResult<bool> DeleteItem(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return storageService.DeleteItem(id, userId);
        }

        [HttpGet("containers")]
        [Authorize]
        public ActionResult<IEnumerable<ListContainer>> GetContainers()
        {
            return Ok(storageService.GetAllContainers().Select(ToListContainer));
        }
        
        [HttpGet("containers/{id}")]
        [Authorize]
        public ActionResult<ModelContainer> GetContainer(int id)
        {
            return Ok(ToModelContainer(storageService.GetContainerById(id)));
        }
        
        [HttpPost("containers/")]
        [Authorize]
        public ActionResult<ModelContainer> AddOrUpdateContainer([FromBody]NewContainer addedItem)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return Ok(ToModelContainer(storageService.AddOrUpdateContainer(addedItem, userId)));
        }        

        [HttpDelete("containers/{id}")]
        [Authorize]
        public ActionResult<bool> DeleteContainer(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return storageService.DeleteContainer(id, userId);
        }

        [HttpGet("temperature/c")]
        public ActionResult<TemperatureData> GetTemperatureC()
        {
            return new TemperatureData
            {
                Temperature = temperatureService.GetTemperatureCelsius(),
                Unit = "°C"
            };
        }

        [HttpGet("temperature/f")]
        public ActionResult<TemperatureData> GetTemperatureF()
        {
            return new TemperatureData
            {
                Temperature = temperatureService.GetTemperatureFahreinheit(),
                Unit = "°F"
            };
        }

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
