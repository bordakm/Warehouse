using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Warehouse.Entities;

namespace Warehouse.Data
{
    public static class ModelBuilderExtensions
    {
        public static void SeedDatabase(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().HasData(new Employee() { Email = "A1alma@alma.hu", EmailConfirmed = true, PasswordHash = "AQAAAAEAACcQAAAAEFa8TDCCW9LcnFG2PIHgFwElP4OrukACgRGKaMiVkhhd3D2ylJdTZyOujIM9Be+Z/A==" });
            modelBuilder.Entity<Container>().HasData(new Container() { Id = 1, Name = "T1" });
            modelBuilder.Entity<Item>().HasData(new Item() { Id = 1, Name = "Csavarhúzó", Count = 4, ContainerId = 1 });
        }
    }
}
