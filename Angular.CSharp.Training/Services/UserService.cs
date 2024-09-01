using Angular.CSharp.Training.Data.Repository;
using Angular.CSharp.Training.Models;
using Serilog;
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
        private readonly ILogger logger;
        public UserService(IUserRepository userRepository, ILogger logger)
        {
            _userRepository = userRepository;
            this.logger = logger;
        }

        public async Task CreateUser(User user)
        {
            try
            {
                await _userRepository.CreateAsync(user);
                logger.Information("User created successfully");
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Error occurred while creating user");
                throw;
            }
        }

        public async Task DeleteUser(int id)
        {
            try
            {
                await _userRepository.DeleteUser(id);
                logger.Information("User deleted successfully");
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Error occurred while deleting user with ID {Id}", id);
                throw;
            }
        }

        public async Task<object> GetAllUsers()
        {
            try
            {
                var users = await _userRepository.GetAllUsers();
                logger.Information("Retrieved all users successfully");
                return users;
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Error occurred while getting all users");
                throw;
            }
        }

        public async Task<bool> GetUserByEmail(string email)
        {
            try
            {
                var user = await _userRepository.GetByEmail(email);
                logger.Information("Retrieved user by email successfully");
                return user;
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Error occurred while getting user by email {Email}", email);
                throw;
            }
        }

        public async Task UpdateUsers(User user)
        {
            try
            {
                await _userRepository.UpdateUser(user);
                logger.Information("User updated successfully");
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Error occurred while updating user with ID {Id}", user.Id);
                throw;
            }
        }
    }
}