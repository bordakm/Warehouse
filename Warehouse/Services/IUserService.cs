using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Warehouse.Entities;

namespace Warehouse.Services
{
    public interface IUserService
    {
        IdentityResult AddUser(string email, string password, string fullname, string role);
        bool DeleteUser(string id);
        ICollection<string> GetAllRoles();
        ICollection<Employee> GetAllUsers();
        string GetUserRole(string id);
    }
}
