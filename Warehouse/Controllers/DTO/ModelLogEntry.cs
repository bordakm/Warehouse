using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Warehouse.Controllers.DTO
{
    public class ModelLogEntry
    {
        public string Time { get; set; }
        public string EmployeeName{ get; set; }
        public string Text { get; set; }
    }
}
