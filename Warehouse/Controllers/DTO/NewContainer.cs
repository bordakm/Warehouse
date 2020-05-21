using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Warehouse.Controllers.DTO
{
    public class NewContainer
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
