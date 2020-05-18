using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Warehouse.Controllers.DTO
{
    public class ModelContainer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<ModelItem> Items { get; set; }
        public string LastEmployee { get; set; }
    }
}
