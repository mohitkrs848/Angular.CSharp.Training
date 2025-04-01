using Angular.CSharp.Training.Data;
using Angular.CSharp.Training.Models;
using NUnit.Framework;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Angular.CSharp.Training.Services
{
    public class AuthService : IAuthService
    {
        private readonly DemoDbContext demoDbContext;
        private readonly ILogger logger;

        public AuthService(DemoDbContext demoDbContext, ILogger logger)
        {
            this.demoDbContext = demoDbContext;
            this.logger = logger;
        }

        public async Task Register(UserRegistrationModel model)
        {
            try
            {
                var user = demoDbContext.Users.SingleOrDefault(u => u.Email == model.Email);
                if (user != null)
                {
                    logger.Information($"User {user.Email} already exists.");
                    throw new Exception("Email already exists");
                }

                var hashedPassword = BCrypt.Net.BCrypt.HashPassword(model.Password);

                var newUser = new User
                {
                    Email = model.Email,
                    PasswordHash = hashedPassword,
                    Role = "User" // Set the role
                };

                demoDbContext.Users.Add(newUser);
                await demoDbContext.SaveChangesAsync();

                logger.Information($"User {user.Email} registered successfully.");
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error occurred while registering user.");
                throw;
            }
        }

        public async Task<List<string>> Login(UserLoginModel model)
        {
            try
            {
                var user = demoDbContext.Users.SingleOrDefault(u => u.Email == model.Email);
                if (user == null || !BCrypt.Net.BCrypt.Verify(model.Password, user.PasswordHash))
                {
                    logger.Information($"Invalid email or password for user {model.Email}");
                    return null;
                }

                // Generate a token and return it along with the user's role
                var token = GenerateToken(user);
                List<string> result = new List<string>() { token, user.Role, user.Email.ToString().Split('@').First() };
                logger.Information($"User {user.Email} logged in successfully.");
                return result;
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Error occurred while logging in user.");
                throw;
            }
        }

        public static string GenerateToken(User user)
        {
            try
            {
                // Dummy token generation logic: base64 encode email and current timestamp
                var tokenPayload = $"{user.Email}:{DateTime.UtcNow}";
                var tokenBytes = Encoding.UTF8.GetBytes(tokenPayload);
                var token = Convert.ToBase64String(tokenBytes);
                return token;
            }
            catch (Exception ex)
            {
                //logger.Error(ex, "Error occurred while generating token.");
                throw;
            }
        }
    }
}