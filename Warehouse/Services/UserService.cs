using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Warehouse.Entities;

namespace Warehouse.Services
{
    public class UserService : IUserService
    {
        UserManager<Employee> userManager;
        private WarehouseContext context;
        public UserService(WarehouseContext context, UserManager<Employee> userManager)
        {
            this.userManager = userManager;
            this.context = context;
        }

        public IdentityResult AddUser(string email, string password, string fullname, string role)
        {
            var result = userManager.CreateAsync(new Employee() { Email = email, UserName = email, FullName = fullname, EmailConfirmed=true }, password).Result;
            var employee = userManager.FindByEmailAsync(email).Result;
            var result2 = userManager.AddToRoleAsync(employee, role).Result;

            return result2;
        }

        public ICollection<Employee> GetAllUsers()
        {
            return context.Employees.ToList();
        }

        public bool DeleteUser(string id)
        {
            var employee = context.Employees.FirstOrDefault(e => e.Id == id);
            if (employee == null) return false;
            var result = userManager.DeleteAsync(employee).Result;
            return result.Succeeded;
        }

        public string GetUserRole(string id)
        {
            var employee = context.Employees.FirstOrDefault(e => e.Id == id);
            var roles = userManager.GetRolesAsync(employee).Result;
            if (roles.Count > 0)
                return roles[0];
            return "";
        }
        public ICollection<string> GetAllRoles()
        {
            return context.Roles.Select(r=>r.Name).ToList();
        }
    }
}
