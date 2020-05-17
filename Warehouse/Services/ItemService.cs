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
    public class ItemService : IItemService
    {
        private WarehouseContext context;
        public ItemService(WarehouseContext context)
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

        public AddedItem AddItem(AddedItem item)
        {
            context.Items.Add(new Item()
            {
                Name = item.Name,
                Description = item.Description,
                Count = item.Count,
                ContainerId = item.ContainerId
            });
            context.SaveChanges();
            return item;
        }
    }
}
