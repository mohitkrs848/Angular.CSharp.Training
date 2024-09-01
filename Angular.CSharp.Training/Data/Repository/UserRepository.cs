using Angular.CSharp.Training.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Angular.CSharp.Training.Data.Repository
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(DemoDbContext demoDbContext) : base(demoDbContext)
        {
        }

        public async Task CreateAsync(User user)
        {
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(user.PasswordHash);
            await InsertAsync(user);
        }

        public async Task DeleteUser(int id)
        {
            await DeleteAsync(id);
        }

        public async Task<object> GetAllUsers()
        {
            return await GetAllAsync();
        }

        public async Task<bool> GetByEmail(string email)
        {
            if(context.Users.Any(u => u.Email == email))
            {
                return true;
            }
            return false;
        }

        public async Task UpdateUser(User user)
        {
            await UpdateAsync(user);
        }
    }
}