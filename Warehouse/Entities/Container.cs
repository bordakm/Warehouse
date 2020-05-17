using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Warehouse.Entities
{
    public class Container
    {
        public int Id { get; set; }
        
        public string Name { get; set; }
        public ICollection<Item> Items { get; set; } = new List<Item>();
        public Employee LastEmployee { get; set; }
    }
}
