using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Warehouse.Models
{
    public class Item
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ContainerId { get; set; }
        public int Number { get; set; } = 0;
        public Container Container { get; set; }
        
    }
}
