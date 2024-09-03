using Angular.CSharp.Training.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Angular.CSharp.Training.Data.Repository
{
    public interface IUserRepository
    {
        Task CreateAsync(User user);
        Task DeleteUser(int id);
        Task<object> GetAllUsers();
        Task<bool> GetByEmail(string email);
        Task UpdateUser(User user);
    }
}
