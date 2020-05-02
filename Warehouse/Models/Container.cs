﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Warehouse.Models
{
    public class Container
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Item> items { get; set; } = new List<Item>();
    }
}
