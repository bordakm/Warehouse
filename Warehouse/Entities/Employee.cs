using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Warehouse.Entities
{
    public class Employee : IdentityUser
    {
        public Employee()
            :base()
        {

        }
        public string FullName { get; set; }
    }
}
