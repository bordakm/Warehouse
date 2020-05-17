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
            modelBuilder.Entity<Container>().HasData(new Container() { Id = 1, Name = "T1" });
            modelBuilder.Entity<Container>().HasData(new Container() { Id = 2, Name = "T2" });
            modelBuilder.Entity<Container>().HasData(new Container() { Id = 3, Name = "T3" });
            modelBuilder.Entity<Item>().HasData(new Item() { Id = 1, Name = "Fúrógép", Description = "akkumulátoros", Count = 4, ContainerId = 1 });
            modelBuilder.Entity<Item>().HasData(new Item() { Id = 2, Name = "Csavarhúzó", Description = "kereszthornyos", Count = 4, ContainerId = 1 });
            modelBuilder.Entity<Item>().HasData(new Item() { Id = 3, Name = "Ethernet kábel", Description = "100 méter hosszú, fekete", Count = 4, ContainerId = 2 });
            modelBuilder.Entity<Item>().HasData(new Item() { Id = 4, Name = "Csavarok", Count = 100, Description="8mm", ContainerId = 3 });
        }
    }
}
