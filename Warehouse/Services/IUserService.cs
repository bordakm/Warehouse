using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Warehouse.Services
{
    public interface IUserService
    {
        IdentityResult AddUser(string email, string password, string fullname);
        bool AddUserToRole(string username, string role);
        bool RemoveUserRole(string username, string role);
    }
}
