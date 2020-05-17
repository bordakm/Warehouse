using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Warehouse.Entities;

namespace Warehouse.Services
{
    public interface IItemService
    {
        public ICollection<Item> GetAllItems();
    }
}
