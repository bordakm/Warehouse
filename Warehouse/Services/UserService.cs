using Microsoft.AspNetCore.Identity;
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
        public UserService(UserManager<Employee> userManager)
        {
            this.userManager = userManager;
        }

        public IdentityResult AddUser(string email, string password, string fullname)
        {
           /* var employee = userManager.FindByEmailAsync(email).Result;            
            if (employee != null) return null;*/
            var result = userManager.CreateAsync(new Employee() { Email = email, UserName = email, FullName = fullname }, password).Result;
            return result;
        }

        public bool AddUserToRole(string username, string role)
        {
            throw new NotImplementedException();
        }

        public bool RemoveUserRole(string username, string role)
        {
            throw new NotImplementedException();
        }
    }
}
