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
        public StorageService(WarehouseContext context)
        {
            this.context = context;
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
            //dbItem.Container.LastEmployee = employee; TODO !!ha item null is!!
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
            }
            else
            {
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

        public bool DeleteItem(int id)
        {
            var item = context.Items.FirstOrDefault(i => i.Id == id);
            if (item == null) return false;
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

        public bool DeleteContainer(int id)
        {
            var container = context.Containers.FirstOrDefault(c => c.Id == id);
            if (container == null) 
                return false;
            context.Containers.Remove(container);
            context.SaveChanges();
            return true;
        }

        public Container AddOrUpdateContainer(NewContainer container)
        {
            var dbContainer = context.Containers.FirstOrDefault(c => c.Id == container.Id);
            if (dbContainer != null)
                dbContainer.Name = container.Name;
            else
            {
                dbContainer = new Container()
                {
                    Name = container.Name
                };
                context.Containers.Add(dbContainer);
            }
            context.SaveChanges();
            return dbContainer;
        }

        public bool ContainerExists(int id)
        {
            var cont = context.Containers.FirstOrDefault(c => c.Id == id);
            if (cont == null) return false;
            return true;
        }
    }
}
