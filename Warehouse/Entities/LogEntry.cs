using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Warehouse.Entities
{
    public class LogEntry
    {
        public DateTime Time { get; set; }
        public int Id { get; set; }
        [ForeignKey("Employee")]
        public string EmployeeId { get; set; }
        public Employee Employee { get; set; }
        public string Text { get; set; }
    }
}
