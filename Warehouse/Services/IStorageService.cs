using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Warehouse.Controllers.DTO;
using Warehouse.Entities;

namespace Warehouse.Services
{
    public interface IStorageService
    {
        ICollection<Item> GetAllItems();
        ICollection<Item> SearchItems(string word);
        Item GetItemById(int id);
        Item AddOrUpdateItem(ModelItem item);
        bool DeleteItem(int id);
        ICollection<Container> GetAllContainers();
        Container GetContainerById(int id);
        bool DeleteContainer(int id);
        Container AddOrUpdateContainer(NewContainer container);
    }
}
