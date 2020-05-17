using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Warehouse.Controllers.DTO;
using Warehouse.Entities;

namespace Warehouse.Services
{
    public interface IItemService
    {
        public ICollection<Item> GetAllItems();
        public ICollection<Item> SearchItems(string word);
        public AddedItem AddItem(AddedItem item);
    }
}
