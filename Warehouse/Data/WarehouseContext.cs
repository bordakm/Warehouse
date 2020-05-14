using IdentityServer4.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Warehouse.Models;

namespace Warehouse
{
    public class WarehouseContext : ApiAuthorizationDbContext<Employee>
    {
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer();
        //}

        public WarehouseContext(DbContextOptions options,
            IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
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
