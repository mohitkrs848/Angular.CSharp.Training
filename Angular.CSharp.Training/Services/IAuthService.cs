using Angular.CSharp.Training.Models;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Angular.CSharp.Training.Services
{
    public interface IAuthService
    {
        Task<List<string>> Login(UserLoginModel model);
        Task Register(UserRegistrationModel model);
    }
}
