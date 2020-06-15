using IdentityServer4.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Warehouse.Data;
using Warehouse.Entities;

namespace Warehouse
{
    public class WarehouseContext : ApiAuthorizationDbContext<Employee>
    {

        public DbSet<Item> Items { get; set; }
        public DbSet<Container> Containers { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<LogEntry> LogEntries { get; set; }

        public WarehouseContext(DbContextOptions options,
            IOptions<OperationalStoreOptions> operationalStoreOptions) 
            : base(options, operationalStoreOptions)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.SeedDatabase();
        }
    }
}
