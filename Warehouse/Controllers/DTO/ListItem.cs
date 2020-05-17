using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Warehouse.Controllers.DTO
{
    public class ListItem
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Count { get; set; }
        public string ContainerName { get; set; }
    }
}