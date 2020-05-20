using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Warehouse.Entities;
using Microsoft.EntityFrameworkCore;
using Warehouse.Controllers.DTO;

namespace Warehouse.Services
{
    public class StorageService : IStorageService
    {
        private WarehouseContext context;
        private ILogService logService;
        public StorageService(WarehouseContext context, ILogService logService)
        {
            this.context = context;
            this.logService = logService;
        }

        public ICollection<Item> GetAllItems()
        {
            return context.Items.Include(i=>i.Container).Select(x => x).ToList();
        }

        public ICollection<Item> SearchItems(string word)
        {
            word = word.ToLower();
            return context.Items.Include(i => i.Container)
                                .Where(
                                    x => x.Name.ToLower().Contains(word) ||
                                    x.Description.ToLower().Contains(word))
                                .ToList();
        }

        public Item AddOrUpdateItem(ModelItem item, string userid)
        {            
            Employee employee = context.Employees.FirstOrDefault(e => e.Id == userid);
            var dbItem = context.Items.Include(i=>i.Container).FirstOrDefault(i => i.Id == item.Id);
            
            if (dbItem == null)
            {
                dbItem = new Item()
                {
                    Name = item.Name,
                    Description = item.Description,
                    Count = item.Count,
                    ContainerId = item.ContainerId
                };
                context.Items.Add(dbItem);
                string containername = context.Containers.FirstOrDefault(c => c.Id == item.ContainerId).Name;
                logService.AddEntry(userid, $"Added {item.Count} item(s) {item.Name} to container {containername}");
            }
            else
            {
                string newcontainername = context.Containers.FirstOrDefault(c => c.Id == item.ContainerId).Name;
                if(dbItem.ContainerId!=item.ContainerId)
                    logService.AddEntry(userid, $"Moved {item.Name} from {dbItem.Container.Name} to {newcontainername}");
                if (dbItem.Name != item.Name)
                    logService.AddEntry(userid, $"Renamed {dbItem.Name} to {item.Name}");
                if(dbItem.Count!=item.Count)
                    logService.AddEntry(userid, $"{dbItem.Name} count changed from {dbItem.Count} to {item.Count}");
               
                dbItem.Container.LastEmployee = employee;
                dbItem.Name = item.Name;
                dbItem.Description = item.Description;
                dbItem.Count = item.Count;
                dbItem.ContainerId = item.ContainerId;                
            }
            Container newContainer = context.Containers.FirstOrDefault(c => c.Id == dbItem.ContainerId);
            if (newContainer != null) newContainer.LastEmployee = employee;
            context.SaveChanges();
            return dbItem;
        }

        public bool DeleteItem(int id, string userid)
        {
            var item = context.Items.Include(i=>i.Container).FirstOrDefault(i => i.Id == id);
            if (item == null) return false;
            logService.AddEntry(userid, $"Deleted {item.Name} from {item.Container.Name}");
            context.Items.Remove(item);
            context.SaveChanges();
            return true;
        }

        public Item GetItemById(int id)
        {
            return context.Items.FirstOrDefault(x=>x.Id == id);
        }

        public ICollection<Container> GetAllContainers()
        {
            return context
                .Containers
                .Include(c => c.Items)
                .Include(c => c.LastEmployee)
                .ToList();        
        }

        public Container GetContainerById(int id)
        {
            return context
               .Containers
               .Include(c => c.Items)
               .Include(c => c.LastEmployee)
               .FirstOrDefault(c => c.Id == id);
        }

        public bool DeleteContainer(int id, string userid)
        {
            var container = context.Containers.FirstOrDefault(c => c.Id == id);
            if (container == null) return false;
            logService.AddEntry(userid, $"Deleted container named {container.Name}");
            context.Containers.Remove(container);
            context.SaveChanges();
            return true;
        }

        public Container AddOrUpdateContainer(NewContainer container, string userid)
        {
            var dbContainer = context.Containers.FirstOrDefault(c => c.Id == container.Id);
            if (dbContainer != null && dbContainer.Name != container.Name)
            {
                logService.AddEntry(userid, $"Renamed container from {dbContainer.Name} to {container.Name}");
                dbContainer.Name = container.Name;               
            }
            else
            {
                dbContainer = new Container()
                {
                    Name = container.Name
                };
                context.Containers.Add(dbContainer);
                logService.AddEntry(userid, $"Added new container named {dbContainer.Name}");
            }
            context.SaveChanges();
            return dbContainer;
        }        
    }
}
