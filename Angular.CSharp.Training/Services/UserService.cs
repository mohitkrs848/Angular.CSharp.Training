using Angular.CSharp.Training.Data.Repository;
using Angular.CSharp.Training.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Angular.CSharp.Training.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task CreateUser(User user)
        {
            await _userRepository.CreateAsync(user);
        }

        public async Task DeleteUser(int id)
        {
            await _userRepository.DeleteUser(id);
        }

        public async Task<object> GetAllUsers()
        {
            return await _userRepository.GetAllUsers();
        }

        public async Task<bool> GetUserByEmail(string email)
        {
            return await _userRepository.GetByEmail(email);
        }

        public async Task UpdateUsers(User user)
        {
            await _userRepository.UpdateUser(user);
        }
    }
}