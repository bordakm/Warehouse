using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Warehouse.Entities;

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
            return context.Items.Select(x => x).ToList();
        }
    }
}
