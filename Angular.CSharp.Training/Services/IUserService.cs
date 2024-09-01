using Angular.CSharp.Training.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Angular.CSharp.Training.Services
{
    public interface IUserService
    {
        Task CreateUser(User user);
        Task DeleteUser(int id);
        Task<object> GetAllUsers();
        Task<bool> GetUserByEmail(string email);
        Task UpdateUsers(User user);
    }
}
