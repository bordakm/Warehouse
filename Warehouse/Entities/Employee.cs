using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        [PersonalData]
        [MaxLength(255)]
        public override string Id { get; set; }
        public string FullName { get; set; }
    }
}
