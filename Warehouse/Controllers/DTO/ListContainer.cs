using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;

namespace Warehouse.Controllers.DTO
{
    public class ListContainer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int StoredItemCount { get; set; }
        public string ItemsNames { get; set; }
        public string LastEmployee { get; set; }
    }
}
