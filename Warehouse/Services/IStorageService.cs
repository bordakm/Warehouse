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
        Item AddOrUpdateItem(ModelItem item, string userid);
        bool DeleteItem(int id, string userid);
        ICollection<Container> GetAllContainers();
        Container GetContainerById(int id);
        bool DeleteContainer(int id, string userid);
        Container AddOrUpdateContainer(NewContainer container, string userid);
    }
}
