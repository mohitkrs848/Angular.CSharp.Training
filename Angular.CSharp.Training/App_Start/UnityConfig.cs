using Angular.CSharp.Training.Agents;
using Angular.CSharp.Training.Data.Repository;
using Angular.CSharp.Training.Data;
using System.Web.Http;
using Unity;
using Unity.Lifetime;
using Unity.WebApi;
using Angular.CSharp.Training.Services;
using Angular.CSharp.Training.Controllers;
using System.Reflection;
using Serilog;

namespace Angular.CSharp.Training
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            // Register your types here
            container.RegisterType<DemoDbContext>(new HierarchicalLifetimeManager());
            container.RegisterType<IEmployeeRepository, EmployeeRepository>();
            container.RegisterType<IUserRepository, UserRepository>();
            container.RegisterType<IProjectRepository, ProjectRepository>();

            container.RegisterType<IEmployeeService, EmployeeService>();
            container.RegisterType<IUserService, UserService>();
            container.RegisterType<IProjectService, ProjectService>();
            container.RegisterType<IAuthService, AuthService>();

            container.RegisterType<AuthController>();
            container.RegisterInstance<ILogger>(Log.Logger);


            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}