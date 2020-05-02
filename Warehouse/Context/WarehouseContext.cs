using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Warehouse.Models;

namespace Warehouse
{
    public class WarehouseContext : DbContext
    {
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer();
        //}

        public WarehouseContext(DbContextOptions options)
            :base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Container>().HasData(new Container() { Id = 1, Name = "T1"});
            modelBuilder.Entity<Item>().HasData(new Item() { Id = 1, Name = "Csavarhúzó", Number = 3, ContainerId = 1});
        }

        public DbSet<Item> Items { get; set; }
        public DbSet<Container> Containers { get; set; }


    }
}
